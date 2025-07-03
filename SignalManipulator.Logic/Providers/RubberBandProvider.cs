using NAudio.Wave;
using RubberBandSharp;
using SignalManipulator.Logic.AudioMath;

namespace SignalManipulator.Logic.Providers
{
    public class RubberBandProvider : ISampleProvider, IWaveProvider
    {
        private readonly ISampleProvider sourceProvider;
        private readonly WaveFormat waveFormat;
        private RubberBandStretcherStereo stretcher;
        private readonly Queue<float> leftBuffer = new();
        private readonly Queue<float> rightBuffer = new();
        private readonly int blockSize = 512; // Size for Process/Retrieve calls
        private float[] leftInput;
        private float[] rightInput;
        private float[] sourceBuffer;

        public double TimeRatio { get; set; } = 1.0;
        public double PitchRatio { get; set; } = 1.0;

        public WaveFormat WaveFormat => waveFormat;

        public RubberBandProvider(IWaveProvider source) : this(source.ToSampleProvider()) { }
        public RubberBandProvider(ISampleProvider source)
        {
            sourceProvider = source;
            waveFormat = source.WaveFormat;

            if (waveFormat.Channels != 2)
                throw new NotSupportedException("RubberBandProvider currently only supports stereo audio");

            Reset();
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int samplesWritten = 0;

            while (samplesWritten < count)
            {
                // At least 2 samples needed (stereo)
                while ((leftBuffer.Count < 1 || rightBuffer.Count < 1) && sourceProvider != null)
                {
                    // Read new samples
                    int read = sourceProvider.Read(sourceBuffer, 0, sourceBuffer.Length);
                    if (read == 0) return samplesWritten; // EOF

                    sourceBuffer.SplitStereo(leftInput, rightInput, read / 2);

                    stretcher.SetTimeRatio(TimeRatio);
                    stretcher.SetPitchScale(PitchRatio);
                    stretcher.Process(leftInput, rightInput, (uint)(read / 2), false);

                    while (stretcher.Available() > 0)
                    {
                        float[] leftOut = new float[blockSize];
                        float[] rightOut = new float[blockSize];
                        uint retrieved = stretcher.Retrieve(leftOut, rightOut, (uint)blockSize);

                        for (int i = 0; i < retrieved; i++)
                        {
                            leftBuffer.Enqueue(leftOut[i]);
                            rightBuffer.Enqueue(rightOut[i]);
                        }
                    }
                }

                // Now, there should be at least one sample per channel
                if (leftBuffer.Count == 0 || rightBuffer.Count == 0) break; // End of source (EOF)

                buffer[offset + samplesWritten++] = leftBuffer.Dequeue();
                buffer[offset + samplesWritten++] = rightBuffer.Dequeue();
            }

            return samplesWritten;
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            float[] samples = new float[count / 4];
            int read = Read(samples, 0, count / 4);
            samples.CopyToBytes(buffer, offset, read * 4);
            return read * 4;
        }

        public void Reset()
        {
            // Recreate the stretcher from scratch
            stretcher = new RubberBandStretcherStereo(
                waveFormat.SampleRate,
                RubberBandStretcher.Options.ProcessRealTime |
                RubberBandStretcher.Options.FormantPreserved |
                RubberBandStretcher.Options.PitchHighConsistency |
                RubberBandStretcher.Options.SmoothingOn |
                RubberBandStretcher.Options.StretchElastic |
                RubberBandStretcher.Options.TransientsSmooth);

            // Set initial TimeRatio and PitchRatio
            stretcher.SetTimeRatio(TimeRatio);
            stretcher.SetPitchScale(PitchRatio);

            leftInput = new float[blockSize];
            rightInput = new float[blockSize];
            sourceBuffer = new float[blockSize * 2]; // Stereo

            // Clear output buffers
            leftBuffer.Clear();
            rightBuffer.Clear();
        }
    }
}