using ScottPlot.Collections;
using ScottPlot.Plottables;
using SignalManipulator.Logic.AudioMath;
using SignalManipulator.Logic.AudioMath.Smoothing;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Events;
using SignalManipulator.Logic.Models;
using SignalManipulator.UI.Helpers;
using SignalManipulator.UI.Misc;
using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    [ExcludeFromCodeCoverage]
    public partial class SpectrumViewerControl : UserControl
    {
        // FFT configuration and visualization
        private const int FFT_SIZE = 8192;                  // Must be power of 2
        private const int MAX_MAGNITUDE_DB = 125;

        // Audio & buffer
        private IAudioEventDispatcher audioEventDispatcher;
        private readonly ConcurrentQueue<WaveformFrame> pendingFrames = new ConcurrentQueue<WaveformFrame>();
        private readonly CircularBuffer<double> audioBuffer = new CircularBuffer<double>(FFT_SIZE);
        private int sampleRate = AudioEngine.SAMPLE_RATE;
        private readonly object lockObject = new object();


        // FFT data
        private SmootherSMA smootherSMA = new SmootherSMA(1);
        private SmootherEMA smootherEMA = new SmootherEMA(0.0);
        private double[] magnitudes = new double[FFT_SIZE];

        // Plotting
        private Signal spectrumPlot;
        private volatile bool needsRender = false;

        // FFT properties
        private double BinSize => (double)sampleRate / FFT_SIZE;
        private int MaxFrequency => sampleRate / 2;


        public SpectrumViewerControl()
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
            // Main events
            audioEventDispatcher.OnLoad += OnLoad;
            audioEventDispatcher.OnStarted += OnStarted;
            audioEventDispatcher.OnStopped += OnStopped;

            // Update event
            UIUpdateService.Instance.Register(RenderPlot);

            // Force stop event to init purpose
            OnStopped(this, EventArgs.Empty);

            // Other events
            smaNum.ValueChanged += (s, e) => { smootherSMA.SetHistoryLength((int)smaNum.Value); };
            emaNum.ValueChanged += (s, e) => { smootherEMA.SetAlpha((double)emaNum.Value); };
        }

        public void OnLoad(object? sender, AudioInfo info)
        {
            sampleRate = info.SampleRate;
            UpdateDataPeriod();
        }

        public void OnStarted(object? sender, EventArgs e)
        {
            audioEventDispatcher.WaveformReady += ProcessFrame;
            settingsPanel.Enabled = true; // Enable UI
        }

        public void OnStopped(object? sender, EventArgs e)
        {
            audioEventDispatcher.WaveformReady -= ProcessFrame;
            ClearBuffers();
            ResetUI();
        }

        private void ResetUI()
        {
            // Checkbox and numeric up-down
            smaNum.Value = 1;
            emaNum.Value = 0M;

            // Navigator
            navigator.Zoom = 1;
            navigator.Pan = -1;

            // Disable UI
            settingsPanel.Enabled = false;
        }

        private void InitializePlot()
        {
            var plt = formsPlot.Plot;
            plt.Title("FFT Spectrum");
            plt.XLabel("Frequency (Hz)"); plt.YLabel("Magnitude (dB)");
            formsPlot.UserInputProcessor.Disable();

            // Set plotting
            spectrumPlot = plt.Add.Signal(magnitudes);

            // Set the bounds
            plt.Axes.SetLimitsY(0, MAX_MAGNITUDE_DB);
            navigator.Capacity = MaxFrequency;

            // Force the update
            needsRender = true;
            RenderPlot();

            // Clear buffers
            ClearBuffers();
        }

        private void ClearBuffers()
        {
            lock (lockObject)
            {
                while (pendingFrames.TryDequeue(out _));

                audioBuffer.Clear();
                while (!audioBuffer.IsFull) audioBuffer.Add(0);

                Array.Clear(magnitudes);
            }

            needsRender = true;
        }

        private void ProcessFrame(object? sender, WaveformFrame frame)
        {
            lock (lockObject)
            {
                foreach (var sample in frame.DoubleMono)
                    audioBuffer.Add(sample);

                // Get magnitudes from FFT
                var fft = FFTFrame.FromFFT(audioBuffer.ToArray(), sampleRate);

                // Smooth values
                double[] emaSmoothed = smootherEMA.Smooth(fft.Magnitudes);
                double[] smaSmoothed = smootherSMA.Smooth(emaSmoothed);

                // Clamp values
                smaSmoothed.CopyTo(magnitudes, 0);
            }

            needsRender = true;
        }

        private void RenderPlot()
        {
            lock (lockObject)
            {
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
            spectrumPlot.Data.Period = BinSize;
            navigator.Capacity = MaxFrequency;
        }
    }
}