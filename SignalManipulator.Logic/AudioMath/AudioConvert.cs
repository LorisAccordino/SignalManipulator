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

        public static void SplitStereo(this float[] stereo, float[] left, float[] right)
        {
            SplitStereo(stereo, left, right, stereo.Length / 2);
        }

        public static void SplitStereo(this float[] stereo, float[] left, float[] right, int length)
        {
            if (length <= 0 || length > stereo.Length / 2) 
                throw new ArgumentException(nameof(length));

            for (int i = 0, j = 0; i < length; i++, j += 2)
            {
                left[i] = stereo[j];
                right[i] = stereo[j + 1];
            }
        }

        public static void SplitStereo(this double[] stereo, double[] left, double[] right)
        {
            SplitStereo(stereo, left, right, stereo.Length / 2);
        }

        public static void SplitStereo(this double[] stereo, double[] left, double[] right, int length)
        {
            if (length <= 0 || length > stereo.Length / 2)
                throw new ArgumentException(nameof(length));

            for (int i = 0, j = 0; i < length; i++, j += 2)
            {
                left[i] = stereo[j];
                right[i] = stereo[j + 1];
            }
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