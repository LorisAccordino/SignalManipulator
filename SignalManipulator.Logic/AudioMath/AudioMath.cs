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


        public static float[] ToMono(this float[] stereo)
        {
            float[] mono = new float[stereo.Length / 2];
            for (int i = 0; i < mono.Length; i++)
            {
                mono[i] = (stereo[i * 2] + stereo[i * 2 + 1]) / 2.0f;
            }
            return mono;
        }

        public static double[] ToMono(this double[] stereo)
        {
            double[] mono = new double[stereo.Length / 2];
            for (int i = 0; i < mono.Length; i++)
            {
                mono[i] = (stereo[i * 2] + stereo[i * 2 + 1]) / 2.0f;
            }
            return mono;
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


        public static void CombineStereo(this (float[] Left, float[] Right) channels, float[] stereo)
        {
            CombineStereo(stereo, channels.Left, channels.Right, stereo.Length / 2);
        }

        public static void CombineStereo(this float[] stereo, float[] left, float[] right)
        {
            CombineStereo(left, right, stereo, stereo.Length / 2);
        }

        public static void CombineStereo(this float[] stereo, float[] left, float[] right, int length)
        {
            if (length <= 0 || stereo.Length < length * 2)
                throw new ArgumentException(nameof(length));

            for (int i = 0, j = 0; i < length; i++, j += 2)
            {
                stereo[j] = left[i];
                stereo[j + 1] = right[i];
            }
        }


        public static float[] ToStereo(this (float[] Left, float[] Right) channels)
        {
            int length = Math.Min(channels.Left.Length, channels.Right.Length);
            float[] stereo = new float[length * 2];
            channels.CombineStereo(stereo);
            return stereo;
        }
    }

    public static class MiscMath
    {
        public static double ExaggerateRms(this double x, double power = 0.3)
        {
            return Math.Clamp(Math.Pow(Math.Clamp(x, 0, 1), power), 0, 1);
        }

        public static double LogNormalize(this double x, double min = 0.01)
        {
            x = Math.Max(x, min);
            return Math.Log10(x) / Math.Log10(1);
        }

        public static double SoftLimit(this double value, double limit, double softness = 10)
        {
            if (value <= limit)
                return value;

            double weightValue = Math.Exp(-value * softness / limit);
            double weightLimit = Math.Exp(-limit * softness / limit);

            return (value * weightValue + limit * weightLimit) / (weightValue + weightLimit);
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

    public static class BufferConversions
    {
        public static float[] AsFloats(this byte[] bytes)
        {
            if (bytes.Length % 4 != 0)
                throw new ArgumentException("Byte array length must be a multiple of 4");

            return MemoryMarshal.Cast<byte, float>(bytes).ToArray();
        }

        public static void AsFloats(this byte[] bytes, float[] floats)
        {
            if (bytes.Length % 4 != 0)
                throw new ArgumentException("Byte array length must be a multiple of 4");

            MemoryMarshal.Cast<byte, float>(bytes).CopyTo(floats);
        }

        public static byte[] AsBytes(this float[] floats)
        {
            return MemoryMarshal.AsBytes(floats.AsSpan()).ToArray();
        }

        public static void AsBytes(this float[] floats, byte[] bytes)
        {
            MemoryMarshal.AsBytes(floats.AsSpan()).CopyTo(bytes);
        }

        public static void CopyToFloats(this byte[] bytes, float[] floats, int floatOffset = 0, int floatCount = -1)
        {
            if (floatCount < 0) floatCount = floats.Length - floatOffset;
            if (bytes.Length < floatCount * 4)
                throw new ArgumentException("Not enough bytes to copy the requested floats");

            var floatSpan = MemoryMarshal.Cast<byte, float>(bytes.AsSpan(0, floatCount * 4));
            floatSpan.CopyTo(floats.AsSpan(floatOffset, floatCount));
        }

        public static void CopyToBytes(this float[] floats, byte[] bytes, int byteOffset = 0, int byteCount = -1)
        {
            if (byteOffset % 4 != 0)
                throw new ArgumentException("byteOffset must be a multiple of 4");

            if (byteCount < 0)
                byteCount = bytes.Length - byteOffset;

            if (byteCount % 4 != 0)
                throw new ArgumentException("byteCount must be a multiple of 4");

            int floatCount = byteCount / 4;
            if (floatCount > floats.Length)
                throw new ArgumentException("Not enough floats to copy");

            var floatSpan = floats.AsSpan(0, floatCount);
            var byteSpan = MemoryMarshal.AsBytes(floatSpan);
            byteSpan.CopyTo(bytes.AsSpan(byteOffset, byteCount));
        }

        public static void Copy(this float[] source, int sourceOffset, float[] dest, int destOffset, int count)
        {
            source.AsSpan(sourceOffset, count).CopyTo(dest.AsSpan(destOffset, count));
        }

        public static void Copy(this double[] source, int sourceOffset, double[] dest, int destOffset, int count)
        {
            source.AsSpan(sourceOffset, count).CopyTo(dest.AsSpan(destOffset, count));
        }

        public static void Copy(this byte[] source, int sourceOffset, byte[] dest, int destOffset, int count)
        {
            source.AsSpan(sourceOffset, count).CopyTo(dest.AsSpan(destOffset, count));
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

        public static double SoftMax(this double[] values, double softness = 10)
        {
            if (values.Length == 0)
                return 0;

            double max = values.Max();
            if (max == 0)
                return 0;

            double sumExp = 0;
            foreach (var v in values)
                sumExp += Math.Exp(v * softness / max); // Normalized

            double weightedSum = 0;
            foreach (var v in values)
                weightedSum += v * Math.Exp(v * softness / max);

            return weightedSum / sumExp;
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