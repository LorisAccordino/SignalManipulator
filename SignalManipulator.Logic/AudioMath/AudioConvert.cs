using System;
using System.Runtime.InteropServices;

namespace SignalManipulator.Logic.AudioMath
{
    public static class AudioConvert
    {
        public static double[] ToDouble(this float[] input)
        {
            int n = input.Length;
            var output = new double[n];
            for (int i = 0; i < n; i++)
                output[i] = input[i];
            return output;
        }

        public static float[] ToFloat(this double[] input)
        {
            int n = input.Length;
            var output = new float[n];
            for (int i = 0; i < n; i++)
                output[i] = (float)input[i];
            return output;
        }

        public static float[] AsFloats(this byte[] bytes)
        {
            return MemoryMarshal.Cast<byte, float>(bytes).ToArray();
        }

        public static byte[] AsBytes(this float[] floats)
        {
            return MemoryMarshal.AsBytes(floats.AsSpan()).ToArray();
        }

        public static (float[] left, float[] right) SplitStereo(this float[] interleaved)
        {
            int sampleCount = interleaved.Length / 2;
            float[] left = new float[sampleCount];
            float[] right = new float[sampleCount];

            for (int i = 0; i < sampleCount; i++)
            {
                left[i] = interleaved[2 * i];
                right[i] = interleaved[2 * i + 1];
            }

            return (left, right);
        }

        public static (double[] left, double[] right) SplitStereo(this double[] interleaved)
        {
            (float[] left, float[] right) = interleaved.ToFloat().SplitStereo();
            return (left.ToDouble(), right.ToDouble());
        }

        public static float[] ToMono(this float[] stereo)
        {
            float[] mono = new float[stereo.Length / 2];
            for (int i = 0; i < mono.Length; i++)
            {
                mono[i] = (stereo[i * 2] + stereo[i * 2 + 1]) / 2.0f;
            }
            return mono;
        }

        public static float ToDecibels(this float linear) => (float)ToDecibels((double)linear);
        public static double ToDecibels(this double linear)
        {
            if (linear <= 0) return double.NegativeInfinity;
            return 20.0 * Math.Log10(linear);
        }

        public static float ToLinear(this float decibels) => (float)ToLinear((double)decibels);
        public static double ToLinear(this double decibels)
        {
            return Math.Pow(10.0, decibels / 20.0);
        }
    }
}