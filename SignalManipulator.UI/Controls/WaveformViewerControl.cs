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
        private IAudioEventDispatcher audioEventDispatcher;
        private readonly ConcurrentQueue<WaveformFrame> pendingFrames = new ConcurrentQueue<WaveformFrame>();
        private int sampleRate;

        // Circular buffers: one for each signal to plot
        private CircularBuffer<double> stereoBuf, leftBuf, rightBuf;
        private double[] stereoArr, leftArr, rightArr;

        private Signal stereoSig, leftSig, rightSig;

        // Zooming and panning parameteres
        private double zoom = 1.0;
        private double pan = 0.0;
        private double startX = 0.0;
        private double endX = 0.0;

        // Track whether a redraw is needed
        private volatile bool needsRender = false;

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
            audioEventDispatcher.OnLoad += (_, info) => ResizeBuffers(info.SampleRate);
            audioEventDispatcher.OnStopped += (_, e) => ClearBuffers();
            audioEventDispatcher.WaveformReady += (_, frame) =>
            {
                pendingFrames.Enqueue(frame);
                ProcessPendingFrames(); // Process immediately
                needsRender = true;
            };

            UIUpdateService.Instance.Register(RenderPlot);

            // Binding control events
            zoomSlider.ValueChanged += (_, v) => { zoom = v; ViewerZoomingChanged(); };
            panSlider.ValueChanged += (_, v) => { pan = v; ViewerZoomingChanged(); };
            monoCheckBox.CheckedChanged += (_, e) => ToggleStreams();
        }

        private void InitializePlot()
        {
            // Init plot
            var plt = formsPlot.Plot;
            plt.Title("Signal Waveform");
            plt.XLabel("Time"); plt.YLabel("Amplitude");
            formsPlot.UserInputProcessor.Disable();

            // Resize buffers
            ResizeBuffers(AudioEngine.SAMPLE_RATE);

            // Add each signal to the plot
            stereoSig = plt.Add.Signal(stereoArr);
            leftSig = plt.Add.Signal(leftArr);
            rightSig = plt.Add.Signal(rightArr);

            // Legend setup
            stereoSig.LegendText = "Stereo"; leftSig.LegendText = "Left"; rightSig.LegendText = "Right";
            plt.ShowLegend();

            // Set bounds
            plt.Axes.SetLimitsX(0, sampleRate);
            plt.Axes.SetLimitsY(-1, 1);

            // Final setups
            ToggleStreams(); // Hide or show streams
            ClearBuffers();  // Start from scratch
        }

        private void ResizeBuffers(int newSampleRate)
        {
            if (newSampleRate == sampleRate) return;
            sampleRate = newSampleRate;

            // Init buffers
            stereoBuf = new CircularBuffer<double>(sampleRate);
            leftBuf = new CircularBuffer<double>(sampleRate);
            rightBuf = new CircularBuffer<double>(sampleRate);
            stereoArr = new double[sampleRate];
            leftArr = new double[sampleRate];
            rightArr = new double[sampleRate];

            // Reallocate data sources
            if (stereoSig != null) stereoSig.Data = new SignalSourceDouble(stereoArr, 1);
            if (leftSig != null) leftSig.Data = new SignalSourceDouble(leftArr, 1);
            if (rightSig != null) rightSig.Data = new SignalSourceDouble(rightArr, 1);
        }

        private void ClearBuffers()
        {
            // Clear pending data
            while (pendingFrames.TryDequeue(out _));

            // Clear buffers
            stereoBuf.Clear(); rightBuf.Clear(); leftBuf.Clear();

            // Fill buffers with 0s
            while (!stereoBuf.IsFull) stereoBuf.Add(0);
            while (!rightBuf.IsFull) rightBuf.Add(0);
            while (!leftBuf.IsFull) leftBuf.Add(0);

            // Clear arrays (and fill them with 0s)
            Array.Clear(stereoArr, 0, stereoArr.Length);
            Array.Clear(leftArr, 0, leftArr.Length);
            Array.Clear(rightArr, 0, rightArr.Length);

            // Back to the initial bounds
            startX = 0;
            endX = sampleRate;
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
            }
        }

        private void RenderPlot()
        {
            if (!needsRender) return;

            formsPlot.Plot.Axes.SetLimitsX(startX, endX);
            formsPlot.Refresh();
            needsRender = false;
        }

        private void ViewerZoomingChanged()
        {
            double vis = sampleRate / zoom;
            double ctr = ((pan + 1) / 2) * (sampleRate - vis) + vis / 2;
            startX = ctr - vis / 2;
            endX = ctr + vis / 2;
            needsRender = true;
        }
    }
}