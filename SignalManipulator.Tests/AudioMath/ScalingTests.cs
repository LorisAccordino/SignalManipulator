using SignalManipulator.Logic.AudioMath.Scaling;
using SignalManipulator.Logic.AudioMath.Scaling.Curves;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.Tests.AudioMath
{
    [ExcludeFromCodeCoverage]
    public class NonLinearScaleMapperTests
    {
        [Theory]
        [InlineData(0.0, 1.0, 0.01)]
        [InlineData(1.0, 10.0, 0.05)]
        [InlineData(0.1, 100.0, 0.1)]
        public void Mapper_ShouldRespectBounds_AndBeDual_Linear(double realMin, double realMax, double precision)
        {
            TestMapper(realMin, realMax, precision, new LinearCurve());
        }

        [Theory]
        [InlineData(-1.0, 1.0, 0.01)]
        [InlineData(-10.0, 0.0, 0.05)]
        [InlineData(-100.0, 100.0, 1.0)]
        [InlineData(-0.1, 0.1, 0.01)]
        public void Mapper_ShouldRespectBounds_AndBeDual_Linear_Negative(double realMin, double realMax, double precision)
        {
            TestMapper(realMin, realMax, precision, new LinearCurve());
        }

        [Theory]
        [InlineData(0.0001, 1.0, 0.0001)]
        [InlineData(1.0, 10.0, 0.01)]
        public void Mapper_ShouldRespectBounds_AndBeDual_Exp(double realMin, double realMax, double precision)
        {
            TestMapper(realMin, realMax, precision, new ExpCurve());
        }

        [Theory]
        [InlineData(-10.0, 10.0, 0.1)]
        [InlineData(-1.0, 1.0, 0.01)]
        public void Mapper_ShouldRespectBounds_AndBeDual_Exp_Negative(double realMin, double realMax, double precision)
        {
            TestMapper(realMin, realMax, precision, new ExpCurve());
        }

        [Theory]
        [InlineData(0.0)]
        [InlineData(0.999)]
        [InlineData(-1.0)]
        [InlineData(3.0)]
        [InlineData(100.0)]
        public void ExpCurve_ShouldThrowException_ForInvalidCurvature(double invalidCurvature)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new ExpCurve(invalidCurvature));
            Assert.Equal("curvature", ex.ParamName);
        }

        [Theory]
        [InlineData(0.0001, 1.0, 0.0001)]
        [InlineData(1.0, 10.0, 0.01)]
        public void Mapper_ShouldRespectBounds_AndBeDual_Log(double realMin, double realMax, double precision)
        {
            TestMapper(realMin, realMax, precision, new LogCurve());
        }

        [Theory]
        [InlineData(-10.0, 10.0, 0.1)]
        public void Mapper_ShouldRespectBounds_AndBeDual_Log_Negative(double realMin, double realMax, double precision)
        {
            TestMapper(realMin, realMax, precision, new LogCurve());
        }

        [Theory]
        [InlineData(0.0, 1.0, 0.01, 2.0)]
        [InlineData(0.0, 1.0, 0.01, 0.5)]
        public void Mapper_ShouldRespectBounds_AndBeDual_Pow(double realMin, double realMax, double precision, double exponent)
        {
            TestMapper(realMin, realMax, precision, new PowCurve(exponent));
        }

        [Theory]
        [InlineData(0.0, 1.0, 0.01, 2.0)]
        [InlineData(0.0, 1.0, 0.01, 1.5)]
        [InlineData(0.0, 1.0, 0.01, 1.2)]
        [InlineData(0.0, 1.0, 0.01, 2.7)]
        public void Mapper_ShouldRespectBounds_AndBeDual_WithVariousCurvatures(double realMin, double realMax, double precision, double curvature)
        {
            TestMapper(realMin, realMax, precision, new ExpCurve(curvature));
            TestMapper(realMin, realMax, precision, new LogCurve(curvature));
        }

        private void TestMapper(double realMin, double realMax, double precision, INonLinearCurve curve)
        {
            var mapper = new NonLinearScaleMapper(realMin, realMax, precision) { Curve = curve };
            int resolution = (int)Math.Round((realMax - realMin) / precision);

            // Check the mapped bounds
            int minCU = mapper.ToControlUnits(realMin);
            int maxCU = mapper.ToControlUnits(realMax);
            Assert.InRange(minCU, 0, resolution);
            Assert.InRange(maxCU, 0, resolution);
            Assert.True(minCU <= maxCU);

            // Duality tests of several points (scalar versions)
            List<double> testValues = new();
            for (int i = 0; i <= 10; i++)
            {
                double realValue = realMin + i * (realMax - realMin) / 10.0;
                testValues.Add(realValue);

                int cu = mapper.ToControlUnits(realValue);
                double back = mapper.ToRealValue(cu);

                Assert.InRange(back, realValue - precision * 1.5, realValue + precision * 1.5);
            }

            // Test that ToRealValue(real) behaves as expected (forward-only curve mapping)
            for (int i = 0; i <= 10; i++)
            {
                double realValue = realMin + i * (realMax - realMin) / 10.0;

                double normalized = ScalingHelper.InverseLerp(realValue, realMin, realMax);
                double curved = curve.Forward(normalized);
                curved = Math.Clamp(curved, 0.0, 1.0);
                double expected = ScalingHelper.Lerp(curved, realMin, realMax);

                double actual = mapper.ToRealValue(realValue);
                Assert.Equal(expected, actual, 10);
            }

            // Test ToControlUnits(double[]) matches individual mapping
            double[] realArray = testValues.ToArray();
            int[] expectedCU = realArray.Select(mapper.ToControlUnits).ToArray();
            int[] actualCU = mapper.ToControlUnits(realArray);
            Assert.Equal(expectedCU, actualCU);

            // Test ToRealValues(double[]) matches individual mapping
            double[] expectedReal = realArray.Select(mapper.ToRealValue).ToArray();
            double[] actualReal = mapper.ToRealValues(realArray);
            for (int i = 0; i < expectedReal.Length; i++)
                Assert.Equal(expectedReal[i], actualReal[i], 10);

            // Perfect duality with real values generated from CU
            for (int cu = 0; cu <= resolution; cu += Math.Max(1, resolution / 10))
            {
                double realValue = mapper.ToRealValue(cu);
                int cu2 = mapper.ToControlUnits(realValue);
                Assert.InRange(Math.Abs(cu2 - cu), 0, 1);
            }
        }
    }

    [ExcludeFromCodeCoverage]
    public class ScalingHelperTests
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