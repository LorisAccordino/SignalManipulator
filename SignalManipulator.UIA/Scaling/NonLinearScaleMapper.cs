using SignalManipulator.UI.Helpers;
using SignalManipulator.UI.Scaling.Curves;
using System;

namespace SignalManipulator.UI.Scaling
{
    public class NonLinearScaleMapper : BaseScaleMapper
    {
        protected INonLinearCurve curve = new LinearCurve();
        private readonly int resolution;

        public NonLinearScaleMapper(double realMin, double realMax, double precision)
            : base(realMin, realMax, precision)
        {
            resolution = (int)Math.Round((realMax - realMin) / precision);
        }

        public void SetCurve(INonLinearCurve curve) => this.curve = curve;

        public override int ToControlUnits(double realValue)
        {
            double normalized = MathHelper.InverseLerp(realValue, RealMin, RealMax);
            double curved = curve.Forward(normalized);
            return (int)Math.Round(curved * resolution);
        }

        public override double ToRealValue(int controlUnits)
        {
            double normalized = (double)controlUnits / resolution;
            double curved = curve.Inverse(normalized);
            double realValue = MathHelper.Lerp(curved, RealMin, RealMax);
            return MathHelper.SnapToPrecision(realValue, Precision);
        }
    }
}