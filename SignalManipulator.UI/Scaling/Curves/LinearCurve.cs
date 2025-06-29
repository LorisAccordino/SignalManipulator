﻿namespace SignalManipulator.UI.Scaling.Curves
{
    public class LinearCurve : INonLinearCurve
    {
        public double Forward(double x) => x;
        public double Inverse(double y) => y;
    }
}