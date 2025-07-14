using BenchmarkDotNet.Attributes;
using MathNet.Numerics.IntegralTransforms;
using System.Numerics;
using SignalManipulator.Logic.AudioMath.Objects;

namespace SignalManipulator.Benchmarks
{
    [MemoryDiagnoser]
    public class FFTBenchmarks
    {
        private double[] sampledSignal;
        private Complex[] complexSignal;
        private Complex[] spectrum;

        [Params(512, 1024, 4096, 16384)] // FFT of various sizes
        public int SampleSize;

        [GlobalSetup]
        public void Setup()
        {
            // Generate a test signal: sum of two sines
            sampledSignal = GenerateTestSignal(SampleSize);

            // CreateInstance the complex version
            complexSignal = sampledSignal.Select(s => new Complex(s, 0)).ToArray();

            // Pre-compute the FFT to use in the inverse one
            spectrum = (Complex[])complexSignal.Clone();
            Fourier.Forward(spectrum, FourierOptions.Matlab);
        }

        private double[] GenerateTestSignal(int length)
        {
            double[] signal = new double[length];
            double freq1 = 440.0; // 440 Hz
            double freq2 = 880.0; // 880 Hz
            double sampleRate = 44100.0;

            for (int i = 0; i < length; i++)
            {
                double t = i / sampleRate;
                signal[i] = Math.Sin(2 * Math.PI * freq1 * t) + 0.5 * Math.Sin(2 * Math.PI * freq2 * t);
            }

            return signal;
        }

        [Benchmark]
        public void ForwardFFT_Doubles()
        {
            var result = FFT.Forward(sampledSignal);
        }

        [Benchmark]
        public void ForwardFFT_Complex()
        {
            var result = FFT.Forward(complexSignal);
        }

        [Benchmark]
        public void InverseFFT()
        {
            var result = FFT.Inverse(spectrum);
        }

        [Benchmark]
        public void MagnitudeSpectrum()
        {
            var (magnitudes, frequencies) = FFT.CalculateMagnitudeSpectrum(sampledSignal, 44100);
        }
    }
}