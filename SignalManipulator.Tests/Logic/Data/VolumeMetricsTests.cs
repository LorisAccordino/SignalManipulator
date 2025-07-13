using SignalManipulator.Logic.Data.Channels;
using SignalManipulator.Logic.Data;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.Tests.Logic.Data
{
    [ExcludeFromCodeCoverage]
    public class VolumeMetricsTests
    {
        private float[] CreateTestStereoSamples()
        {
            // L = [1, 3], R = [2, 4] → Stereo interleaved: [1,2,3,4]
            return [1f, 2f, 3f, 4f];
        }

        [Fact]
        public void Constructor_CreatesWaveform()
        {
            var samples = CreateTestStereoSamples();
            var metrics = new VolumeMetrics(samples);

            Assert.NotNull(metrics.Waveform);
            Assert.Equal(samples, metrics.Waveform.Stereo);
        }

        [Fact]
        public void RMS_Values_AreCorrect()
        {
            var samples = CreateTestStereoSamples();
            var metrics = new VolumeMetrics(samples);

            var stereo = metrics.RMS[AudioChannel.Stereo];
            var left = metrics.RMS[AudioChannel.Left];
            var right = metrics.RMS[AudioChannel.Right];
            var mono = metrics.RMS[AudioChannel.Mono];
            var mid = metrics.RMS[AudioChannel.Mid];
            var side = metrics.RMS[AudioChannel.Side];

            Assert.True(stereo > 0);
            Assert.True(left > 0);
            Assert.True(right > 0);
            Assert.True(mono > 0);
            Assert.True(mid > 0);
            Assert.True(side >= 0);

            // Mid = Mono
            Assert.Equal(mono, mid, precision: 6);
        }

        [Fact]
        public void RMS_CachesCorrectly()
        {
            var samples = CreateTestStereoSamples();
            var metrics = new VolumeMetrics(samples);

            var first = metrics.RMS[AudioChannel.Right];
            var second = metrics.RMS[AudioChannel.Right];

            Assert.Equal(first, second);
        }

        [Fact]
        public void Loudness_IsCalculatedCorrectly()
        {
            var samples = CreateTestStereoSamples();
            var metrics = new VolumeMetrics(samples);

            double rms = metrics.RMS[AudioChannel.Stereo];
            double loudness = metrics.Loudness;

            double expected = 20 * Math.Log10(rms + 1e-9);
            Assert.Equal(expected, loudness, precision: 6);
        }

        [Fact]
        public void Loudness_IsCached()
        {
            var samples = CreateTestStereoSamples();
            var metrics = new VolumeMetrics(samples);

            var first = metrics.Loudness;
            var second = metrics.Loudness;

            Assert.Equal(first, second);
        }

        [Fact]
        public void Peak_IsMaximumAbsoluteStereoSample()
        {
            var samples = new float[] { -1.5f, 0.2f, 0.9f, -0.4f };
            var metrics = new VolumeMetrics(samples);

            var expected = samples.Max(Math.Abs);
            Assert.Equal(expected, metrics.Peak, precision: 6);
        }

        [Fact]
        public void Peak_IsCached()
        {
            var samples = CreateTestStereoSamples();
            var metrics = new VolumeMetrics(samples);

            var first = metrics.Peak;
            var second = metrics.Peak;

            Assert.Equal(first, second);
        }

        [Fact]
        public void RMS_ReportsAllExpectedChannels()
        {
            var samples = CreateTestStereoSamples();
            var metrics = new VolumeMetrics(samples);

            var channels = metrics.RMS.AvailableChannels.ToArray();

            Assert.Contains(AudioChannel.Stereo, channels);
            Assert.Contains(AudioChannel.Left, channels);
            Assert.Contains(AudioChannel.Right, channels);
            Assert.Contains(AudioChannel.Mono, channels);
            Assert.Contains(AudioChannel.Mid, channels);
            Assert.Contains(AudioChannel.Side, channels);
        }

        [Fact]
        public void RMS_ThrowsIfChannelInvalid()
        {
            var samples = CreateTestStereoSamples();
            var metrics = new VolumeMetrics(samples);
            Assert.Throws<InvalidOperationException>(() => metrics.RMS.GetOrThrow(AudioChannel.None));
        }

        [Fact]
        public void RMS_HasChannel_ReturnsExpectedValues()
        {
            var samples = CreateTestStereoSamples();
            var metrics = new VolumeMetrics(samples);

            // These should be there
            Assert.True(metrics.RMS.HasChannel(AudioChannel.Stereo));
            Assert.True(metrics.RMS.HasChannel(AudioChannel.Left));
            Assert.True(metrics.RMS.HasChannel(AudioChannel.Right));
            Assert.True(metrics.RMS.HasChannel(AudioChannel.Mono));
            Assert.True(metrics.RMS.HasChannel(AudioChannel.Mid));
            Assert.True(metrics.RMS.HasChannel(AudioChannel.Side));

            // This should not be there
            Assert.False(metrics.RMS.HasChannel(AudioChannel.None));
        }
    }
}