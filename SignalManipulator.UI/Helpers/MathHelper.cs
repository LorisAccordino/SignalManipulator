using System;

namespace SignalManipulator.UI.Helpers
{
    public static class MathHelper
    {
        public static int GetIntegerDigits(double value)
        {
            if (value <= 0) return 1;

            // log10(999) ≈ 2.99 → floor = 2 → +1 = 3 digits
            double log = Math.Log10(value);

            return (int)Math.Floor(log) + 1;
        }

        public static int GetDecimalPlaces(double precision)
        {
            if (precision <= 0) return 0;
            double log = Math.Log10(precision);
            if (log >= 0) return 0;

            // Round it to avoid floating point errors
            return (int)Math.Round(-log);
        }

        public static double SnapToPrecision(double value, double precision)
        {
            if (precision <= 0) return value;
            return Math.Round(value / precision) * precision;
        }

        public static double Lerp(double value, double min, double max)
        {
            return min + value * (max - min);
        }

        public static double InverseLerp(double value, double min, double max)
        {
            if (max == min) return 0; // Avoid division by zero
            return (value - min) / (max - min);
        }

        public static double Normalize(double value, double min, double max)
        {
            return InverseLerp(value, min, max); // Alias
        }

        public static double Clamp(double value, double min, double max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}