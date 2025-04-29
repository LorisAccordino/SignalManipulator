using System;
using System.Collections.Generic;
namespace SignalManipulator.UI.Helpers.Scaling
{
    public class LinearScaleMapper : IScaleMapper
    {
        public int ToControlUnits(double realValue, float realMin, float realMax, float precision, int controlMin, int controlMax)
        {
            double range = realMax - realMin;
            double normalized = (realValue - realMin) / range;
            return (int)(controlMin + normalized * (controlMax - controlMin));
        }

        public double ToRealValue(int controlUnits, float realMin, float realMax, float precision, int controlMin, int controlMax)
        {
            double range = realMax - realMin;
            double normalized = (double)(controlUnits - controlMin) / (controlMax - controlMin);
            return realMin + normalized * range;
        }
    }
}