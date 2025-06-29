using NAudio.Wave;
using SignalManipulator.Logic.Models;
using SignalManipulator.Logic.AudioMath;
using RubberBandSharp;

namespace SignalManipulator.Logic.Providers
{
    public class AudioDataProvider : ISampleProvider, IWaveProvider
    {
        private readonly IWaveProvider source;

        public event EventHandler<byte[]> OnBytes;
        public event EventHandler<float[]> OnSamples;
        public event EventHandler<WaveformFrame> WaveformReady;
        public event EventHandler<FFTFrame> FFTReady;

        public WaveFormat WaveFormat => source.WaveFormat;
        public bool EnableSpectrum { get; set; } = false;

        public AudioDataProvider(ISampleProvider source) : this(source.ToWaveProvider()) { }
        public AudioDataProvider(IWaveProvider source)
        {
            this.source = source;


            int sampleRate = source.WaveFormat.SampleRate;

            stretcher = new RubberBandStretcherStereo(sampleRate,
        RubberBandStretcher.Options.ProcessRealTime |
        RubberBandStretcher.Options.WindowShort |
        RubberBandStretcher.Options.FormantPreserved |
        RubberBandStretcher.Options.PitchHighConsistency);

            //stretcher.SetPitchScale(0.5f); // Cambia il pitch di +20%
            stretcher.SetTimeRatio(0.5);
        }

        private RubberBandStretcherStereo stretcher;

        public int Read(byte[] buffer, int offset, int count)
        {
            int read = source.Read(buffer, offset, count);
            OnBytes?.Invoke(this, buffer);

            float[] samples = buffer.AsFloats();
            OnSamples?.Invoke(this, samples);




            // --- PITCH SHIFT ---

            int sampleCount = samples.Length;
            int stereoSamples = sampleCount / 2;

            // Ignora se non è stereo o dispari
            if (sampleCount % 2 == 0)
            {
                Span<float> left = stackalloc float[stereoSamples];
                Span<float> right = stackalloc float[stereoSamples];

                // Split stereo
                for (int i = 0, j = 0; i < stereoSamples; i++, j += 2)
                {
                    left[i] = samples[j];
                    right[i] = samples[j + 1];
                }

                // Processa
                stretcher.Process(left, right, (uint)stereoSamples, false);

                // Retrieve pitch-shifted samples
                int available = stretcher.Available();

                if (available >= stereoSamples)
                {
                    Span<float> outLeft = stackalloc float[stereoSamples];
                    Span<float> outRight = stackalloc float[stereoSamples];

                    stretcher.Retrieve(outLeft, outRight, (uint)stereoSamples);

                    // Ricompone in stereo
                    for (int i = 0, j = 0; i < stereoSamples; i++, j += 2)
                    {
                        samples[j] = outLeft[i];
                        samples[j + 1] = outRight[i];
                    }

                    // Ricompatta in byte[]
                    byte[] outBytes = samples.AsBytes();
                    Array.Copy(outBytes, 0, buffer, offset, read);
                }
            }




            WaveformFrame frame = new WaveformFrame(samples);
            WaveformReady?.Invoke(this, frame);

            if (EnableSpectrum)
            {
                var (magnitudes, freqs) = FFTCalculator.CalculateMagnitudeSpectrum(frame.DoubleMono, WaveFormat.SampleRate);
                FFTReady?.Invoke(this, new FFTFrame(freqs, magnitudes));
            }

            return read;
        }

        public int Read(float[] samples, int offset, int count)
        {
            byte[] buffer = new byte[count * 4];
            int read = Read(buffer, offset * 4, count * 4);
            Array.Copy(buffer, samples, count * 4);
            return read;
        }
    }
}