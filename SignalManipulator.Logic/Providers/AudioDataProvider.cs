using NAudio.Wave;
using SignalManipulator.Logic.Models;
using SignalManipulator.Logic.AudioMath;
using System;

namespace SignalManipulator.Logic.Providers
{
    public class AudioDataProvider : ISampleProvider, IWaveProvider
    {
        private readonly IWaveProvider source;

        public event Action<byte[]> OnBytes;
        public event Action<float[]> OnSamples;
        public event Action<WaveformFrame> WaveformReady;
        public event Action<FFTFrame> FFTReady;

        public WaveFormat WaveFormat => source.WaveFormat;
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

            WaveformFrame frame = new WaveformFrame(samples);
            WaveformReady?.Invoke(frame);

            if (EnableSpectrum)
            {
                var (magnitudes, freqs) = FFTCalculator.CalculateMagnitudeSpectrum(frame.DoubleMono, WaveFormat.SampleRate);
                FFTReady?.Invoke(new FFTFrame(freqs, magnitudes));
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