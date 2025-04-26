using NAudio.Wave;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Utils;
using System;

namespace SignalManipulator.Logic.Providers
{
    public class AudioDataProvider : ISampleProvider, IWaveProvider
    {
        private readonly IWaveProvider source;

        public event Action<byte[]> OnBytes;
        public event Action<float[]> OnSamples;
        public event Action<AudioFrame> FrameReady;
        public event Action<FrequencySpectrum> SpectrumReady;

        public bool EnableSpectrum { get; set; } = false;

        public AudioDataProvider(ISampleProvider source) : this(source.ToWaveProvider()) { }
        public AudioDataProvider(IWaveProvider source)
        {
            this.source = source;
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            int read = source.Read(buffer, offset, count);
            OnBytes?.Invoke(buffer);

            float[] samples = buffer.AsFloats();
            OnSamples?.Invoke(samples);

            AudioFrame frame = new AudioFrame(samples);
            FrameReady?.Invoke(frame);

            if (EnableSpectrum)
            {
                double[] magnitudes = AudioMathHelper.CalculateFFT(frame.DoubleStereo, SampleRate, out double[] freqs);
                SpectrumReady?.Invoke(new FrequencySpectrum(freqs, magnitudes));
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

        public WaveFormat WaveFormat => source.WaveFormat;
        public int SampleRate => WaveFormat.SampleRate;
    }
}