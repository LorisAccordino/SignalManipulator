using SignalManipulator.Logic.Data;
using SignalManipulator.Logic.Data.Channels;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.Tests.Logic.Data
{
    [ExcludeFromCodeCoverage]
    public class WaveformSliceTests
    {
        [Fact]
        public void Stereo_IsReturnedCorrectly()
        {
            float[] stereo = [0.1f, -0.1f];
            var slice = new WaveformSlice(stereo);

            Assert.Same(stereo, slice.Stereo);
        }

        [Fact]
        public void DoubleStereo_IsCorrect()
        {
            float[] stereo = [1.0f, 2.0f];
            var slice = new WaveformSlice(stereo);

            Assert.Equal([1.0, 2.0], slice.DoubleStereo, new DoubleComparer(1e-6));
        }

        [Fact]
        public void Mono_IsComputedCorrectly()
        {
            float[] stereo = [0.4f, 0.6f, 0.1f, 0.1f]; // Mono = [(0.4+0.6)/2, (0.1+0.1)/2] = [0.5, 0.1]
            var slice = new WaveformSlice(stereo);

            Assert.Equal([0.5f, 0.1f], slice.Mono, new FloatComparer(1e-6f));
        }

        [Fact]
        public void DoubleMono_IsCorrect()
        {
            var slice = new WaveformSlice([0.3f, 0.5f, 0.2f, 0.2f]);
            double[] expected = [(0.3 + 0.5) / 2.0, (0.2 + 0.2) / 2.0];

            Assert.Equal(expected, slice.DoubleMono, new DoubleComparer(1e-6));
        }

        [Fact]
        public void SplitStereo_SplitsCorrectly()
        {
            var slice = new WaveformSlice([1f, 2f, 3f, 4f]); // Left = [1,3], Right = [2,4]
            var (left, right) = slice.SplitStereo;

            Assert.Equal([1f, 3f], left);
            Assert.Equal([2f, 4f], right);
        }

        [Fact]
        public void DoubleSplitStereo_IsCorrect()
        {
            var slice = new WaveformSlice([1f, 2f, 3f, 4f]); // Double Left = [1,3], Double Right = [2,4]
            var (left, right) = slice.DoubleSplitStereo;

            Assert.Equal([1.0, 3.0], left, new DoubleComparer(1e-6));
            Assert.Equal([2.0, 4.0], right, new DoubleComparer(1e-6));
        }


        [Theory]
        [InlineData(AudioChannel.Stereo)]
        [InlineData(AudioChannel.Left)]
        [InlineData(AudioChannel.Right)]
        [InlineData(AudioChannel.Mono)]
        public void Get_ReturnsSameDataAsTryGet(AudioChannel channel)
        {
            var slice = new WaveformSlice([1f, 2f, 3f, 4f]);

            // Get() uses TryGet internally but ignores the bool, returns data array (or empty)
            var getResult = slice.Get(channel);

            // TryGet returns bool and out data
            bool tryGetResult = slice.TryGet(channel, out double[] tryGetData);

            Assert.Equal(tryGetData, getResult);

            if (channel == AudioChannel.Mid || channel == AudioChannel.Side)
            {
                // For these channels, data should be empty and TryGet returns false
                Assert.False(tryGetResult);
                Assert.Empty(tryGetData);
            }
            else
            {
                // For valid channels, TryGet returns true and data is not empty
                Assert.True(tryGetResult);
                Assert.NotEmpty(tryGetData);
            }
        }

        [Theory]
        [InlineData(AudioChannel.Stereo)]
        [InlineData(AudioChannel.Left)]
        [InlineData(AudioChannel.Right)]
        [InlineData(AudioChannel.Mono)]
        public void TryGet_KnownChannels_ReturnsTrue(AudioChannel channel)
        {
            var slice = new WaveformSlice([1f, 2f, 3f, 4f]);
            bool result = slice.TryGet(channel, out var data);

            Assert.True(result);
            Assert.NotEmpty(data);
        }

        [Theory]
        [InlineData(AudioChannel.Mid)]
        [InlineData(AudioChannel.Side)]
        public void TryGet_UnknownChannels_ReturnsFalse(AudioChannel channel)
        {
            var slice = new WaveformSlice([1f, 2f, 3f, 4f]);
            bool result = slice.TryGet(channel, out var data);

            Assert.False(result);
            Assert.Empty(data);
        }

        [Fact]
        public void GetOrThrow_InvalidChannel_Throws()
        {
            var slice = new WaveformSlice([1f, 2f, 3f, 4f]);
            Assert.Throws<InvalidOperationException>(() => slice.GetOrThrow(AudioChannel.Mid));
        }

        [Theory]
        [InlineData(AudioChannel.Stereo)]
        [InlineData(AudioChannel.Left)]
        [InlineData(AudioChannel.Right)]
        [InlineData(AudioChannel.Mono)]
        public void GetOrThrow_ValidChannel_ReturnsData(AudioChannel channel)
        {
            var slice = new WaveformSlice([1f, 2f, 3f, 4f]);

            var data = slice.GetOrThrow(channel);

            Assert.NotNull(data);
            Assert.NotEmpty(data);
        }

        [Fact]
        public void AvailableChannels_ContainsAll()
        {
            var slice = new WaveformSlice([1f, 2f, 3f, 4f]);
            var channels = slice.AvailableChannels.ToList();

            Assert.Contains(AudioChannel.Stereo, channels);
            Assert.Contains(AudioChannel.Left, channels);
            Assert.Contains(AudioChannel.Right, channels);
            Assert.Contains(AudioChannel.Mono, channels);
        }

        [Fact]
        public void HasChannel_ReturnsExpectedResults()
        {
            var slice = new WaveformSlice([1f, 2f, 3f, 4f]);

            Assert.True(slice.HasChannel(AudioChannel.Stereo));
            Assert.True(slice.HasChannel(AudioChannel.Left));
            Assert.True(slice.HasChannel(AudioChannel.Right));
            Assert.True(slice.HasChannel(AudioChannel.Mono));
            Assert.False(slice.HasChannel(AudioChannel.Mid));
        }

        private class DoubleComparer(double tolerance) : IEqualityComparer<double>
        {
            public bool Equals(double x, double y) => Math.Abs(x - y) < tolerance;
            public int GetHashCode(double obj) => obj.GetHashCode();
        }

        private class FloatComparer(float tolerance) : IEqualityComparer<float>
        {
            public bool Equals(float x, float y) => Math.Abs(x - y) < tolerance;
            public int GetHashCode(float obj) => obj.GetHashCode();
        }
    }

    [ExcludeFromCodeCoverage]
    public class FFTSliceTests
    {
        [Fact]
        public void Constructor_WaveformSlice_CreatesProperties()
        {
            // Stereo test data
            float[] stereoSamples = [1, 2, 3, 4];
            int sampleRate = 44100;

            var waveform = new WaveformSlice(stereoSamples);
            var fftSlice = new FFTSlice(waveform, sampleRate);

            Assert.NotNull(fftSlice.Frequencies);
            Assert.NotNull(fftSlice.Stereo);
            Assert.NotNull(fftSlice.Left);
            Assert.NotNull(fftSlice.Right);
        }

        [Fact]
        public void Constructor_StereoSamples_CreatesEquivalentInstance()
        {
            float[] stereoSamples = [1, 2, 3, 4];
            int sampleRate = 44100;

            var fftSlice = new FFTSlice(stereoSamples, sampleRate);

            Assert.NotNull(fftSlice.Frequencies);
            Assert.NotNull(fftSlice.Stereo);
            Assert.NotNull(fftSlice.Left);
            Assert.NotNull(fftSlice.Right);
        }

        [Fact]
        public void Constructor_LeftRightSamples_CreatesEquivalentInstance()
        {
            float[] left = [1, 3];
            float[] right = [2, 4];
            int sampleRate = 44100;

            var fftSlice = new FFTSlice(left, right, sampleRate);

            Assert.NotNull(fftSlice.Frequencies);
            Assert.NotNull(fftSlice.Stereo);
            Assert.NotNull(fftSlice.Left);
            Assert.NotNull(fftSlice.Right);
        }

        [Theory]
        [InlineData(AudioChannel.Stereo)]
        [InlineData(AudioChannel.Left)]
        [InlineData(AudioChannel.Right)]
        public void TryGet_ValidChannel_ReturnsTrueAndData(AudioChannel channel)
        {
            var fftSlice = CreateValidFFTSlice();

            bool result = fftSlice.TryGet(channel, out double[] data);

            Assert.True(result);
            Assert.NotNull(data);
            Assert.NotEmpty(data);
        }

        [Theory]
        [InlineData(AudioChannel.Mid)]
        [InlineData(AudioChannel.Side)]
        [InlineData(AudioChannel.Mono)]
        public void TryGet_InvalidChannel_ReturnsFalseAndEmptyArray(AudioChannel channel)
        {
            var fftSlice = CreateValidFFTSlice();

            bool result = fftSlice.TryGet(channel, out double[] data);

            Assert.False(result);
            Assert.NotNull(data);
            Assert.Empty(data);
        }

        [Theory]
        [InlineData(AudioChannel.Stereo)]
        [InlineData(AudioChannel.Left)]
        [InlineData(AudioChannel.Right)]
        [InlineData(AudioChannel.Mid)]
        public void Get_ReturnsSameDataAsTryGet(AudioChannel channel)
        {
            var fftSlice = CreateValidFFTSlice();

            var getData = fftSlice.Get(channel);
            fftSlice.TryGet(channel, out var tryGetData);

            Assert.Equal(tryGetData, getData);

            if (channel == AudioChannel.Mid)
                Assert.Empty(getData);
            else
                Assert.NotEmpty(getData);
        }

        [Theory]
        [InlineData(AudioChannel.Stereo)]
        [InlineData(AudioChannel.Left)]
        [InlineData(AudioChannel.Right)]
        public void GetOrThrow_ValidChannel_ReturnsData(AudioChannel channel)
        {
            var fftSlice = CreateValidFFTSlice();

            var data = fftSlice.GetOrThrow(channel);

            Assert.NotNull(data);
            Assert.NotEmpty(data);
        }

        [Theory]
        [InlineData(AudioChannel.Mid)]
        [InlineData(AudioChannel.Side)]
        [InlineData(AudioChannel.Mono)]
        public void GetOrThrow_InvalidChannel_Throws(AudioChannel channel)
        {
            var fftSlice = CreateValidFFTSlice();

            Assert.Throws<InvalidOperationException>(() => fftSlice.GetOrThrow(channel));
        }

        [Fact]
        public void AvailableChannels_ReturnsCorrectChannels()
        {
            var fftSlice = CreateValidFFTSlice();

            var channels = fftSlice.AvailableChannels.ToList();

            Assert.Contains(AudioChannel.Stereo, channels);
            Assert.Contains(AudioChannel.Left, channels);
            Assert.Contains(AudioChannel.Right, channels);

            // It does not contain invalid channels
            Assert.DoesNotContain(AudioChannel.Mid, channels);
            Assert.DoesNotContain(AudioChannel.Mono, channels);
            Assert.DoesNotContain(AudioChannel.Side, channels);
        }

        [Theory]
        [InlineData(AudioChannel.Stereo, true)]
        [InlineData(AudioChannel.Left, true)]
        [InlineData(AudioChannel.Right, true)]
        [InlineData(AudioChannel.Mid, false)]
        [InlineData(AudioChannel.Side, false)]
        [InlineData(AudioChannel.Mono, false)]
        public void HasChannel_ReturnsCorrectly(AudioChannel channel, bool expected)
        {
            var fftSlice = CreateValidFFTSlice();

            bool result = fftSlice.HasChannel(channel);

            Assert.Equal(expected, result);
        }

        // Helper to create a FFTSlice with non-null data for Stereo, Left and Right
        private FFTSlice CreateValidFFTSlice()
        {
            var fftSlice = new FFTSlice([1f, 2f, 3f, 4f], 44100);
            return fftSlice;
        }
    }

    [ExcludeFromCodeCoverage]
    public class VolumeMetricsTests
    {
        // Helper: create a stereo array for testing
        private static float[] CreateStereoSamples()
        {
            // Stereo interleaved: Left, Right, Left, Right ...
            return [1f, 2f, 3f, 4f, 5f, 6f];
        }

        [Fact]
        public void Constructor_WithFloatArray_CreatesWaveform()
        {
            var samples = CreateStereoSamples();
            var metrics = new VolumeMetrics(samples);

            Assert.NotNull(metrics.Waveform);
            Assert.Equal(samples, metrics.Waveform.Stereo);
        }

        [Fact]
        public void Constructor_WithWaveformSlice_SetsWaveform()
        {
            var samples = CreateStereoSamples();
            var waveform = new WaveformSlice(samples);
            var metrics = new VolumeMetrics(waveform);

            Assert.Same(waveform, metrics.Waveform);
        }

        [Fact]
        public void RMS_Properties_ReturnCorrectValues()
        {
            var samples = new float[] { 1f, -1f, 1f, -1f }; // Simple stereo with constant abs 1
            var metrics = new VolumeMetrics(samples);

            // RMS for constant 1 or -1 is 1
            Assert.Equal(1.0, metrics.StereoRMS, 6);
            Assert.Equal(1.0, metrics.LeftRMS, 6);
            Assert.Equal(1.0, metrics.RightRMS, 6);
            Assert.Equal(0.0, metrics.MonoRMS, 6); // RMS is 0
        }

        [Fact]
        public void Peak_ReturnsCorrectValue()
        {
            var samples = new float[] { -5f, 2f, -3f, 4f };
            var metrics = new VolumeMetrics(samples);

            Assert.Equal(5.0, metrics.Peak, 6);
        }

        [Fact]
        public void Loudness_ComputesLogarithm()
        {
            var samples = new float[] { 0f, 0f, 0f, 0f };
            var metrics = new VolumeMetrics(samples);

            // StereoRMS is zero, so loudness ~ 20*log10(1e-9) = -180 dB approx
            double expected = 20 * Math.Log10(1e-9);
            Assert.Equal(expected, metrics.Loudness, 6);
        }

        [Fact]
        public void Mid_CalculatesCorrectRMS()
        {
            var samples = new float[] { 1f, 3f, 1f, 5f }; // Left = [1,1], Right = [3,5]
            var metrics = new VolumeMetrics(samples);

            // mid = (L+R)/2 = [(1+3)/2, (1+5)/2] = [2, 3]
            // RMS = sqrt((2^2 + 3^2)/2) = sqrt((4 + 9)/2) = sqrt(6.5) ~ 2.549509
            Assert.InRange(metrics.Mid, 2.549, 2.55);
        }

        [Fact]
        public void Side_CalculatesCorrectRMS()
        {
            var samples = new float[] { 1f, 3f, 1f, 5f }; // Left = [1,1], Right = [3,5]
            var metrics = new VolumeMetrics(samples);

            // side = (L-R)/2 = [(1-3)/2, (1-5)/2] = [-1, -2]
            // RMS = sqrt((1^2 + 2^2)/2) = sqrt((1 + 4)/2) = sqrt(2.5) ~ 1.581139
            Assert.InRange(metrics.Side, 1.58, 1.59);
        }

        [Fact]
        public void Properties_AreCached()
        {
            var samples = new float[] { 1f, -1f, 1f, -1f };
            var metrics = new VolumeMetrics(samples);

            // Access each property twice to test caching (should not throw or change)
            var stereo1 = metrics.StereoRMS;
            var stereo2 = metrics.StereoRMS;
            Assert.Equal(stereo1, stereo2);

            var left1 = metrics.LeftRMS;
            var left2 = metrics.LeftRMS;
            Assert.Equal(left1, left2);

            var right1 = metrics.RightRMS;
            var right2 = metrics.RightRMS;
            Assert.Equal(right1, right2);

            var mono1 = metrics.MonoRMS;
            var mono2 = metrics.MonoRMS;
            Assert.Equal(mono1, mono2);

            var peak1 = metrics.Peak;
            var peak2 = metrics.Peak;
            Assert.Equal(peak1, peak2);

            var loud1 = metrics.Loudness;
            var loud2 = metrics.Loudness;
            Assert.Equal(loud1, loud2);

            var mid1 = metrics.Mid;
            var mid2 = metrics.Mid;
            Assert.Equal(mid1, mid2);

            var side1 = metrics.Side;
            var side2 = metrics.Side;
            Assert.Equal(side1, side2);
        }
    }

    [ExcludeFromCodeCoverage]
    public class AnalyzedAudioSliceTests
    {
        [Fact]
        public void Constructor_WithStereoSamples_CreatesValidComponents()
        {
            // Arrange
            float[] stereoSamples = [1.0f, -1.0f, 0.5f, -0.5f];
            int sampleRate = 44100;

            // Act
            var slice = new AnalyzedAudioSlice(stereoSamples, sampleRate);

            // Assert
            Assert.NotNull(slice.Waveform);
            Assert.NotNull(slice.FFT);
            Assert.NotNull(slice.Volume);

            Assert.Equal(stereoSamples, slice.Waveform.Stereo);
        }

        [Fact]
        public void Constructor_WithWaveformOnly_CreatesFftAndVolume()
        {
            // Arrange
            float[] stereoSamples = [0.2f, -0.2f, 0.3f, -0.3f];
            var waveform = new WaveformSlice(stereoSamples);
            int sampleRate = 48000;

            // Act
            var slice = new AnalyzedAudioSlice(waveform, sampleRate);

            // Assert
            Assert.Same(waveform, slice.Waveform);
            Assert.NotNull(slice.FFT);
            Assert.NotNull(slice.Volume);
        }

        [Fact]
        public void Constructor_WithAllComponents_AssignsCorrectly()
        {
            // Arrange
            var waveform = new WaveformSlice([0.1f, 0.2f, 0.3f, 0.4f]);
            var fft = new FFTSlice(waveform, 44100);
            var volume = new VolumeMetrics(waveform);

            // Act
            var slice = new AnalyzedAudioSlice(waveform, fft, volume);

            // Assert
            Assert.Same(waveform, slice.Waveform);
            Assert.Same(fft, slice.FFT);
            Assert.Same(volume, slice.Volume);
        }
    }
}