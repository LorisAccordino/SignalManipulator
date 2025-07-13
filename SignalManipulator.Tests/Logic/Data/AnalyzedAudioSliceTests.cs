using SignalManipulator.Logic.Data;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.Tests.Logic.Data
{
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