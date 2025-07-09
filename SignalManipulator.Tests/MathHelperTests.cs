using System.Diagnostics.CodeAnalysis;
using SignalManipulator.Logic.AudioMath.Scaling;

namespace SignalManipulator.Tests
{
    [ExcludeFromCodeCoverage]
    public class MathHelperTests
    {
        [Theory]
        [InlineData(0, 1)]
        [InlineData(-1, 1)]
        [InlineData(1, 1)]
        [InlineData(9, 1)]
        [InlineData(99, 2)]
        [InlineData(999, 3)]
        [InlineData(1000, 4)]
        [InlineData(123456789, 9)]
        public void GetIntegerDigits_ShouldReturnCorrectDigits(double value, int expected)
        {
            int result = ScalingHelper.GetIntegerDigits(value);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(0.1, 1)]
        [InlineData(0.01, 2)]
        [InlineData(0.001, 3)]
        [InlineData(0.00001, 5)]
        [InlineData(0.0000001, 7)]
        public void GetDecimalPlaces_ShouldReturnCorrectPlaces(double precision, int expected)
        {
            int result = ScalingHelper.GetDecimalPlaces(precision);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5.1, 1.0, 5.0)]
        [InlineData(5.5, 1.0, 6.0)]
        [InlineData(0.123, 0.01, 0.12)]
        [InlineData(0.127, 0.01, 0.13)]
        [InlineData(0.125, 0.05, 0.1)]
        [InlineData(0.0, 0.1, 0.0)]
        [InlineData(1.0, 0.0, 1.0)] // precision <= 0 → returns original
        [InlineData(1.0, -1.0, 1.0)]
        public void SnapToPrecision_ShouldSnapCorrectly(double value, double precision, double expected)
        {
            double result = ScalingHelper.SnapToPrecision(value, precision);
            Assert.Equal(expected, result, 10);
        }

        [Theory]
        [InlineData(0.0, 10.0, 20.0, 10.0)]
        [InlineData(0.5, 10.0, 20.0, 15.0)]
        [InlineData(1.0, 10.0, 20.0, 20.0)]
        [InlineData(0.25, 0.0, 100.0, 25.0)]
        [InlineData(0.0, -10.0, 10.0, -10.0)]
        [InlineData(1.0, -10.0, 10.0, 10.0)]
        public void Lerp_ShouldReturnCorrectInterpolation(double value, double min, double max, double expected)
        {
            double result = ScalingHelper.Lerp(value, min, max);
            Assert.Equal(expected, result, 10);
        }

        [Theory]
        [InlineData(10.0, 0.0, 20.0, 0.5)]
        [InlineData(0.0, 0.0, 20.0, 0.0)]
        [InlineData(20.0, 0.0, 20.0, 1.0)]
        [InlineData(-5.0, -10.0, 0.0, 0.5)]
        [InlineData(10.0, 10.0, 10.0, 0.0)] // Division by zero
        public void InverseLerp_ShouldReturnCorrectValue(double value, double min, double max, double expected)
        {
            double result = ScalingHelper.InverseLerp(value, min, max);
            Assert.Equal(expected, result, 10);
        }

        [Theory]
        [InlineData(5.0, 0.0, 10.0, 0.5)]
        [InlineData(0.0, 0.0, 10.0, 0.0)]
        [InlineData(10.0, 0.0, 10.0, 1.0)]
        [InlineData(-5.0, -10.0, 0.0, 0.5)]
        public void Normalize_ShouldAliasInverseLerp(double value, double min, double max, double expected)
        {
            double result = ScalingHelper.Normalize(value, min, max);
            Assert.Equal(expected, result, 10);
        }
    }
}