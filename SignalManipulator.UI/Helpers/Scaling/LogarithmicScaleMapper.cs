using System;

namespace SignalManipulator.UI.Helpers.Scaling
{
    public class LogarithmicScaleMapper : IScaleMapper
    {
        public int ToControlUnits(double realValue, float realMin, float realMax, float precision, int controlMin, int controlMax)
        {
            realValue = Math.Max(realValue, double.Epsilon); // Avoid log(0)
            double logMin = Math.Log10(realMin);
            double logMax = Math.Log10(realMax);
            double logValue = Math.Log10(realValue);

            double normalized = (logValue - logMin) / (logMax - logMin);
            return (int)(controlMin + normalized * (controlMax - controlMin));
        }

        public double ToRealValue(int controlUnits, float realMin, float realMax, float precision, int controlMin, int controlMax)
        {
            double logMin = Math.Log10(realMin);
            double logMax = Math.Log10(realMax);
            double normalized = (double)(controlUnits - controlMin) / (controlMax - controlMin);
            double logValue = logMin + normalized * (logMax - logMin);

            return Math.Pow(10, logValue);
        }
    }
}