using ScottPlot.Collections;
using ScottPlot.DataSources;
using ScottPlot.Plottables;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Events;
using SignalManipulator.Logic.Models;
using SignalManipulator.UI.Helpers;
using SignalManipulator.UI.Misc;
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
        private int windowSeconds = 1;                    // Current shown seconds

        // Audio & buffer
        private IAudioEventDispatcher audioEventDispatcher;
        private readonly ConcurrentQueue<WaveformFrame> pendingFrames = new ConcurrentQueue<WaveformFrame>();
        private int sampleRate = AudioEngine.SAMPLE_RATE;
        private readonly object lockObject = new object();

        // Buffers, arrays and signals
        private CircularBuffer<double> stereoBuf, leftBuf, rightBuf;
        private double[] stereoArr, leftArr, rightArr;
        private Signal stereoSig, leftSig, rightSig;

        // UI dirty flag
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
            audioEventDispatcher.OnLoad += (_, info) => { sampleRate = info.SampleRate; ConfigureBuffers(); };
            audioEventDispatcher.OnStopped += (_, e) => { ClearBuffers(); UIUpdateService.Instance.ForceUpdate(); };
            audioEventDispatcher.WaveformReady += (_, frame) => { pendingFrames.Enqueue(frame); ProcessPendingFrames(); };

            UIUpdateService.Instance.Register(RenderPlot);

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

            // Set the bounds
            plt.Axes.SetLimitsY(-1, 1);
            navigatorControl.Navigator.SetCapacity(sampleRate);

            // Force the update
            needsRender = true;
            RenderPlot();

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

            lock (lockObject)
            {
                stereoBuf.Clear(); leftBuf.Clear(); rightBuf.Clear();

                while (!stereoBuf.IsFull) stereoBuf.Add(0);
                while (!leftBuf.IsFull) leftBuf.Add(0);
                while (!rightBuf.IsFull) rightBuf.Add(0);

                Array.Clear(stereoArr);
                Array.Clear(leftArr);
                Array.Clear(rightArr);
            }

            needsRender = true;
        }

        private void ToggleStreams()
        {
            bool mono = monoCheckBox.Checked;

            lock (lockObject)
            {
                stereoSig.IsVisible = !mono;
                leftSig.IsVisible = mono;
                rightSig.IsVisible = mono;
            }

            needsRender = true;
        }

        private void ProcessPendingFrames()
        {
            bool updated = false;

            while (pendingFrames.TryDequeue(out var frame))
            {
                updated = true;

                lock (lockObject)
                {
                    // Decimate samples for each channel/signal
                    DecimateSamples(frame.DoubleMono, stereoBuf, windowSeconds);
                    DecimateSamples(frame.DoubleSplitStereo.Left, leftBuf, windowSeconds);
                    DecimateSamples(frame.DoubleSplitStereo.Right, rightBuf, windowSeconds);

                    stereoBuf.CopyTo(stereoArr, 0);
                    leftBuf.CopyTo(leftArr, 0);
                    rightBuf.CopyTo(rightArr, 0);
                }
            }

            if (updated) 
                needsRender = true;
        }

        private void DecimateSamples(double[] source, CircularBuffer<double> target, int step)
        {
            for (int i = 0; i < source.Length; i += step)
                target.Add(source[i]);
        }

        private void RenderPlot()
        {
            lock (lockObject)
            {
                var navigator = navigatorControl.Navigator;
                if (navigator.NeedsUpdate)
                {
                    navigator.Recalculate();
                    formsPlot.Plot.Axes.SetLimitsX(navigator.Start, navigator.End);
                    needsRender = true;
                }

                if (needsRender)
                    formsPlot.Refresh();
            }

            needsRender = false;
        }

        private void UpdateDataPeriod()
        {
            stereoSig.Data.Period = windowSeconds;
            leftSig.Data.Period = windowSeconds;
            rightSig.Data.Period = windowSeconds;

            navigatorControl.Navigator.SetCapacity(sampleRate * windowSeconds);
        }
    }
}