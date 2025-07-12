namespace SignalManipulator.Logic.AudioMath
{
    public static class StereoConversions
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


        public static float[] ToMid(this float[] stereo) => ToMono(stereo);
        public static float[] ToMono(this float[] stereo)
        {
            float[] mono = new float[stereo.Length / 2];
            for (int i = 0; i < mono.Length; i++)
            {
                mono[i] = (stereo[i * 2] + stereo[i * 2 + 1]) / 2.0f;
            }
            return mono;
        }

        public static double[] ToMid(this double[] stereo) => ToMono(stereo);
        public static double[] ToMono(this double[] stereo)
        {
            double[] mono = new double[stereo.Length / 2];
            for (int i = 0; i < mono.Length; i++)
            {
                mono[i] = (stereo[i * 2] + stereo[i * 2 + 1]) / 2.0f;
            }
            return mono;
        }


        public static float[] ToSide(this float[] stereo)
        {
            float[] mono = new float[stereo.Length / 2];
            for (int i = 0; i < mono.Length; i++)
            {
                mono[i] = (stereo[i * 2] - stereo[i * 2 + 1]) / 2.0f;
            }
            return mono;
        }

        public static double[] ToSide(this double[] stereo)
        {
            double[] mono = new double[stereo.Length / 2];
            for (int i = 0; i < mono.Length; i++)
            {
                mono[i] = (stereo[i * 2] - stereo[i * 2 + 1]) / 2.0f;
            }
            return mono;
        }


        public static void SplitStereo(this float[] stereo, float[] left, float[] right)
            => SplitStereo(stereo, left, right, stereo.Length / 2);

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
            => SplitStereo(stereo, left, right, stereo.Length / 2);

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
            => CombineStereo(stereo, channels.Left, channels.Right, stereo.Length / 2);

        public static void CombineStereo(this float[] stereo, float[] left, float[] right)
            => CombineStereo(stereo, left, right, stereo.Length / 2);

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
}