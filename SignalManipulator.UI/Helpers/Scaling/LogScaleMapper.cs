using System;

namespace SignalManipulator.UI.Helpers.Scaling
{
    public class LogScaleMapper : BaseScaleMapper
    {
        private double logMin, logMax;
        private const int RESOLUTION = 1000;

        public LogScaleMapper(double realMin, double realMax, double precision) : base(realMin, realMax, precision) { }

        public override void Configure(double realMin, double realMax, double precision)
        {
            base.Configure(realMin, realMax, precision);
            this.realMin = Math.Max(realMin, 0.000001);  // Avoid log(0)
            logMin = Math.Log10(this.realMin);
            logMax = Math.Log10(this.realMax);
        }

        public override int ToControlUnits(double realValue)
        {
            double logValue = Math.Log10(Math.Max(realValue, 0.000001));
            double normalized = (logValue - logMin) / (logMax - logMin);
            return (int)Math.Round(normalized * RESOLUTION);
        }

        public override double ToRealValue(int controlUnits)
        {
            double normalized = (double)controlUnits / RESOLUTION;
            double logVal = logMin + normalized * (logMax - logMin);
            return Math.Pow(10, logVal);
        }
    }
}