using SignalManipulator.Logic.AudioMath;

namespace SignalManipulator.Logic.Models
{
    public class FFTFrame
    {
        public double[] Frequencies { get; }
        public double[] Magnitudes { get; }

        public FFTFrame(double[] frequencies, double[] magnitudes)
        {
            Frequencies = frequencies;
            Magnitudes = magnitudes;
        }

        public static FFTFrame FromFFT(double[] samples, int sampleRate)
        {
            var (magnitudes, freqs) = FFTCalculator.CalculateMagnitudeSpectrum(samples, sampleRate);
            return new FFTFrame(freqs, magnitudes);
        }
    }
}