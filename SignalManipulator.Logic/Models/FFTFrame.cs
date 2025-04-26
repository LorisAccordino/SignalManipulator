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
            double[] freqs, magnitudes;
            magnitudes = FFTCalculator.CalculateFFT(samples, sampleRate, out freqs);
            return new FFTFrame(freqs, magnitudes);
        }
    }
}