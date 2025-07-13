using NAudio.Wave;
using SignalManipulator.Logic.Providers;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace SignalManipulator.Tests.Logic.Providers
{
    [ExcludeFromCodeCoverage]
    public class RubberBandProviderTests
    {
        private class TestStereoProvider : ISampleProvider
        {
            private readonly WaveFormat waveFormat;
            private int sampleIndex;

            public TestStereoProvider(int sampleRate = 44100)
            {
                waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, 2);
            }

            public WaveFormat WaveFormat => waveFormat;

            public int Read(float[] buffer, int offset, int count)
            {
                int samplesToWrite = Math.Min(count, 1000);

                for (int i = 0; i < samplesToWrite; i += 2)
                {
                    buffer[offset + i] = (float)(Math.Sin(2 * Math.PI * 440 * sampleIndex / waveFormat.SampleRate));     
                    buffer[offset + i + 1] = (float)(Math.Cos(2 * Math.PI * 440 * sampleIndex / waveFormat.SampleRate));
                    sampleIndex++;
                }
                return samplesToWrite;
            }
        }

        [Fact]
        public void Constructor_ThrowsIfNotStereo()
        {
            var monoProvider = new SineWaveProviderMono();

            Assert.Throws<NotSupportedException>(() => new RubberBandProvider(monoProvider));
        }

        [Fact]
        public void Constructor_WithIWaveProvider_Works()
        {
            var rawProvider = new DefaultAudioProvider();
            var stereoProvider = rawProvider.ToSampleProvider();
            var waveProvider = stereoProvider.ToWaveProvider();
            var rubber = new RubberBandProvider(waveProvider);
            Assert.NotNull(rubber);
        }

        [Fact]
        public void WaveFormat_ReturnsCorrectFormat()
        {
            var provider = new TestStereoProvider();
            var rubber = new RubberBandProvider(provider);

            Assert.Equal(provider.WaveFormat.SampleRate, rubber.WaveFormat.SampleRate);
            Assert.Equal(2, rubber.WaveFormat.Channels);
        }

        [Fact]
        public void Read_ReturnsSamples_FromSourceProvider()
        {
            var source = new TestStereoProvider();
            var provider = new RubberBandProvider(source);

            float[] buffer = new float[512 * 2]; // stereo samples count
            int read = provider.Read(buffer, 0, buffer.Length);

            Assert.True(read > 0);
            Assert.Contains(buffer, s => s != 0f);
        }

        [Fact]
        public void ReadByte_ReturnsValidBytes()
        {
            var rubber = new RubberBandProvider(new TestStereoProvider());

            byte[] buffer = new byte[1024];
            int read = rubber.Read(buffer, 0, buffer.Length);

            Assert.True(read > 0);
            Assert.Contains(buffer, b => b != 0);
        }

        private class EmptyStereoProvider : ISampleProvider
        {
            public WaveFormat WaveFormat => WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);
            public int Read(float[] buffer, int offset, int count) => 0; // Always EOF
        }

        [Fact]
        public void ReadFloat_HandlesEOF()
        {
            var rubber = new RubberBandProvider(new EmptyStereoProvider());

            float[] buffer = new float[256];
            int read = rubber.Read(buffer, 0, buffer.Length);

            Assert.Equal(0, read); // Simula EOF
        }

        [Fact]
        public void ReadFloat_FillsOutputBuffer()
        {
            var rubber = new RubberBandProvider(new TestStereoProvider());

            float[] buffer = new float[512];
            int read = rubber.Read(buffer, 0, buffer.Length);

            Assert.True(read > 0);
            Assert.Contains(buffer, b => b != 0f);
        }


        [Fact]
        public void Reset_ResetsInternalStateWithoutErrors()
        {
            var source = new TestStereoProvider();
            var provider = new RubberBandProvider(source);

            provider.Reset();

            float[] buffer = new float[512 * 2];
            int read = provider.Read(buffer, 0, buffer.Length);

            Assert.True(read > 0);
        }

        private class SineWaveProviderMono : ISampleProvider
        {
            private readonly WaveFormat waveFormat;

            public SineWaveProviderMono()
            {
                waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 1);
            }

            public WaveFormat WaveFormat => waveFormat;

            public int Read(float[] buffer, int offset, int count)
            {
                for (int i = 0; i < count; i++)
                    buffer[offset + i] = 0f;
                return count;
            }
        }

        [Fact]
        public void ReadFloat_EndsWhenLeftOrRightBufferEmpty()
        {
            var rubber = new RubberBandProvider((ISampleProvider)null); // Not very safe :,)

            float[] buffer = new float[256];
            int read = rubber.Read(buffer, 0, buffer.Length);
            Assert.Equal(0, read); // It should reach immediately EOF
        }
    }
}