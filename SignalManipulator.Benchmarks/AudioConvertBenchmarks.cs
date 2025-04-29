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

        private float[] floatLeft;
        private float[] floatRight;
        private double[] doubleLeft;
        private double[] doubleRight;

        [GlobalSetup]
        public void Setup()
        {
            floatData = Enumerable.Range(0, 44100).Select(i => (float)Math.Sin(i * 2 * Math.PI / 44100)).ToArray();
            doubleData = floatData.Select(f => (double)f).ToArray();
            stereoFloat = new float[floatData.Length * 2];
            floatLeft = new float[floatData.Length];
            floatRight = new float[floatData.Length];
            doubleLeft = new double[floatData.Length];
            doubleRight = new double[floatData.Length];
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
        [Benchmark] public void SplitStereoFloat() => floatData.SplitStereo(floatLeft, floatRight);
        [Benchmark] public void SplitStereoDouble() => doubleData.SplitStereo(doubleLeft, doubleRight);
        [Benchmark] public float[] ToMono() => stereoFloat.ToMono();
    }
}