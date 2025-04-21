namespace SignalManipulator.Logic.Utils
{
    public class FrequencySpectrum
    {
        public double[] Frequencies { get; }
        public double[] Magnitudes { get; }

        public FrequencySpectrum(double[] frequencies, double[] magnitudes)
        {
            Frequencies = frequencies;
            Magnitudes = magnitudes;
        }

        public static FrequencySpectrum FromFFT(double[] samples, int sampleRate)
        {
            double[] freqs, magnitudes;
            magnitudes = AudioMathHelper.CalculateFFT(samples, sampleRate, out freqs);
            return new FrequencySpectrum(freqs, magnitudes);
        }
    }
}