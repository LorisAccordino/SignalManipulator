using SignalManipulator.UI.Helpers;
using SignalManipulator.UI.Scaling;
using SignalManipulator.UI.Scaling.Curves;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace SignalManipulator.Tests.UI
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
            var mapper = new NonLinearScaleMapper(realMin, realMax, precision);
            mapper.SetCurve(curve);
            int resolution = (int)Math.Round((realMax - realMin) / precision);

            // Check the mapped bounds
            int minCU = mapper.ToControlUnits(realMin);
            int maxCU = mapper.ToControlUnits(realMax);

            Assert.InRange(minCU, 0, resolution);
            Assert.InRange(maxCU, 0, resolution);
            Assert.True(minCU <= maxCU);

            // Duality tests of several points
            for (int i = 0; i <= 10; i++)
            {
                double realValue = realMin + i * (realMax - realMin) / 10.0;
                int cu = mapper.ToControlUnits(realValue);
                double back = mapper.ToRealValue(cu);

                // The rounding can cause a slight difference, tollerance proportionally to the precision
                Assert.InRange(back, realValue - precision * 1.5, realValue + precision * 1.5);
            }

            // Perfect duality with real values generates from CU
            for (int cu = 0; cu <= resolution; cu += Math.Max(1, resolution / 10))
            {
                double realValue = mapper.ToRealValue(cu);
                int cu2 = mapper.ToControlUnits(realValue);

                // Must be equal or at most differ by 1
                Assert.InRange(Math.Abs(cu2 - cu), 0, 1);
            }
        }
    }

    [ExcludeFromCodeCoverage]
    public class TrackBarHelperTests
    {
        [Theory]
        [InlineData("linear", 0.0, 100.0, 0.1)]
        [InlineData("linear", 1.0, 5.0, 0.01)]

        [InlineData("exp", 1.0, 10.0, 0.01)]
        [InlineData("exp", 0.01, 100.0, 0.1)]

        [InlineData("log", 0.0001, 1.0, 0.0001)]
        [InlineData("log", 0.01, 100.0, 0.1)]
        public void TrackBarSettings_ShouldBeCorrect(string curveType, double realMin, double realMax, double precision)
        {
            NonLinearScaleMapper mapper = new NonLinearScaleMapper(realMin, realMax, precision);
            INonLinearCurve curve = curveType switch
            {
                "linear" => new LinearCurve(),
                "exp" => new ExpCurve(),
                "log" => new LogCurve(),
                _ => throw new ArgumentException($"Invalid curve type: {curveType}")
            };
            mapper.SetCurve(curve);
            var settings = TrackBarHelper.GetTrackBarSettings(mapper);

            int expectedMin = mapper.ToControlUnits(realMin);
            int expectedMax = mapper.ToControlUnits(realMax);

            Assert.Equal(expectedMin, settings.Minimum);
            Assert.Equal(expectedMax, settings.Maximum);
            Assert.True(settings.Minimum < settings.Maximum);
        }
    }
}