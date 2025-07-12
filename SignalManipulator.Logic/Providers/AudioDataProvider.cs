using NAudio.Wave;
using SignalManipulator.Logic.AudioMath;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Data;
using SignalManipulator.Logic.Utils;

namespace SignalManipulator.Logic.Providers
{
    public class AudioDataProvider : ISampleProvider, IWaveProvider
    {
        private CircularBuffer<float> fftBuffer = new(AudioEngine.CurrentFFTSize * 2);
        private readonly IWaveProvider source;

        public event EventHandler<byte[]>? OnBytes;
        public event EventHandler<float[]>? OnSamples;
        public event EventHandler<AnalyzedAudioSlice>? AudioDataReady;

        public WaveFormat WaveFormat => source.WaveFormat;

        public AudioDataProvider(ISampleProvider source)
            : this(source.ToWaveProvider()) { }

        public AudioDataProvider(IWaveProvider source)
        {
            this.source = source;
        }

        private void EnsureFFTBufferCapacity()
        {
            int required = AudioEngine.CurrentFFTSize * 2;
            if (fftBuffer.Capacity != required)
                fftBuffer.Capacity = required;
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            EnsureFFTBufferCapacity();

            int read = source.Read(buffer, offset, count);
            OnBytes?.Invoke(this, buffer[..read]);

            float[] samples = buffer.AsFloats();
            OnSamples?.Invoke(this, samples);

            fftBuffer.AddRange(samples);

            if (AudioDataReady != null)
            {
                var data = new AnalyzedAudioSlice(fftBuffer.ToArray(), WaveFormat.SampleRate);
                AudioDataReady.Invoke(this, data);
            }

            return read;
        }

        public int Read(float[] samples, int offset, int count)
        {
            byte[] buffer = new byte[count * 4];
            int read = Read(buffer, 0, count * 4);
            buffer.CopyToFloats(samples, offset, read / 4);
            return read / 4;
        }
    }
}