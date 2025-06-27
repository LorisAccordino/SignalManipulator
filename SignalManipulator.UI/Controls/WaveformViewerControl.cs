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
        private const int MAX_WINDOWS_SECONDS = 5;       // Maximum limit
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

        // Total capacity of the buffer and the window (fixed based on MAX_WINDOWS_SECONDS)
        private int BufferCapacity => sampleRate * MAX_WINDOWS_SECONDS;
        private int WindowCapacity => sampleRate * windowSeconds;

        // Current number of samples to show = windowSeconds * sampleRate / zoom
        private int ViewSamples => (int)(windowSeconds * sampleRate / zoom);

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
            audioEventDispatcher.OnLoad += (_, info) => { sampleRate = info.SampleRate; ResizeBuffers(); };
            audioEventDispatcher.OnStopped += (_, e) => ClearBuffers();
            audioEventDispatcher.WaveformReady += (_, frame) => { pendingFrames.Enqueue(frame); ProcessPendingFrames(); };

            UIUpdateService.Instance.Register(RenderPlot);

            // Binding viewing and scaling events
            zoomSlider.ValueChanged += (_, v) => { zoom = v; UpdateViewWindow(); };
            panSlider.ValueChanged += (_, v) => { pan = v; UpdateViewWindow(); };
            secNum.ValueChanged += (_, v) => { windowSeconds = (int)secNum.Value; UpdateViewWindow(); UpdateDataOffset(); };
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
            ResizeBuffers();

            // Add each signal to the plot
            stereoSig = plt.Add.Signal(stereoArr);
            leftSig = plt.Add.Signal(leftArr);
            rightSig = plt.Add.Signal(rightArr);

            // Legend setup
            stereoSig.LegendText = "Stereo"; leftSig.LegendText = "Left"; rightSig.LegendText = "Right";
            plt.ShowLegend();

            // Set bounds
            UpdateViewWindow();
            plt.Axes.SetLimitsX(startX, endX);
            plt.Axes.SetLimitsY(-1, 1);
            formsPlot.Refresh();

            // Final setups
            ToggleStreams(); // Hide or show streams
            ClearBuffers();  // Start from scratch
        }

        private void ResizeBuffers()
        {
            // Init buffers
            stereoBuf = new CircularBuffer<double>(BufferCapacity);
            leftBuf = new CircularBuffer<double>(BufferCapacity);
            rightBuf = new CircularBuffer<double>(BufferCapacity);
            stereoArr = new double[BufferCapacity];
            leftArr = new double[BufferCapacity];
            rightArr = new double[BufferCapacity];

            // Reallocate data sources
            if (stereoSig != null) stereoSig.Data = new SignalSourceDouble(stereoArr, 1);
            if (leftSig != null) leftSig.Data = new SignalSourceDouble(leftArr, 1);
            if (rightSig != null) rightSig.Data = new SignalSourceDouble(rightArr, 1);

            // Update data offset
            UpdateDataOffset();
        }

        private void UpdateDataOffset()
        {
            int xOffset = -(MAX_WINDOWS_SECONDS - windowSeconds) * sampleRate;
            if (stereoSig?.Data != null) stereoSig.Data.XOffset = xOffset;
            if (leftSig?.Data != null) leftSig.Data.XOffset = xOffset;
            if (rightSig?.Data != null) rightSig.Data.XOffset = xOffset;
            needsRender = true;
        }

        private void ClearBuffers()
        {
            // Clear pending data
            while (pendingFrames.TryDequeue(out _));

            // Clear buffers
            stereoBuf.Clear(); rightBuf.Clear(); leftBuf.Clear();

            // Fill buffers with 0s
            while (!stereoBuf.IsFull) stereoBuf.Add(0);
            while (!leftBuf.IsFull) leftBuf.Add(0);
            while (!rightBuf.IsFull) rightBuf.Add(0);

            // Clear arrays (and fill them with 0s)
            Array.Clear(stereoArr, 0, stereoArr.Length);
            Array.Clear(leftArr, 0, leftArr.Length);
            Array.Clear(rightArr, 0, rightArr.Length);

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

                // Add mono and stereo samples
                foreach (double sample in frame.DoubleMono) stereoBuf.Add(sample);
                foreach (double sample in frame.DoubleSplitStereo.Left) leftBuf.Add(sample);
                foreach (double sample in frame.DoubleSplitStereo.Right) rightBuf.Add(sample);
            }

            if (updated)
            {
                stereoBuf.CopyTo(stereoArr, 0);
                leftBuf.CopyTo(leftArr, 0);
                rightBuf.CopyTo(rightArr, 0);
                needsRender = true;
            }
        }

        private void RenderPlot()
        {
            if (!needsRender) return;

            formsPlot.Plot.Axes.SetLimitsX(startX, endX);
            formsPlot.Refresh();
            needsRender = false;
        }

        private void UpdateViewWindow()
        {
            // Calculate the window to show
            double center = ((pan + 1) / 2.0) * (WindowCapacity - ViewSamples) + ViewSamples / 2.0;
            startX = center - ViewSamples / 2.0;
            endX = center + ViewSamples / 2.0;
            needsRender = true;
        }
    }
}