using NAudio.Wave;
using RubberBandSharp;

namespace SignalManipulator.Logic.Providers
{
    public class RubberBandProvider : ISampleProvider, IWaveProvider
    {
        private readonly ISampleProvider sourceProvider;
        private readonly WaveFormat waveFormat;
        private readonly RubberBandStretcherStereo stretcher;
        private readonly Queue<float> leftBuffer = new();
        private readonly Queue<float> rightBuffer = new();
        private readonly int blockSize = 512; // Size for Process/Retrieve calls
        private readonly float[] leftInput;
        private readonly float[] rightInput;
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

            stretcher = new RubberBandStretcherStereo(
                waveFormat.SampleRate,
                RubberBandStretcher.Options.ProcessRealTime |
                RubberBandStretcher.Options.FormantPreserved |
                RubberBandStretcher.Options.PitchHighConsistency |
                RubberBandStretcher.Options.SmoothingOn |
                RubberBandStretcher.Options.StretchElastic |
                RubberBandStretcher.Options.TransientsSmooth);

            stretcher.SetTimeRatio(TimeRatio);
            stretcher.SetPitchScale(PitchRatio);

            leftInput = new float[blockSize];
            rightInput = new float[blockSize];
            sourceBuffer = new float[blockSize * 2]; // Stereo
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int samplesWritten = 0;

            while (samplesWritten < count)
            {
                if (leftBuffer.Count == 0 || rightBuffer.Count == 0)
                {
                    int read = sourceProvider.Read(sourceBuffer, 0, sourceBuffer.Length);
                    if (read == 0) break;

                    int stereoFrames = read / 2;

                    for (int i = 0; i < stereoFrames; i++)
                    {
                        leftInput[i] = sourceBuffer[i * 2];
                        rightInput[i] = sourceBuffer[i * 2 + 1];
                    }

                    stretcher.SetTimeRatio(TimeRatio);
                    stretcher.SetPitchScale(PitchRatio);
                    stretcher.Process(leftInput, rightInput, (uint)stereoFrames, false);

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

                if (leftBuffer.Count == 0 || rightBuffer.Count == 0) break;

                buffer[offset + samplesWritten++] = leftBuffer.Dequeue();
                buffer[offset + samplesWritten++] = rightBuffer.Dequeue();
            }

            return samplesWritten;
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            int samplesNeeded = count / 4; // Float = 4 byte
            float[] temp = new float[samplesNeeded];

            int read = Read(temp, 0, samplesNeeded);
            Buffer.BlockCopy(temp, 0, buffer, offset, read * 4);
            return read * 4;
        }
    }
}