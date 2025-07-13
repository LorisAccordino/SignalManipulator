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
    }
}