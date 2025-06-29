using MathNet.Numerics.IntegralTransforms;
using System.Linq;
using System.Numerics;

namespace SignalManipulator.Logic.AudioMath
{
    public static class FFTCalculator
    {
        public static Complex[] Forward(double[] realSamples, FourierOptions options = FourierOptions.Matlab)
        {
            Complex[] complexSamples = realSamples.Select(s => new Complex(s, 0)).ToArray();
            Fourier.Forward(complexSamples, options);
            return complexSamples;
        }

        public static Complex[] Forward(Complex[] complexSamples, FourierOptions options = FourierOptions.Matlab)
        {
            var copy = (Complex[])complexSamples.Clone();
            Fourier.Forward(copy, options);
            return copy;
        }

        public static Complex[] Inverse(Complex[] spectrum, FourierOptions options = FourierOptions.Matlab)
        {
            var copy = (Complex[])spectrum.Clone();
            Fourier.Inverse(copy, options);
            return copy;
        }

        public static (double[] Magnitudes, double[] Frequencies) CalculateMagnitudeSpectrum(double[] realSamples, int sampleRate)
        {
            Complex[] spectrum = Forward(realSamples);
            int n = spectrum.Length;
            int half = n / 2;

            double[] magnitudes = new double[half];
            double[] frequencies = new double[half];

            for (int i = 0; i < half; i++)
            {
                magnitudes[i] = spectrum[i].Magnitude;
                frequencies[i] = i * sampleRate / (double)n;
            }

            return (magnitudes, frequencies);
        }
    }
}