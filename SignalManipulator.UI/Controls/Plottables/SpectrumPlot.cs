using ScottPlot.Collections;
using ScottPlot.DataSources;
using ScottPlot.Plottables;
using SignalManipulator.Logic.AudioMath.Smoothing;
using SignalManipulator.Logic.Models;

namespace SignalManipulator.UI.Controls.Plottables
{
    public class SpectrumPlot : Signal
    {
        // Data
        private CircularBuffer<double> buffer;
        private double[] frequencies;
        private double[] magnitudes;

        // Sampling/sizing
        private int sampleRate;
        private int fftSize;

        // Smoothing
        private Smoother smootherSMA = new SmootherSMA(1);
        private Smoother smootherEMA = new SmootherEMA(0.0);

        // X-thread
        private object objectLock = new object();

        public SpectrumPlot(int sampleRate, int fftSize) : this(sampleRate, fftSize, "") { }
        public SpectrumPlot(int sampleRate, int fftSize, string channelName) : base(new SignalSourceDouble(new double[sampleRate], 1.0))
        {
            this.sampleRate = Math.Max(sampleRate, 1);

            buffer = new CircularBuffer<double>(fftSize);
            frequencies = new double[fftSize];
            magnitudes = new double[fftSize];
            Data = new SignalSourceDouble(magnitudes, 1.0);

            ResizeBuffer(fftSize);

            LegendText = channelName;
        }

        public void ResizeBuffer(int fftSize)
        {
            lock (objectLock)
            {
                this.fftSize = fftSize;
                buffer = new CircularBuffer<double>(fftSize);
                frequencies = new double[fftSize];
                magnitudes = new double[fftSize];
                Data = new SignalSourceDouble(magnitudes, 1.0);
                UpdatePeriod(sampleRate);
            }
        }

        public void AddSamples(double[] samples)
        {
            lock (objectLock)
            {
                foreach (var sample in samples)
                    buffer.Add(sample);

                // Get magnitudes computing the FFT from waveform
                var fft = FFTFrame.FromWaveform(buffer.ToArray(), sampleRate);

                // Smooth values
                double[] emaSmoothed = smootherEMA.Smooth(fft.Magnitudes);
                double[] smaSmoothed = smootherSMA.Smooth(emaSmoothed);
                smaSmoothed.CopyTo(magnitudes, 0);
            }
        }

        public void UpdatePeriod(int sampleRate)
        {
            lock (objectLock)
            {
                this.sampleRate = sampleRate;
                Data.Period = (double)sampleRate / fftSize; // BinSize
            }
        }

        public void ClearBuffer()
        {
            lock (objectLock)
            {
                buffer.Clear();
                while (!buffer.IsFull) buffer.Add(0);
                Array.Clear(frequencies);
                Array.Clear(magnitudes);
            }
        }

        public void SetSMA(int historyLength) => smootherSMA.Set(historyLength);
        public void SetEMA(double alpha) => smootherEMA.Set(alpha);
    }
}