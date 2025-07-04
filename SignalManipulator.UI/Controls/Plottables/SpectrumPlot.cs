﻿using ScottPlot.Collections;
using ScottPlot.DataSources;
using SignalManipulator.Logic.AudioMath.Smoothing;
using SignalManipulator.Logic.Models;

namespace SignalManipulator.UI.Controls.Plottables
{
    public class SpectrumPlot : BaseSignalPlot
    {
        private int fftSize;
        private double[] magnitudes;
        private Smoother smootherSMA = new SmootherSMA(1);
        private Smoother smootherEMA = new SmootherEMA(0.0);

        public SpectrumPlot(int sampleRate, int fftSize) : this(sampleRate, fftSize, "") { }
        public SpectrumPlot(int sampleRate, int fftSize, string channelName = "") : base(sampleRate, channelName)
        {
            ResizeBuffer(fftSize);
        }

        public override void ResizeBuffer(int fftSize)
        {
            lock (lockObject)
            {
                this.fftSize = fftSize;
                buffer = new CircularBuffer<double>(fftSize);
                magnitudes = new double[fftSize];
                data = magnitudes; // Alias for base compatibility
                Data = new SignalSourceDouble(magnitudes, 1.0);
                UpdatePeriod(sampleRate);
            }
        }

        public override void AddSamples(double[] samples)
        {
            lock (lockObject)
            {
                foreach (var s in samples)
                    buffer.Add(s);

                var fft = FFTFrame.FromWaveform(buffer.ToArray(), sampleRate);
                double[] ema = smootherEMA.Smooth(fft.Magnitudes);
                double[] sma = smootherSMA.Smooth(ema);
                sma.CopyTo(magnitudes, 0);
            }
        }

        public override void UpdatePeriod(int sampleRate)
        {
            lock (lockObject)
            {
                this.sampleRate = sampleRate;
                Data.Period = (double)sampleRate / fftSize;
            }
        }

        public void SetSMA(int historyLength) => smootherSMA.Set(historyLength);
        public void SetEMA(double alpha) => smootherEMA.Set(alpha);
    }
}