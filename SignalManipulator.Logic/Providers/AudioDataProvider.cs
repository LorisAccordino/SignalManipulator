using NAudio.Wave;
using SignalManipulator.Logic.AudioMath;
using SignalManipulator.Logic.Utils;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Data;

namespace SignalManipulator.Logic.Providers
{
    public class AudioDataProvider : ISampleProvider, IWaveProvider
    {
        private CircularBuffer<float> fftBuffer = new CircularBuffer<float>(AudioEngine.CurrentFFTSize);
        private readonly IWaveProvider source;

        public event EventHandler<byte[]>? OnBytes;
        public event EventHandler<float[]>? OnSamples;
        public event EventHandler<AnalyzedAudioSlice>? AudioFrameReady;
        public WaveFormat WaveFormat => source.WaveFormat;

        public AudioDataProvider(ISampleProvider source) : this(source.ToWaveProvider()) { }
        public AudioDataProvider(IWaveProvider source)
        {
            this.source = source;
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            if (fftBuffer.Capacity != AudioEngine.CurrentFFTSize * 2)
            {
                fftBuffer.Capacity = AudioEngine.CurrentFFTSize * 2;
                //fftBuffer.Clear();
            }

            // Bytes
            int read = source.Read(buffer, offset, count);
            OnBytes?.Invoke(this, buffer);

            // Samples
            float[] samples = buffer.AsFloats();
            OnSamples?.Invoke(this, samples);

            // Add samples to the FFT fftBuffer
            fftBuffer.AddRange(samples);

            // Audio frame delivering
            AudioFrameReady?.Invoke(this, new AnalyzedAudioSlice(fftBuffer.ToArray(), WaveFormat.SampleRate));

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