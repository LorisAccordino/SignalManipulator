using SignalManipulator.Logic.AudioMath.Scaling.Curves;

namespace SignalManipulator.Logic.AudioMath.Scaling
{
    public class NonLinearScaleMapper : BaseScaleMapper
    {
        public INonLinearCurve Curve { get; set; } = new LinearCurve();
        private readonly int resolution;

        public NonLinearScaleMapper(double realMin, double realMax, double precision)
            : base(realMin, realMax, precision)
        {
            resolution = (int)Math.Round((realMax - realMin) / precision);
        }

        public override int ToControlUnits(double realValue)
        {
            double normalized = ScalingHelper.InverseLerp(realValue, RealMin, RealMax);
            double curved = Curve.Forward(normalized);
            curved = Math.Clamp(curved, 0.0, 1.0);
            return (int)Math.Round(curved * resolution);
        }

        public override int[] ToControlUnits(double[] realValues)
        {
            int[] input = new int[realValues.Length];
            for (int i = 0; i < realValues.Length; i++)
                input[i] = ToControlUnits(realValues[i]);
            return input;
        }

        public override double ToRealValue(double realValue)
        {
            double normalized = ScalingHelper.InverseLerp(realValue, RealMin, RealMax);
            double curved = Curve.Forward(normalized);
            curved = Math.Clamp(curved, 0.0, 1.0);
            return ScalingHelper.Lerp(curved, RealMin, RealMax);
        }

        public override double[] ToRealValues(double[] realValues)
        {
            double[] output = new double[realValues.Length];
            for (int i = 0; i < realValues.Length; i++)
                output[i] = ToRealValue(realValues[i]);
            return output;
        }

        public override double ToRealValue(int controlUnits)
        {
            double normalized = (double)controlUnits / resolution;
            double curved = Curve.Inverse(normalized);
            curved = Math.Clamp(curved, 0.0, 1.0);
            double realValue = ScalingHelper.Lerp(curved, RealMin, RealMax);
            return ScalingHelper.SnapToPrecision(realValue, Precision);
        }
    }
}