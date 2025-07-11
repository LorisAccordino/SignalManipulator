namespace SignalManipulator.Logic.AudioMath
{
    public static class ArrayMath
    {
        // --- Clear ---
        public static void Clear(this float[] array) => Array.Clear(array);
        public static void Clear(this double[] array) => Array.Clear(array);

        public static void Clear(this float[] array, int index, int length) => Array.Clear(array, index, length);
        public static void Clear(this double[] array, int index, int length) => Array.Clear(array, index, length);


        // --- Add/Sub ---
        public static void Add(this float[] dest, float[] addend)
        {
            int len = Math.Min(dest.Length, addend.Length);
            var spanDest = dest.AsSpan(0, len);
            var spanAdd = addend.AsSpan(0, len);

            for (int i = 0; i < len; i++)
                spanDest[i] += spanAdd[i];
        }

        public static void Sub(this float[] dest, float[] subtrahend)
        {
            int len = Math.Min(dest.Length, subtrahend.Length);
            var spanDest = dest.AsSpan(0, len);
            var spanSub = subtrahend.AsSpan(0, len);

            for (int i = 0; i < len; i++)
                spanDest[i] -= spanSub[i];
        }


        // --- Scalar ops ---
        public static void Mul(this float[] array, float scalar)
        {
            var span = array.AsSpan();
            for (int i = 0; i < span.Length; i++)
                span[i] *= scalar;
        }

        public static void Div(this float[] array, float divisor)
        {
            if (divisor == 0f)
                throw new DivideByZeroException(nameof(divisor));

            var span = array.AsSpan();
            for (int i = 0; i < span.Length; i++)
                span[i] /= divisor;
        }


        // --- Normalize ---
        public static void Normalize(this float[] array)
        {
            if (array.Length == 0)
                return;

            float max = array.Max(MathF.Abs);
            if (max > 0f)
            {
                float invMax = 1f / max;
                array.Mul(invMax);
            }
        }


        // --- Fades ---
        public static void FadeIn(this float[] array)
        {
            int len = array.Length;
            if (len <= 1) return;

            for (int i = 0; i < len; i++)
                array[i] *= i / (float)(len - 1);
        }

        public static void FadeOut(this float[] array)
        {
            int len = array.Length;
            if (len <= 1) return;

            for (int i = 0; i < len; i++)
                array[i] *= 1f - i / (float)(len - 1);
        }


        // --- Clamp ---
        public static void Clamp(this float[] array, float min, float max)
        {
            var span = array.AsSpan();
            for (int i = 0; i < span.Length; i++)
                span[i] = Math.Clamp(span[i], min, max);
        }

        public static void Clamp(this double[] array, double min, double max)
        {
            var span = array.AsSpan();
            for (int i = 0; i < span.Length; i++)
                span[i] = Math.Clamp(span[i], min, max);
        }
    }
}