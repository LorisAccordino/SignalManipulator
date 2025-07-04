﻿namespace SignalManipulator.UI.Scaling
{
    public abstract class BaseScaleMapper : IScaleMapper
    {
        public double RealMin { get; protected set; }
        public double RealMax { get; protected set; }
        public double Precision { get; protected set; }

        public BaseScaleMapper(double realMin, double realMax, double precision)
        {
            RealMin = realMin;
            RealMax = realMax;
            Precision = precision;
        }

        public abstract int ToControlUnits(double realValue);
        public abstract double ToRealValue(int controlUnits);
    }
}