using BenchmarkDotNet.Attributes;
using SignalManipulator.Logic.AudioMath;
using System;
using System.Linq;

namespace SignalManipulator.Benchmarks
{
    [MemoryDiagnoser] // Mostra allocazioni RAM
    public class AudioConvertBenchmarks
    {
        private float[] floatData;
        private double[] doubleData;
        private byte[] byteData;
        private float[] stereoFloat;

        [GlobalSetup]
        public void Setup()
        {
            floatData = Enumerable.Range(0, 44100).Select(i => (float)Math.Sin(i * 2 * Math.PI / 44100)).ToArray();
            doubleData = floatData.Select(f => (double)f).ToArray();
            stereoFloat = new float[floatData.Length * 2];
            for (int i = 0; i < floatData.Length; i++)
            {
                stereoFloat[i * 2] = floatData[i];
                stereoFloat[i * 2 + 1] = floatData[i];
            }
            byteData = floatData.AsBytes();
        }

        [Benchmark] public double[] ToDouble() => floatData.ToDouble();
        [Benchmark] public float[] ToFloat() => doubleData.ToFloat();
        [Benchmark] public float[] AsFloats() => byteData.AsFloats();
        [Benchmark] public byte[] AsBytes() => floatData.AsBytes();
        [Benchmark] public (float[] left, float[] right) SplitStereoFloat() => stereoFloat.SplitStereo();
        [Benchmark] public (double[] left, double[] right) SplitStereoDouble() => doubleData.Concat(doubleData).ToArray().SplitStereo();
        [Benchmark] public float[] ToMono() => stereoFloat.ToMono();
    }
}