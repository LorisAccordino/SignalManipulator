using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace SignalManipulator.Logic.Providers
{
    public class ResampleSpeedProvider : ISampleProvider
    {
        private readonly ISampleProvider source;
        private WdlResamplingSampleProvider resampler;
        private double speedRatio;

        public WaveFormat WaveFormat => resampler.WaveFormat;

        /// <summary>
        /// Time ratio di: 1.0 = normal, 2.0 = double, 0.5 = half.
        /// Changing this value, the inner resampler it'll be automatically rebuild.
        /// </summary>
        public double SpeedRatio
        {
            get => 1.0 / speedRatio;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(SpeedRatio));
                if (Math.Abs(value - speedRatio) < 0.0001) return;
                speedRatio = 1.0 / value;
                RebuildResampler();
            }
        }

        public ResampleSpeedProvider(ISampleProvider source, float initialSpeed = 1.0f)
        {
            this.source = source;
            speedRatio = initialSpeed;
            RebuildResampler();
        }

        public void RebuildResampler()
        {
            int newRate = (int)(source.WaveFormat.SampleRate * speedRatio);
            resampler = new WdlResamplingSampleProvider(source, newRate);
        }

        public int Read(float[] buffer, int offset, int count)
        {
            // semplicemente inoltra al resampler
            return resampler.Read(buffer, offset, count);
        }
    }
}