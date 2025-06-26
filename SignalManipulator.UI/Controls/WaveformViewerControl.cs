using ScottPlot.Collections;
using ScottPlot.Plottables;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Events;
using SignalManipulator.Logic.Models;
using SignalManipulator.UI.Helpers;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    [ExcludeFromCodeCoverage]
    public partial class WaveformViewerControl : UserControl
    {
        private IAudioEventDispatcher audioEventDispatcher;
        private readonly ConcurrentQueue<WaveformFrame> pendingFrames = new ConcurrentQueue<WaveformFrame>();
        private int sampleRate = AudioEngine.SAMPLE_RATE;

        // Circular buffers: one for each signal to plot
        private CircularBuffer<double> stereoBuf, leftBuf, rightBuf;
        private double[] stereoArr, leftArr, rightArr;

        private Signal stereoSig, leftSig, rightSig;

        public double Zoom { get; set; } = 1.0;

        // Track whether a redraw is needed
        private volatile bool needsRender = false;

        public WaveformViewerControl()
        {
            InitializeComponent();

            // Save the UI context
            //uiContext = SynchronizationContext.Current;

            if (!DesignModeHelper.IsDesignMode)
            {
                audioEventDispatcher = AudioEngine.Instance.AudioEventDispatcher;
                InitializeEvents();
                InitializePlot();
            }
        }

        private void InitializeEvents()
        {
            audioEventDispatcher.OnLoad += (_, info) => HandleLoad(info.SampleRate);
            audioEventDispatcher.OnStopped += (_, e) => ClearBuffers();
            audioEventDispatcher.WaveformReady += (_, frame) =>
            {
                pendingFrames.Enqueue(frame);
                ProcessPendingFrames(); // Process immediately
                needsRender = true;
            };

            UIUpdateService.Instance.Register(RenderPlot);

            zoomSlider.ValueChanged += ZoomChanged;
            monoCheckBox.CheckedChanged += (_, e) => ToggleStreams();
        }

        private void InitializePlot()
        {
            var plt = formsPlot.Plot;
            plt.Title("Signal Waveform");
            plt.XLabel("Time"); plt.YLabel("Amplitude");
            formsPlot.UserInputProcessor.Disable();

            // Init buffers
            stereoBuf = new CircularBuffer<double>(sampleRate);
            leftBuf = new CircularBuffer<double>(sampleRate);
            rightBuf = new CircularBuffer<double>(sampleRate);
            stereoArr = new double[sampleRate];
            leftArr = new double[sampleRate];
            rightArr = new double[sampleRate];

            // Add each signal to the plot
            stereoSig = plt.Add.Signal(stereoArr);
            leftSig = plt.Add.Signal(leftArr);
            rightSig = plt.Add.Signal(rightArr);

            // Legend setup
            stereoSig.LegendText = "Stereo"; leftSig.LegendText = "Left"; rightSig.LegendText = "Right";
            ToggleStreams();

            plt.Axes.SetLimitsX(0, sampleRate);
            plt.Axes.SetLimitsY(-1, 1);
            plt.ShowLegend();

            ToggleStreams(); // Hide or show streams
            ClearBuffers();  // Start from scratch
        }

        private void HandleLoad(int newSampleRate)
        {
            if (newSampleRate == sampleRate) return;
            sampleRate = newSampleRate;

            // Reset all the streamers with the new sample rate
            formsPlot.Plot.Clear();
            InitializePlot();
            formsPlot.Refresh();
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
            formsPlot.Plot.Axes.SetLimitsX(0, sampleRate);
            formsPlot.Refresh();
        }

        private void ToggleStreams()
        {
            bool mono = monoCheckBox.Checked;
            stereoSig.IsVisible = !mono;
            leftSig.IsVisible = mono;
            rightSig.IsVisible = mono;
        }

        private void ProcessPendingFrames()
        {
            bool updated = false;

            while (pendingFrames.TryDequeue(out var frame))
            {
                updated = true;

                foreach (double sample in frame.DoubleMono)
                    stereoBuf.Add(sample);

                if (monoCheckBox.Checked)
                {
                    foreach (double sample in frame.DoubleSplitStereo.Left)
                        leftBuf.Add(sample);
                    foreach (double sample in frame.DoubleSplitStereo.Right)
                        rightBuf.Add(sample);
                }
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

            formsPlot.Refresh();
            needsRender = false;
        }

        private void ZoomChanged(object sender, double value)
        {
            Zoom = value;
            double visible = sampleRate / Zoom;
            double start = sampleRate - visible;
            UIUpdateService.Instance.Enqueue(() => formsPlot.Plot.Axes.SetLimitsX(start, sampleRate));
        }
    }
}