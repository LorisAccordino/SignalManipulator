namespace SignalManipulator.Logic.AudioMath
{
    public class ArrayMath
    {
        public static void Copy(float[] source, int sourceOffset, float[] dest, int destOffset, int count)
        {
            Array.Copy(source, sourceOffset, dest, destOffset, count);
        }

        public static void AddInPlace(float[] dest, float[] addend)
        {
            int len = Math.Min(dest.Length, addend.Length);
            for (int i = 0; i < len; i++)
                dest[i] += addend[i];
        }

        public static void SubtractInPlace(float[] dest, float[] subtrahend)
        {
            int len = System.Math.Min(dest.Length, subtrahend.Length);
            for (int i = 0; i < len; i++)
                dest[i] -= subtrahend[i];
        }

        public static void MultiplyInPlace(float[] array, float scalar)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] *= scalar;
        }

        public static float Sum(float[] array)
        {
            float sum = 0f;
            for (int i = 0; i < array.Length; i++)
                sum += array[i];
            return sum;
        }

        public static float Mean(float[] array)
        {
            if (array.Length == 0) return 0f;
            return Sum(array) / array.Length;
        }

        public static float Max(float[] array)
        {
            if (array.Length == 0) return float.MinValue;
            float max = array[0];
            for (int i = 1; i < array.Length; i++)
                if (array[i] > max)
                    max = array[i];
            return max;
        }

        public static float Min(float[] array)
        {
            if (array.Length == 0) return float.MaxValue;
            float min = array[0];
            for (int i = 1; i < array.Length; i++)
                if (array[i] < min)
                    min = array[i];
            return min;
        }

        public static void Normalize(float[] array)
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
                MultiplyInPlace(array, invMax);
            }
        }

        // Useful for RMS
        public static void SqrtInPlace(float[] array)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = (float)Math.Sqrt(array[i]);
        }

        public static float RMS(float[] array)
        {
            if (array.Length == 0) return 0f;
            double sumSquares = 0;
            for (int i = 0; i < array.Length; i++)
                sumSquares += array[i] * array[i];
            return (float)Math.Sqrt(sumSquares / array.Length);
        }

        public static void FadeIn(float[] array)
        {
            int len = array.Length;
            for (int i = 0; i < len; i++)
            {
                float gain = (float)i / (len - 1);
                array[i] *= gain;
            }
        }

        public static void FadeOut(float[] array)
        {
            int len = array.Length;
            for (int i = 0; i < len; i++)
            {
                float gain = 1f - ((float)i / (len - 1));
                array[i] *= gain;
            }
        }

        public static void ClampInPlace(float[] array, float min, float max)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < min) array[i] = min;
                else if (array[i] > max) array[i] = max;
            }
        }
    }
}