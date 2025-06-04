using System;

namespace SignalManipulator.UI.Helpers.Scaling
{
    public class ExpScaleMapper : BaseScaleMapper
    {
        private const int RESOLUTION = 1000;

        public ExpScaleMapper(double realMin, double realMax, double precision) : base(realMin, realMax, precision) { }

        public override int ToControlUnits(double realValue)
        {
            double normalized = (realValue - realMin) / (realMax - realMin);
            //double expNormalized = Math.Pow(normalized, 10);
            double expNormalized = Math.Pow(10, normalized);
            return (int)Math.Round(expNormalized * RESOLUTION);
        }

        public override double ToRealValue(int controlUnits)
        {
            /*double normalized = (double)controlUnits / RESOLUTION;
            // Inversione della mappatura
            double linear = Math.Pow(normalized, 2.0); // Quadrato
            return realMin + linear * (realMax - realMin);*/

            double logValue = Math.Log10(Math.Max(controlUnits, 0.000001));
            double normalized = (logValue - realMin) / (realMax - realMin);
            return (int)Math.Round(normalized * RESOLUTION);
        }
    }
}