using BenchmarkDotNet.Running;
using System.Diagnostics;
using System;
using SignalManipulator.Logic.AudioMath;
using System.Linq;

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