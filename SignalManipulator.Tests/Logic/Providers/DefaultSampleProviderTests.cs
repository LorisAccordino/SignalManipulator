using SignalManipulator.Logic.Providers;
using SignalManipulator.Logic.Core;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.Tests.Logic.Providers
{
    [ExcludeFromCodeCoverage]
    public class DefaultSampleProviderTests
    {
        [Fact]
        public void WaveFormat_MatchesAudioEngineFormat()
        {
            var provider = new DefaultAudioProvider();

            Assert.Equal(AudioEngine.WAVE_FORMAT.SampleRate, provider.WaveFormat.SampleRate);
            Assert.Equal(AudioEngine.WAVE_FORMAT.Channels, provider.WaveFormat.Channels);
            Assert.Equal(AudioEngine.WAVE_FORMAT.Encoding, provider.WaveFormat.Encoding);
        }

        [Fact]
        public void Read_FillsBufferWithZeros_AndReturnsZero()
        {
            var provider = new DefaultAudioProvider();
            float[] buffer = new float[10];
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = 1f; // Fill with dummy data

            int read = provider.Read(buffer, 0, buffer.Length);

            Assert.Equal(0, read); // No samples read
            Assert.All(buffer, sample => Assert.Equal(0f, sample)); // All samples should be zeroed
        }

        [Fact]
        public void Read_Byte_FillsBufferWithZeros_AndReturnsZero()
        {
            var provider = new DefaultAudioProvider();
            byte[] buffer = new byte[10];
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = 0x01; // Fill with dummy data

            int read = provider.Read(buffer, 0, buffer.Length);

            Assert.Equal(0, read); // No samples read
            Assert.All(buffer, sample => Assert.Equal(0f, sample)); // All samples should be zeroed
        }

        [Fact]
        public void Empty_Instance_IsSingleton()
        {
            var instance1 = DefaultAudioProvider.Empty;
            var instance2 = DefaultAudioProvider.Empty;

            Assert.Same(instance1, instance2);
        }
    }
}