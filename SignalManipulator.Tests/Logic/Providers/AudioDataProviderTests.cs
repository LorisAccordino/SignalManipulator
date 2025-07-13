using NAudio.Wave;
using SignalManipulator.Logic.Providers;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Data;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.Tests.Logic.Providers
{
    [ExcludeFromCodeCoverage]
    public class AudioDataProviderTests
    {
        private class DummyWaveProvider : IWaveProvider
        {
            private readonly byte[] data;
            private int position = 0;

            public WaveFormat WaveFormat { get; }

            public DummyWaveProvider(byte[] data, WaveFormat? format = null)
            {
                this.data = data;
                WaveFormat = format ?? WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);
            }

            public int Read(byte[] buffer, int offset, int count)
            {
                int remaining = data.Length - position;
                int toCopy = Math.Min(count, remaining);
                Array.Copy(data, position, buffer, offset, toCopy);
                position += toCopy;
                return toCopy;
            }
        }

        private byte[] CreateFloatBytes(float[] floats)
        {
            var buffer = new byte[floats.Length * 4];
            Buffer.BlockCopy(floats, 0, buffer, 0, buffer.Length);
            return buffer;
        }

        [Fact]
        public void WaveFormat_MatchesSource()
        {
            var dummy = new DummyWaveProvider(new byte[0]);
            var provider = new AudioDataProvider(dummy);

            Assert.Equal(dummy.WaveFormat, provider.WaveFormat);
        }

        [Fact]
        public void Read_Triggers_OnBytes_Event()
        {
            var floatSamples = new float[] { 0.1f, -0.2f, 0.3f, -0.4f };
            var byteData = CreateFloatBytes(floatSamples);

            var dummy = new DummyWaveProvider(byteData);
            var provider = new AudioDataProvider(dummy);

            byte[]? received = null;
            provider.OnBytes += (s, b) => received = b;

            byte[] buffer = new byte[byteData.Length];
            int read = provider.Read(buffer, 0, buffer.Length);

            Assert.NotNull(received);
            Assert.Equal(byteData[..read], received);
        }

        [Fact]
        public void Read_Triggers_OnSamples_Event()
        {
            var floatSamples = new float[] { 0.5f, -0.5f };
            var byteData = CreateFloatBytes(floatSamples);

            var dummy = new DummyWaveProvider(byteData);
            var provider = new AudioDataProvider(dummy);

            float[]? received = null;
            provider.OnSamples += (s, sps) => received = sps;

            byte[] buffer = new byte[byteData.Length];
            _ = provider.Read(buffer, 0, buffer.Length);

            Assert.NotNull(received);
            Assert.Equal(floatSamples, received);
        }

        [Fact]
        public void Read_Triggers_AudioDataReady_Event()
        {
            var floatSamples = new float[AudioEngine.CurrentFFTSize * 2];
            for (int i = 0; i < floatSamples.Length; i++)
                floatSamples[i] = i / 100f;

            var byteData = CreateFloatBytes(floatSamples);
            var dummy = new DummyWaveProvider(byteData);
            var provider = new AudioDataProvider(dummy);

            AnalyzedAudioSlice? slice = null;
            provider.AudioDataReady += (s, a) => slice = a;

            byte[] buffer = new byte[byteData.Length];
            _ = provider.Read(buffer, 0, buffer.Length);

            Assert.NotNull(slice);
            Assert.Equal(floatSamples.Length, slice!.Waveform.Stereo.Length);
        }

        [Fact]
        public void Read_FloatOverload_ConvertsCorrectly()
        {
            var floats = new float[] { 0.1f, 0.2f, 0.3f, 0.4f };
            var byteData = CreateFloatBytes(floats);

            var dummy = new DummyWaveProvider(byteData);
            var provider = new AudioDataProvider(dummy);

            var target = new float[4];
            int read = provider.Read(target, 0, 4);

            Assert.Equal(4, read);
            Assert.Equal(floats, target);
        }

        [Fact]
        public void FFTBuffer_Capacity_Adjusts_WhenFFTSizeChanges()
        {
            var oldSize = AudioEngine.CurrentFFTSize;

            var floatSamples = new float[4096]; // new FFT buffer = 4096
            var byteData = CreateFloatBytes(floatSamples);

            var dummy = new DummyWaveProvider(byteData);
            var provider = new AudioDataProvider(dummy);

            AudioEngine.CurrentFFTSize = 2048;

            byte[] buffer = new byte[byteData.Length];
            provider.Read(buffer, 0, buffer.Length);

            // If it didn't throw, the buffer capacity adjusted correctly.
            AudioEngine.CurrentFFTSize = oldSize; // Restore
        }
    }
}