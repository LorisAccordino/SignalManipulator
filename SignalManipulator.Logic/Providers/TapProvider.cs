using NAudio.Wave;
using SignalManipulator.Logic.Utils;
using System;

namespace SignalManipulator.Logic.Providers
{
    public class TapProvider : ISampleProvider, IWaveProvider
    {
        private readonly IWaveProvider source;

        public event Action<byte[]> OnBytes;
        public event Action<float[]> OnSamples;

        public TapProvider(ISampleProvider source) : this(source.ToWaveProvider()) { }
        public TapProvider(IWaveProvider source)
        {
            this.source = source;
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            int read = source.Read(buffer, offset, count);
            OnBytes?.Invoke(buffer);
            OnSamples?.Invoke(buffer.AsFloats());
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
    }
}