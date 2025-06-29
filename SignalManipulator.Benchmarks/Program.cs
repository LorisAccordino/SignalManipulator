using BenchmarkDotNet.Running;

namespace SignalManipulator.Benchmarks
{
    public class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<AudioConvertBenchmarks>();
            BenchmarkRunner.Run<FFTBenchmarks>();
        }
    }
}