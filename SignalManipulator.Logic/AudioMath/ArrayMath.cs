namespace SignalManipulator.Logic.AudioMath
{
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

        public static void Exaggerate(this double[] array, double power = 0.3, double max = 1.0)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = Math.Clamp(Math.Pow(Math.Clamp(array[i], 0, 1), power), 0, max);
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