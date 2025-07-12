using SignalManipulator.Logic.Data;
using SignalManipulator.Logic.Data.Channels;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.Tests.Logic.Data
{
    [ExcludeFromCodeCoverage]
    public class WaveformSliceTests
    {
        // Test interleaved stereo array: L0, R0, L1, R1, ...
        private float[] CreateTestStereoSamples(int frames)
        {
            var samples = new float[frames * 2];
            for (int i = 0; i < frames; i++)
            {
                samples[2 * i] = i;         // Left channel increasing
                samples[2 * i + 1] = -i;    // Right channel decreasing (negativo)
            }
            return samples;
        }

        [Fact]
        public void Constructor_StoresStereoSamples()
        {
            var stereo = CreateTestStereoSamples(5);
            var slice = new WaveformSlice(stereo);

            Assert.Equal(stereo, slice.Stereo);
        }

        [Fact]
        public void FloatSamples_Stereo_ReturnsOriginalArray()
        {
            var stereo = CreateTestStereoSamples(3);
            var slice = new WaveformSlice(stereo);

            var result = slice.FloatSamples.Get(AudioChannel.Stereo);

            Assert.Equal(stereo, result);
        }

        [Fact]
        public void FloatSamples_LeftRight_AreCorrectlySplit()
        {
            var stereo = new float[] { 1f, 2f, 3f, 4f }; // L0=1, R0=2, L1=3, R1=4
            var slice = new WaveformSlice(stereo);

            var left = slice.FloatSamples.Get(AudioChannel.Left);
            var right = slice.FloatSamples.Get(AudioChannel.Right);

            Assert.Equal([1f, 3f], left);
            Assert.Equal([2f, 4f], right);
        }

        [Fact]
        public void FloatSamples_Mono_IsAverageOfLeftRight()
        {
            var stereo = new float[] { 1f, 3f, 5f, 7f }; // L0=1, R0=3, L1=5, R1=7
            var slice = new WaveformSlice(stereo);

            var mono = slice.FloatSamples.Get(AudioChannel.Mono);

            // Mono = (L + R) / 2 = [ (1+3)/2, (5+7)/2 ] = [2, 6]
            Assert.Equal([2f, 6f], mono);
        }

        [Fact]
        public void FloatSamples_Side_IsCorrectlyCalculated()
        {
            var stereo = new float[] { 3f, 1f, 7f, 5f }; // L0=3, R0=1, L1=7, R1=5
            var slice = new WaveformSlice(stereo);

            var side = slice.FloatSamples.Get(AudioChannel.Side);

            // Side = (L - R)/2 = [ (3-1)/2, (7-5)/2 ] = [1, 1]
            Assert.Equal([1f, 1f], side);
        }

        [Fact]
        public void FloatSamples_Mid_EqualsMono()
        {
            var stereo = new float[] { 2f, 4f, 6f, 8f };
            var slice = new WaveformSlice(stereo);

            var mid = slice.FloatSamples.Get(AudioChannel.Mid);
            var mono = slice.FloatSamples.Get(AudioChannel.Mono);

            Assert.Equal(mono, mid);
        }

        [Fact]
        public void DoubleSamples_AreCorrectConversionsOfFloatSamples()
        {
            var stereo = new float[] { 1f, 2f, 3f, 4f };
            var slice = new WaveformSlice(stereo);

            foreach (var channel in slice.FloatSamples.AvailableChannels)
            {
                var floatSamples = slice.FloatSamples.Get(channel);
                var doubleSamples = slice.DoubleSamples.Get(channel);

                Assert.Equal(floatSamples.Length, doubleSamples.Length);

                for (int i = 0; i < floatSamples.Length; i++)
                {
                    Assert.Equal(floatSamples[i], doubleSamples[i], 5); // precision 1e-5
                }
            }
        }

        [Fact]
        public void AvailableChannels_ReturnsAllExpectedChannels()
        {
            var stereo = CreateTestStereoSamples(2);
            var slice = new WaveformSlice(stereo);

            var floatChannels = slice.FloatSamples.AvailableChannels.ToArray();
            var doubleChannels = slice.DoubleSamples.AvailableChannels.ToArray();

            var expected = new[]
            {
            AudioChannel.Stereo,
            AudioChannel.Mono,
            AudioChannel.Left,
            AudioChannel.Right,
            AudioChannel.Mid,
            AudioChannel.Side
        };

            Assert.All(expected, ch => Assert.Contains(ch, floatChannels));
            Assert.All(expected, ch => Assert.Contains(ch, doubleChannels));
        }
    }

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

    [ExcludeFromCodeCoverage]
    public class AnalyzedAudioSliceTests
    {
        private float[] CreateTestStereoSamples() => [1f, 2f, 3f, 4f];
        private const int SampleRate = 44100;

        [Fact]
        public void Constructor_FromFloatArray_CreatesComponents()
        {
            var samples = CreateTestStereoSamples();
            var analyzed = new AnalyzedAudioSlice(samples, SampleRate);

            Assert.NotNull(analyzed.Waveform);
            Assert.NotNull(analyzed.FFT);
            Assert.NotNull(analyzed.Volume);

            Assert.Equal(samples, analyzed.Waveform.Stereo);
        }

        [Fact]
        public void Constructor_FromWaveform_CreatesFFTAndVolume()
        {
            var waveform = new WaveformSlice(CreateTestStereoSamples());
            var analyzed = new AnalyzedAudioSlice(waveform, SampleRate);

            Assert.Equal(waveform, analyzed.Waveform);
            Assert.NotNull(analyzed.FFT);
            Assert.NotNull(analyzed.Volume);
        }

        [Fact]
        public void Constructor_WithAllDependencies_AssignsCorrectly()
        {
            var waveform = new WaveformSlice(CreateTestStereoSamples());
            var fft = new FFTSlice(waveform, SampleRate);
            var volume = new VolumeMetrics(waveform);

            var analyzed = new AnalyzedAudioSlice(waveform, fft, volume);

            Assert.Same(waveform, analyzed.Waveform);
            Assert.Same(fft, analyzed.FFT);
            Assert.Same(volume, analyzed.Volume);
        }

        [Fact]
        public void FFT_UsesSameWaveformReference()
        {
            var waveform = new WaveformSlice(CreateTestStereoSamples());
            var analyzed = new AnalyzedAudioSlice(waveform, SampleRate);

            Assert.Same(waveform, analyzed.FFT.Waveform);
        }

        [Fact]
        public void Volume_UsesSameWaveformReference()
        {
            var waveform = new WaveformSlice(CreateTestStereoSamples());
            var analyzed = new AnalyzedAudioSlice(waveform, SampleRate);

            Assert.Same(waveform, analyzed.Volume.Waveform);
        }
    }
}