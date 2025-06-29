﻿using ScottPlot.Collections;
using ScottPlot.Plottables;
using SignalManipulator.Logic.AudioMath;
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
            audioEventDispatcher.OnLoad += (s, info) => { sampleRate = info.SampleRate; UpdateDataPeriod(); };
            audioEventDispatcher.OnStopped += (s, e) => ClearBuffers();
            audioEventDispatcher.WaveformReady += (s, frame) => { pendingFrames.Enqueue(frame); ProcessPendingFrames(); };

            UIUpdateService.Instance.Register(RenderPlot);

            smaNum.ValueChanged += (s, e) => { smootherSMA.SetHistoryLength((int)smaNum.Value); };
            emaNum.ValueChanged += (s, e) => { smootherEMA.SetAlpha((double)emaNum.Value); };
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
            //plt.Axes.SetLimitsY(0, FFT_SIZE / 16);
            navigatorControl.Navigator.SetCapacity(MaxFrequency);

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
                while (pendingFrames.TryDequeue(out _)) ;
                while (!audioBuffer.IsFull) audioBuffer.Add(0);
                Array.Clear(magnitudes, 0, magnitudes.Length);
            }

            needsRender = true;
        }

        private void ProcessPendingFrames()
        {
            bool updated = false;

            lock (lockObject)
            {
                while (pendingFrames.TryDequeue(out var frame))
                {
                    updated = true;

                    foreach (var sample in frame.DoubleMono)
                        audioBuffer.Add(sample);
                }

                if (!updated) return;

                // Get magnitudes from FFT
                var fft = FFTFrame.FromFFT(audioBuffer.ToArray(), sampleRate);

                // Smooth values
                double[] emaSmoothed = smootherEMA.Smooth(fft.Magnitudes);
                double[] smaSmoothed = smootherSMA.Smooth(emaSmoothed);
                smaSmoothed.CopyTo(magnitudes, 0);
            }

            needsRender = true;
        }

        private void RenderPlot()
        {
            if (!needsRender) return;

            lock (lockObject)
            {
                var navigator = navigatorControl.Navigator;
                if (navigator.NeedsUpdate)
                {
                    navigator.Recalculate();
                    formsPlot.Plot.Axes.SetLimitsX(navigator.Start, navigator.End);
                }

                formsPlot.Refresh();
            }
            
            needsRender = false;
        }

        private void UpdateDataPeriod()
        {
            spectrumPlot.Data.Period = BinSize;
            navigatorControl.Navigator.SetCapacity(MaxFrequency);
        }
    }
}