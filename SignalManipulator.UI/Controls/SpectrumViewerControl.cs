using ScottPlot.Collections;
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
    public partial class SpectrumViewerControl : UserControl
    {
        // FFT configuration and visualization
        private const int FFTSize = 1024;                  // Must be power of 2
        private const int MAX_MAGNITUDE_DB = 125;

        // Audio & buffer
        private IAudioEventDispatcher audioEventDispatcher;
        private readonly ConcurrentQueue<WaveformFrame> pendingFrames = new ConcurrentQueue<WaveformFrame>();
        private readonly CircularBuffer<double> audioBuffer = new CircularBuffer<double>(FFTSize);
        private int sampleRate = AudioEngine.SAMPLE_RATE;

        // Plotting
        private Signal spectrumPlot;
        private double[] magnitudes = new double[FFTSize];
        private volatile bool needsRender = false;

        // FFT properties
        private double BinSize => (double)sampleRate / FFTSize;
        private int MaxFrequency => sampleRate / 2;

        // Window config
        private double zoom = 1.0, pan = 0.0;            // Pan and zoom
        private double startX = 0.0;                     // Start X of window
        private double endX = 0.0;                       // End X of window


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
            audioEventDispatcher.OnLoad += (s, info) => { sampleRate = info.SampleRate; UpdateDataPeriod(); };
            audioEventDispatcher.OnStopped += (s, e) => ClearBuffers();
            audioEventDispatcher.WaveformReady += (s, frame) => { pendingFrames.Enqueue(frame); ProcessPendingFrames(); };

            UIUpdateService.Instance.Register(RenderPlot);
        }

        private void InitializePlot()
        {
            var plt = formsPlot.Plot;
            plt.Title("FFT Spectrum");
            plt.XLabel("Frequency (Hz)"); plt.YLabel("Magnitude (dB)");
            formsPlot.UserInputProcessor.Disable();

            // Set plotting
            spectrumPlot = plt.Add.Signal(magnitudes);

            // Set bounds
            UpdateWindowLimits();
            plt.Axes.SetLimitsX(startX, endX);
            plt.Axes.SetLimitsY(0, MAX_MAGNITUDE_DB);
            formsPlot.Refresh();

            // Clear buffers
            ClearBuffers();
        }

        private void ClearBuffers()
        {
            // Clear pending data
            while (pendingFrames.TryDequeue(out _)) ;

            // Fill buffers with 0s
            while (!audioBuffer.IsFull) audioBuffer.Add(0);

            Array.Clear(magnitudes, 0, magnitudes.Length);

            needsRender = true;
        }

        private void ProcessPendingFrames()
        {
            bool updated = false;

            while (pendingFrames.TryDequeue(out var frame))
            {
                updated = true;

                foreach (var sample in frame.DoubleMono)
                    audioBuffer.Add(sample);
            }

            if (!updated) return;

            // Get magnitudes from FFT
            var fft = FFTFrame.FromFFT(audioBuffer.ToArray(), sampleRate);
            fft.Magnitudes.CopyTo(magnitudes, 0);

            needsRender = true;
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
            int capacity = MaxFrequency;   // Samples in the entire window
            int view = (int)(MaxFrequency / zoom);         // Samples to show

            // Normalized pan [–1,+1] → [0, capacity–view]
            double panNorm = (pan + 1.0) / 2.0;
            startX = panNorm * (capacity - view);
            endX = startX + view;

            needsRender = true;
        }

        private void UpdateDataPeriod()
        {
            spectrumPlot.Data.Period = BinSize;

            // Update the bounds of the window
            UpdateWindowLimits();
        }
    }
}