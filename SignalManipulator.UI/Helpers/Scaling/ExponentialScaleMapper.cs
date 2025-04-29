using System;

namespace SignalManipulator.UI.Helpers.Scaling
{
    public class ExponentialScaleMapper : IScaleMapper
    {
        public int ToControlUnits(double realValue, float realMin, float realMax, float precision, int controlMin, int controlMax)
        {
            double normalized = (realValue - realMin) / (realMax - realMin);
            double expNormalized = (Math.Pow(10, normalized) - 1) / (10 - 1); // Scale from 1 to 10
            return (int)(controlMin + expNormalized * (controlMax - controlMin));
        }

        public double ToRealValue(int controlUnits, float realMin, float realMax, float precision, int controlMin, int controlMax)
        {
            double normalized = (double)(controlUnits - controlMin) / (controlMax - controlMin);
            double invExpNormalized = Math.Log10(normalized * (10 - 1) + 1);
            return realMin + invExpNormalized * (realMax - realMin);
        }
    }
}