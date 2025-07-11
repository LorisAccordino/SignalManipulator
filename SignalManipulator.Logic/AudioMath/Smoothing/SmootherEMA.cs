namespace SignalManipulator.Logic.AudioMath.Smoothing
{
    public class SmootherEMA : Smoother
    {
        private double alpha;
        private double[] previous = [];

        public SmootherEMA(double alpha)
        {
            if (alpha < 0 || alpha > 1)
                throw new ArgumentOutOfRangeException(nameof(alpha), "Alpha must be between 0 and 1");

            this.alpha = alpha;
        }

        public override void Set(double alpha) => this.alpha = alpha;

        public override double[] Smooth(double[] input)
        {
            if (previous.Length != input.Length)
                previous = (double[])input.Clone();

            double[] output = new double[input.Length];
            for (int i = 0; i < input.Length; i++)
                output[i] = alpha * previous[i] + (1 - alpha) * input[i];

            previous = output;
            return output;
        }
    }
}