using System;

namespace SignalManipulator.UI.Helpers.Scaling
{
    public class LinearScaleMapper : BaseScaleMapper
    {
        public LinearScaleMapper(double realMin, double realMax, double precision) : base(realMin, realMax, precision) { }
        public override int ToControlUnits(double realValue)
        {
            return (int)Math.Round((realValue - realMin) / precision);
        }

        public override double ToRealValue(int controlUnits)
        {
            return realMin + controlUnits * precision;
        }
    }
}