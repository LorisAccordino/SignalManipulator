using ScottPlot.Collections;
using ScottPlot.DataSources;
using ScottPlot.Plottables;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Events;
using SignalManipulator.Logic.Models;
using SignalManipulator.UI.Helpers;
using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    [ExcludeFromCodeCoverage]
    public partial class WaveformViewerControl : UserControl
    {
        // Window config
        private const int MAX_WINDOWS_SECONDS = 10;       // Maximum limit
        private int windowSeconds = 1;                   // Current shown seconds
        private double zoom = 1.0, pan = 0.0;            // Pan and zoom
        private double startX = 0.0;                     // Start X of window
        private double endX = 0.0;                       // End X of window

        // Audio & buffer
        private IAudioEventDispatcher audioEventDispatcher;
        private readonly ConcurrentQueue<WaveformFrame> pendingFrames = new ConcurrentQueue<WaveformFrame>();
        private int sampleRate = AudioEngine.SAMPLE_RATE;

        // Circular buffer of capacity WindowCapacity = sampleRate * MaxWindowSeconds
        CircularBuffer<double> stereoBuf, leftBuf, rightBuf;

        // Arrays on which the Signal draws: length = WindowCapacity
        private double[] stereoArr, leftArr, rightArr;

        // SignalPlot (created once)
        private Signal stereoSig, leftSig, rightSig;

        // UI dirty flag
        private volatile bool needsRender = false;

        // Total capacity of the window (fixed based on MAX_WINDOWS_SECONDS)
        private int WindowCapacity => sampleRate * windowSeconds;

        // Current number of samples to show = windowSeconds * sampleRate / zoom
        private int ViewCapacity => (int)(windowSeconds * sampleRate / zoom);

        public WaveformViewerControl()
        {
            InitializeComponent();

            if (!DesignModeHelper.IsDesignMode)
            {
                audioEventDispatcher = AudioEngine.Instance.AudioEventDispatcher;
                InitializeEvents();
                InitializePlot();
            }
        }

        private void InitializeEvents()
        {
            audioEventDispatcher.OnLoad += (_, info) => { sampleRate = info.SampleRate; ConfigureBuffers(); };
            audioEventDispatcher.OnStopped += (_, e) => ClearBuffers();
            audioEventDispatcher.WaveformReady += (_, frame) => { pendingFrames.Enqueue(frame); ProcessPendingFrames(); };

            UIUpdateService.Instance.Register(RenderPlot);

            // Binding viewing and scaling events
            zoomSlider.ValueChanged += (_, v) => { zoom = v; UpdateWindowLimits(); };
            panSlider.ValueChanged += (_, v) => { pan = v; UpdateWindowLimits(); };
            secNum.ValueChanged += (_, v) => { windowSeconds = (int)secNum.Value; UpdateDataPeriod(); };
            secNum.Maximum = MAX_WINDOWS_SECONDS;
            monoCheckBox.CheckedChanged += (_, e) => ToggleStreams();
        }

        private void InitializePlot()
        {
            // Init plot
            var plt = formsPlot.Plot;
            plt.Title("Signal Waveform");
            plt.XLabel("Time (samples)"); plt.YLabel("Amplitude");
            formsPlot.UserInputProcessor.Disable();

            // Resize buffers
            ConfigureBuffers();

            // Add each signal to the plot
            stereoSig = plt.Add.Signal(stereoArr);
            leftSig = plt.Add.Signal(leftArr);
            rightSig = plt.Add.Signal(rightArr);

            // Legend setup
            stereoSig.LegendText = "Stereo"; leftSig.LegendText = "Left"; rightSig.LegendText = "Right";
            plt.ShowLegend();

            // Set bounds
            UpdateWindowLimits();
            plt.Axes.SetLimitsX(startX, endX);
            plt.Axes.SetLimitsY(-1, 1);
            formsPlot.Refresh();

            // Final setups
            ToggleStreams(); // Hide or show streams
            ClearBuffers();  // Start from scratch
        }

        private void ConfigureBuffers()
        {
            // Init buffers
            stereoBuf = new CircularBuffer<double>(sampleRate);
            leftBuf = new CircularBuffer<double>(sampleRate);
            rightBuf = new CircularBuffer<double>(sampleRate);
            stereoArr = new double[sampleRate];
            leftArr = new double[sampleRate];
            rightArr = new double[sampleRate];

            // Return if signals are null
            if (stereoSig == null || leftSig == null || rightSig == null)
                return;

            // Reallocate data sources
            stereoSig.Data = new SignalSourceDouble(stereoArr, 1);
            leftSig.Data = new SignalSourceDouble(leftArr, 1);
            rightSig.Data = new SignalSourceDouble(rightArr, 1);

            // Update data period
            UpdateDataPeriod();
        }

        private void ClearBuffers()
        {
            // Clear pending data
            while (pendingFrames.TryDequeue(out _));

            // Fill buffers with 0s
            while (!stereoBuf.IsFull) stereoBuf.Add(0);
            while (!leftBuf.IsFull) leftBuf.Add(0);
            while (!rightBuf.IsFull) rightBuf.Add(0);

            // Back to the initial bounds
            startX = 0;
            endX = sampleRate * windowSeconds;
            needsRender = true;
        }

        private void ToggleStreams()
        {
            bool mono = monoCheckBox.Checked;
            stereoSig.IsVisible = !mono;
            leftSig.IsVisible = mono;
            rightSig.IsVisible = mono;
            needsRender = true;
        }

        private void ProcessPendingFrames()
        {
            bool updated = false;

            while (pendingFrames.TryDequeue(out var frame))
            {
                updated = true;

                // Decimate samples for each channel/signal
                DecimateSamples(frame.DoubleMono, stereoBuf, windowSeconds);
                DecimateSamples(frame.DoubleSplitStereo.Left, leftBuf, windowSeconds);
                DecimateSamples(frame.DoubleSplitStereo.Right, rightBuf, windowSeconds);
            }

            if (!updated) return;

            stereoBuf.CopyTo(stereoArr, 0);
            leftBuf.CopyTo(leftArr, 0);
            rightBuf.CopyTo(rightArr, 0);
            needsRender = true;
        }

        private void DecimateSamples(double[] source, CircularBuffer<double> target, int step)
        {
            for (int i = 0; i < source.Length; i += step)
                target.Add(source[i]);
        }


        private void RenderPlot()
        {
            if (!needsRender) return;

            formsPlot.Plot.Axes.SetLimitsX(startX, endX);
            formsPlot.Refresh();
            needsRender = false;
        }

        private void UpdateWindowLimits()
        {
            int capacity = WindowCapacity;   // Samples in the entire window
            int view = ViewCapacity;         // Samples to show

            // Normalized pan [–1,+1] → [0, capacity–view]
            double panNorm = (pan + 1.0) / 2.0;
            startX = panNorm * (capacity - view);
            endX = startX + view;

            needsRender = true;
        }

        private void UpdateDataPeriod()
        {
            stereoSig.Data.Period = windowSeconds;
            leftSig.Data.Period = windowSeconds;
            rightSig.Data.Period = windowSeconds;

            // Update the bounds of the window
            UpdateWindowLimits();
        }
    }
}