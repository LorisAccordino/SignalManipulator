using ScottPlot.DataSources;
using SignalManipulator.Logic.AudioMath.Smoothing;
using SignalManipulator.Logic.Data;
using SignalManipulator.Logic.Data.Channels;

namespace SignalManipulator.UI.Controls.User.Plottables.Signals
{
    public class Spectrum : BaseSignalPlot
    {
        public AudioChannel Channel { get; set; } = AudioChannel.Stereo;

        private int fftSize;
        private double[] magnitudes = [];
        private Smoother smootherSMA = new SmootherSMA(1);
        private Smoother smootherEMA = new SmootherEMA(0.0);

        public Spectrum(int sampleRate, int fftSize) : this(sampleRate, fftSize, "") { }
        public Spectrum(int sampleRate, int fftSize, string channelName = "") : base(sampleRate, channelName)
        {
            ResizeBuffer(fftSize);
        }

        public override void ResizeBuffer(int fftSize)
        {
            if (this.fftSize == fftSize) return;

            lock (lockObject)
            {
                this.fftSize = fftSize;
                buffer = new Logic.Utils.CircularBuffer<double>(fftSize);
                magnitudes = new double[fftSize];
                data = magnitudes; // Alias for base compatibility
                Signal.Data = new SignalSourceDouble(magnitudes, 1.0);
                UpdatePeriod(sampleRate);
            }
        }

        public void AddData(FFTSlice fft)
        {
            lock (lockObject)
            {
                double[] ema = smootherEMA.Smooth(fft.Magnitudes[Channel]);
                double[] sma = smootherSMA.Smooth(ema);
                sma.CopyTo(magnitudes, 0);
            }
        }

        public override void UpdatePeriod(int sampleRate)
        {
            lock (lockObject)
            {
                this.sampleRate = sampleRate;
                Signal.Data.Period = (double)sampleRate / fftSize;
            }
        }

        public void SetSMA(int historyLength) => smootherSMA.Set(historyLength);
        public void SetEMA(double alpha) => smootherEMA.Set(alpha);
    }
}