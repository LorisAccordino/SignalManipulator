using System.Runtime.InteropServices;

namespace SignalManipulator.Logic.AudioMath
{
    public static class AudioMath
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

    public static class ArrayMath
    {
        public static void Clear(this float[] array) => Clear(array, 0, array.Length);
        public static void Clear(this float[] array, int index, int length)
        {
            Array.Clear(array, index, length);
        }

        public static void Clear(this double[] array) => Clear(array, 0, array.Length);
        public static void Clear(this double[] array, int index, int length)
        {
            Array.Clear(array, index, length);
        }

        public static void Add(this float[] dest, float[] addend)
        {
            int len = Math.Min(dest.Length, addend.Length);
            for (int i = 0; i < len; i++)
                dest[i] += addend[i];
        }

        public static void Sub(this float[] dest, float[] subtrahend)
        {
            int len = Math.Min(dest.Length, subtrahend.Length);
            for (int i = 0; i < len; i++)
                dest[i] -= subtrahend[i];
        }

        public static void Mul(this float[] array, float scalar)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] *= scalar;
        }

        public static void Div(this float[] array, float div)
        {
            if (div == 0) throw new ArithmeticException(nameof(div));

            for (int i = 0; i < array.Length; i++)
                array[i] /= div;
        }

        public static void Normalize(this float[] array)
        {
            float max = 0f;
            for (int i = 0; i < array.Length; i++)
            {
                float absVal = Math.Abs(array[i]);
                if (absVal > max)
                    max = absVal;
            }
            if (max > 0)
            {
                float invMax = 1f / max;
                Mul(array, invMax);
            }
        }

        public static void FadeIn(this float[] array)
        {
            int len = array.Length;
            for (int i = 0; i < len; i++)
            {
                float gain = (float)i / (len - 1);
                array[i] *= gain;
            }
        }

        public static void FadeOut(this float[] array)
        {
            int len = array.Length;
            for (int i = 0; i < len; i++)
            {
                float gain = 1f - ((float)i / (len - 1));
                array[i] *= gain;
            }
        }

        public static void Clamp(this float[] array, float min, float max)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < min) array[i] = min;
                else if (array[i] > max) array[i] = max;
            }
        }

        public static void Clamp(this double[] array, double min, double max)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < min) array[i] = min;
                else if (array[i] > max) array[i] = max;
            }
        }
    }
}