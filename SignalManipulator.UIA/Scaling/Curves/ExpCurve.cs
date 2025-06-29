using System;

namespace SignalManipulator.UI.Scaling.Curves
{
    public class ExpCurve : INonLinearCurve
    {
        private readonly double baseValue = Math.E;

        public ExpCurve(double curvature = 1)
        {
            if (curvature < 1 || curvature > Math.E)
                throw new ArgumentOutOfRangeException(nameof(curvature), "Curvature must be >= 1 and < e (2,72~).");
            baseValue = Math.Exp(curvature);
        }

        public double Forward(double x) => (Math.Pow(baseValue, x) - 1) / (baseValue - 1);
        public double Inverse(double y) => Math.Log(1 + y * (baseValue - 1), baseValue);
    }
}