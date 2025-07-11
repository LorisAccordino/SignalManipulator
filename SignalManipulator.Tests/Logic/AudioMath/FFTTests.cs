using System.Numerics;
using SignalManipulator.Logic.AudioMath.Objects;
using MathNet.Numerics.IntegralTransforms;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.Tests.Logic.AudioMath
{
    [ExcludeFromCodeCoverage]
    public class FFTTests
    {
        [Fact]
        public void Forward_From_RealSamples_Returns_SymmetricSpectrum()
        {
            double[] signal = Enumerable.Repeat(1.0, 8).ToArray(); // DC signal
            Complex[] spectrum = FFT.Forward(signal, FourierOptions.Default);

            Assert.Equal(signal.Length, spectrum.Length);
            Assert.True(spectrum[0].Real > 0); // DC component
            Assert.All(spectrum.Skip(1), c => Assert.True(Math.Abs(c.Magnitude) < 1e-6));
        }

        [Fact]
        public void Forward_And_Inverse_Preserve_OriginalSignal()
        {
            double[] original = Enumerable.Range(0, 8).Select(i => Math.Sin(2 * Math.PI * i / 8)).ToArray();
            Complex[] spectrum = FFT.Forward(original);
            Complex[] restored = FFT.Inverse(spectrum);

            double[] reconstructed = restored.Select(c => c.Real).ToArray();

            for (int i = 0; i < original.Length; i++)
                Assert.Equal(original[i], reconstructed[i], 4); // Allow small numerical error
        }

        [Fact]
        public void Forward_From_ComplexArray_DoesNotModifyOriginal()
        {
            var data = new Complex[] { 1, 2, 3, 4 };
            var copy = data.ToArray();
            var result = FFT.Forward(data);

            Assert.Equal(copy, data); // original array must not be changed
            Assert.NotEqual(copy, result); // result must be different
        }

        [Fact]
        public void Inverse_DoesNotModifyOriginal()
        {
            var spectrum = new Complex[] { 1, 0, 0, 0 };
            var copy = spectrum.ToArray();
            var result = FFT.Inverse(spectrum);

            Assert.Equal(copy, spectrum); // original not modified
            Assert.NotEqual(copy, result); // result is new
        }

        [Fact]
        public void CalculateMagnitudeSpectrum_Returns_HalfLengthAndExpectedFreqs()
        {
            int sampleRate = 8000;
            double[] signal = new double[8];
            signal[1] = 1;

            var (magnitudes, frequencies) = FFT.CalculateMagnitudeSpectrum(signal, sampleRate);

            Assert.Equal(4, magnitudes.Length);
            Assert.Equal(4, frequencies.Length);
            Assert.Equal(0, frequencies[0]);
            Assert.Equal(sampleRate / 8.0, frequencies[1], 6);
        }

        [Fact]
        public void Magnitudes_Are_Positive()
        {
            double[] signal = new double[8];
            signal[3] = 1;

            var (magnitudes, _) = FFT.CalculateMagnitudeSpectrum(signal, 44100);
            Assert.All(magnitudes, mag => Assert.True(mag >= 0));
        }

        [Fact]
        public void ZeroSignal_HasZeroMagnitudes()
        {
            double[] signal = new double[16];
            var (magnitudes, _) = FFT.CalculateMagnitudeSpectrum(signal, 44100);
            Assert.All(magnitudes, m => Assert.True(m < 1e-10));
        }
    }
}