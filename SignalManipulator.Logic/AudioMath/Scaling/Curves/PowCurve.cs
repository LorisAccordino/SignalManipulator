using System;

namespace SignalManipulator.Logic.AudioMath.Scaling.Curves
{
    public class PowCurve : INonLinearCurve
    {
        private readonly double exponent;
        public PowCurve(double exponent) => this.exponent = exponent;

        public double Forward(double x) => Math.Pow(x, exponent);
        public double Inverse(double y) => Math.Pow(y, 1.0 / exponent);
    }
}