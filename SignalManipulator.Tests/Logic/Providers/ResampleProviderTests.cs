using NAudio.Wave;
using SignalManipulator.Logic.Providers;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.Tests.Logic.Providers
{
    [ExcludeFromCodeCoverage]
    public class ResampleSpeedProviderTests
    {
        private class SineWaveProvider : ISampleProvider
        {
            private readonly WaveFormat waveFormat;
            private int sample;

            public SineWaveProvider(int sampleRate = 44100)
            {
                waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, 1);
            }

            public WaveFormat WaveFormat => waveFormat;

            public int Read(float[] buffer, int offset, int count)
            {
                for (int i = 0; i < count; i++)
                {
                    buffer[offset + i] = (float)Math.Sin(2 * Math.PI * 440 * sample / waveFormat.SampleRate);
                    sample++;
                }
                return count;
            }
        }

        [Fact]
        public void Constructor_SetsInitialSpeedAndWaveFormat()
        {
            var source = new SineWaveProvider();
            var resampleProvider = new ResampleSpeedProvider(source, 1.0f);

            Assert.Equal(source.WaveFormat.SampleRate, resampleProvider.WaveFormat.SampleRate);
            Assert.Equal(1.0 / 1.0f, resampleProvider.SpeedRatio);
        }

        [Fact]
        public void SpeedRatio_Setter_ChangesSpeedRatioAndRebuilds()
        {
            var source = new SineWaveProvider();
            var resampleProvider = new ResampleSpeedProvider(source, 1.0f);

            double originalSpeed = resampleProvider.SpeedRatio;
            resampleProvider.SpeedRatio = 2.0;

            Assert.NotEqual(originalSpeed, resampleProvider.SpeedRatio);
            Assert.Equal(2.0, resampleProvider.SpeedRatio, 5);
        }

        [Fact]
        public void SpeedRatio_Setter_ThrowsOnInvalidValues()
        {
            var source = new SineWaveProvider();
            var resampleProvider = new ResampleSpeedProvider(source);

            Assert.Throws<ArgumentOutOfRangeException>(() => resampleProvider.SpeedRatio = 0);
            Assert.Throws<ArgumentOutOfRangeException>(() => resampleProvider.SpeedRatio = -1);
        }

        [Fact]
        public void Read_DelegatesToResampler()
        {
            var source = new SineWaveProvider();
            var resampleProvider = new ResampleSpeedProvider(source, 1.0f);

            float[] buffer = new float[1000];
            int read = resampleProvider.Read(buffer, 0, buffer.Length);

            Assert.Equal(buffer.Length, read);
            Assert.Contains(buffer, sample => sample != 0f);
        }

        [Fact]
        public void Reset_SetsPositionToZero_IfAudioFileReader()
        {
            var source = new SineWaveProvider();
            var resampleProvider = new ResampleSpeedProvider(source);

            Exception? ex = Record.Exception(resampleProvider.Reset);
            Assert.Null(ex);
        }

        [Fact]
        public void Reset_WithAudioFileReader_ResetsPosition()
        {
            // Arrange
            var reader = new AudioFileReader(CreateSilentWavFile());
            reader.Position = 1000; // move cursor
            var provider = new ResampleSpeedProvider(reader);

            // Act
            provider.Reset();

            // Assert
            Assert.Equal(0, reader.Position);
        }

        private class DummyWaveStream : WaveStream, ISampleProvider
        {
            public override WaveFormat WaveFormat => WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);
            public override long Length => 44100 * 4;
            public override long Position { get; set; }
            public int Read(float[] buffer, int offset, int count)
            {
                Array.Clear(buffer, offset, count);
                return count;
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                Array.Clear(buffer, offset, count);
                return count;
            }
        }

        [Fact]
        public void Reset_WithWaveStreamSampleProvider_ResetsPosition()
        {
            // Arrange
            var dummy = new DummyWaveStream();
            dummy.Position = 5000;
            var provider = new ResampleSpeedProvider(dummy);

            // Act
            provider.Reset();

            // Assert
            Assert.Equal(0, dummy.Position);
        }

        private class DummyUnsupportedProvider : ISampleProvider
        {
            public WaveFormat WaveFormat => WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);
            public int Read(float[] buffer, int offset, int count) => 0;
        }

        [Fact]
        public void Reset_WithUnsupportedSource_DoesNotThrow()
        {
            // Arrange
            var dummy = new DummyUnsupportedProvider();
            var provider = new ResampleSpeedProvider(dummy);

            // Act & Assert (no exception)
            var ex = Record.Exception(() => provider.Reset());
            Assert.Null(ex); // Ensure it doesn’t throw
        }

        // Helper to create an in-memory silent WAV file for testing
        private static string CreateSilentWavFile()
        {
            var path = Path.GetTempFileName();
            using var writer = new WaveFileWriter(path, WaveFormat.CreateIeeeFloatWaveFormat(44100, 2));
            float[] silence = new float[44100];
            writer.WriteSamples(silence, 0, silence.Length);
            writer.Flush();
            return path;
        }
    }
}