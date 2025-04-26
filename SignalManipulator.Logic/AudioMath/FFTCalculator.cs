using MathNet.Numerics.IntegralTransforms;
using System.Linq;
using System.Numerics;

namespace SignalManipulator.Logic.AudioMath
{
    public static class FFTCalculator
    {
        public static double[] CalculateFFT(double[] samples, int sampleRate, out double[] frequencies)
        {
            // Convert to complex (imaginary = 0)
            Complex[] complexSamples = samples.Select(s => new Complex(s, 0)).ToArray();

            // Apply FFT in-place
            Fourier.Forward(complexSamples, FourierOptions.Matlab);

            int n = complexSamples.Length;
            int half = n / 2; // An half of the data is sufficient for the real spectrum

            // Magnitude (ampltiude for each frequency)
            double[] magnitudes = new double[half];
            frequencies = new double[half];

            for (int i = 0; i < half; i++)
            {
                magnitudes[i] = complexSamples[i].Magnitude;
                frequencies[i] = i * sampleRate / (double)n;
            }

            return magnitudes;
        }
    }
}