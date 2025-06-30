using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace SignalManipulator.Logic.Providers
{
    /*
    public class ResampleSpeedProvider : ISampleProvider
    {
        readonly ISampleProvider source;
        readonly WaveFormat waveFormat;

        // Input interleaved buffer
        float[] sourceBuffer = Array.Empty<float>();
        // *Frame* position (not samples): 0.0 = first frame, 1.0 = second frame, ecc.
        double sourcePosition = 0.0;

        public double SpeedRatio { get; set; } = 1.0;

        public WaveFormat WaveFormat => waveFormat;

        public ResampleSpeedProvider(ISampleProvider source)
        {
            this.source = source;
            waveFormat = source.WaveFormat;

            if (waveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
                throw new InvalidOperationException("Only IEEE float format supported");
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int channels = waveFormat.Channels;
            if (channels < 1) return 0;

            // Stereo/mono frame number needed
            int neededFrames = (int)(count / channels * SpeedRatio) + 3;
            int neededSamples = neededFrames * channels;
            if (sourceBuffer.Length < neededSamples)
                sourceBuffer = new float[neededSamples];

            // Read from the source
            int readSamples = source.Read(sourceBuffer, 0, neededSamples);
            int availableFrames = readSamples / channels;

            int framesWritten = 0;
            while (framesWritten < count / channels)
            {
                // posFrame and fractioning
                int posFrame = (int)sourcePosition;
                double frac = sourcePosition - posFrame;

                // If there are not frames enough for the cubic interpolation, break
                if (posFrame < 1 || posFrame + 2 >= availableFrames)
                    break;

                // For each channel
                for (int ch = 0; ch < channels; ch++)
                {
                    // Source buffer indexes
                    int i0 = (posFrame - 1) * channels + ch;
                    int i1 = posFrame * channels + ch;
                    int i2 = (posFrame + 1) * channels + ch;
                    int i3 = (posFrame + 2) * channels + ch;

                    float p0 = sourceBuffer[i0];
                    float p1 = sourceBuffer[i1];
                    float p2 = sourceBuffer[i2];
                    float p3 = sourceBuffer[i3];

                    // Hermite/catmull‑rom coeff
                    double t = frac, t2 = t * t, t3 = t2 * t;
                    double m0 = (p2 - p0) * 0.5;
                    double m1 = (p3 - p1) * 0.5;
                    double a0 = 2 * p1 - 2 * p2 + m0 + m1;
                    double a1 = -3 * p1 + 3 * p2 - 2 * m0 - m1;
                    double a2 = m0;
                    double a3 = p1;

                    float sample = (float)(a0 * t3 + a1 * t2 + a2 * t + a3);
                    buffer[offset + framesWritten * channels + ch] = sample;
                }

                framesWritten++;
                sourcePosition += SpeedRatio;
            }

            // Keep only the [0,1) interval for the next Read()
            sourcePosition -= (int)sourcePosition;

            return framesWritten * channels;
        }
    }
    */

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
            get => speedRatio;
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

        private void RebuildResampler()
        {
            int newRate = (int)(source.WaveFormat.SampleRate * speedRatio);
            // ricrea il provider con il nuovo sample rate
            resampler = new WdlResamplingSampleProvider(source, newRate);
        }

        public int Read(float[] buffer, int offset, int count)
        {
            // semplicemente inoltra al resampler
            return resampler.Read(buffer, offset, count);
        }
    }
}