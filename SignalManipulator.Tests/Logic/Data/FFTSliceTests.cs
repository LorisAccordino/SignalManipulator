using SignalManipulator.Logic.Data.Channels;
using SignalManipulator.Logic.Data;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.Tests.Logic.Data
{
    [ExcludeFromCodeCoverage]
    public class FFTSliceTests
    {
        private float[] CreateTestStereoSamples()
        {
            // Simple L: [1, 3], R: [2, 4]
            return [1f, 2f, 3f, 4f];
        }

        private const int SampleRate = 44100;

        [Fact]
        public void Constructor_StoresWaveformCorrectly()
        {
            var stereo = CreateTestStereoSamples();
            var slice = new FFTSlice(stereo, SampleRate);

            Assert.NotNull(slice.Waveform);
            Assert.Equal(stereo, slice.Waveform.Stereo);
        }

        [Fact]
        public void Constructor_FromLeftRight_CreatesCorrectStereo()
        {
            float[] left = [1f, 3f];
            float[] right = [2f, 4f];

            var slice = new FFTSlice(left, right, SampleRate);
            var expectedStereo = new float[] { 1f, 2f, 3f, 4f }; // Interleaved L0, R0, L1, R1
            Assert.Equal(expectedStereo, slice.Waveform.Stereo);

            var stereoMagnitudes = slice.Magnitudes.Get(AudioChannel.Stereo);
            Assert.NotNull(stereoMagnitudes);
            Assert.True(stereoMagnitudes.Length > 0);
        }

        [Fact]
        public void Frequencies_AreConsistentAndPrecomputed()
        {
            var stereo = CreateTestStereoSamples();
            var slice = new FFTSlice(stereo, SampleRate);

            var freqs = slice.Frequencies;

            Assert.NotNull(freqs);
            Assert.True(freqs.Length > 0);
            Assert.All(freqs, f => Assert.True(f >= 0));
        }

        [Fact]
        public void Magnitudes_Stereo_IsConsistentWithPrecalculated()
        {
            var stereo = CreateTestStereoSamples();
            var slice = new FFTSlice(stereo, SampleRate);

            var stereoMagnitudes = slice.Magnitudes.Get(AudioChannel.Stereo);

            Assert.NotNull(stereoMagnitudes);
            Assert.True(stereoMagnitudes.Length > 0);
        }

        [Fact]
        public void Magnitudes_Mid_EqualsMono()
        {
            var stereo = CreateTestStereoSamples();
            var slice = new FFTSlice(stereo, SampleRate);

            var mid = slice.Magnitudes.Get(AudioChannel.Mid);
            var mono = slice.Magnitudes.Get(AudioChannel.Mono);

            Assert.Equal(mono, mid);
        }

        [Fact]
        public void Magnitudes_LeftRightSideMono_AreCalculated()
        {
            var stereo = CreateTestStereoSamples();
            var slice = new FFTSlice(stereo, SampleRate);

            var left = slice.Magnitudes.Get(AudioChannel.Left);
            var right = slice.Magnitudes.Get(AudioChannel.Right);
            var mono = slice.Magnitudes.Get(AudioChannel.Mono);
            var side = slice.Magnitudes.Get(AudioChannel.Side);

            Assert.All(new[] { left, right, mono, side }, m =>
            {
                Assert.NotNull(m);
                Assert.True(m.Length > 0);
            });
        }

        [Fact]
        public void Magnitudes_CachesCorrectly()
        {
            var stereo = CreateTestStereoSamples();
            var slice = new FFTSlice(stereo, SampleRate);

            var first = slice.Magnitudes.Get(AudioChannel.Right);
            var second = slice.Magnitudes.Get(AudioChannel.Right);

            // Should be the same reference due to caching
            Assert.Same(first, second);
        }

        [Fact]
        public void Magnitudes_ThrowsIfChannelInvalid()
        {
            var stereo = CreateTestStereoSamples();
            var slice = new FFTSlice(stereo, SampleRate);

            Assert.Throws<InvalidOperationException>(() =>
            {
                slice.Magnitudes.GetOrThrow(AudioChannel.None);
            });
        }

        [Fact]
        public void Magnitudes_ReportsAvailableChannels()
        {
            var stereo = CreateTestStereoSamples();
            var slice = new FFTSlice(stereo, SampleRate);

            var available = slice.Magnitudes.AvailableChannels.ToArray();

            Assert.Contains(AudioChannel.Stereo, available);
            Assert.Contains(AudioChannel.Left, available);
            Assert.Contains(AudioChannel.Right, available);
            Assert.Contains(AudioChannel.Mono, available);
            Assert.Contains(AudioChannel.Mid, available);
            Assert.Contains(AudioChannel.Side, available);
        }
    }
}