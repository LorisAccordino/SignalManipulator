using SignalManipulator.UI.Helpers.Scaling;
using Xunit;

namespace SignalManipulator.Tests.UI
{
    public class LinearScaleMapperTests
    {
        [Theory]
        [InlineData(0, 10, 0.1, 0.0)]
        [InlineData(0, 10, 0.1, 5.0)]
        [InlineData(0, 10, 0.1, 10.0)]
        [InlineData(-5, 5, 0.5, -2.5)]
        [InlineData(-5, 5, 0.5, 0.0)]
        [InlineData(-5, 5, 0.5, 2.5)]
        public void RealValueToControlUnitsAndBack_ShouldBeAccurate(double realMin, double realMax, double precision, double input)
        {
            // Arrange
            var mapper = new LinearScaleMapper(realMin, realMax, precision);

            // Act
            int controlUnits = mapper.ToControlUnits(input);
            double output = mapper.ToRealValue(controlUnits);

            // Assert
            double tolerance = precision / 2;
            Assert.True(Math.Abs(input - output) <= tolerance,
                $"Expected {input}, got {output}, difference {Math.Abs(input - output)} is greater than tolerance {tolerance}");
        }

        [Fact]
        public void ControlUnitsToRealValue_ShouldMapCorrectly()
        {
            // Arrange
            var mapper = new LinearScaleMapper(0, 100, 1);

            // Act & Assert
            Assert.Equal(0.0, mapper.ToRealValue(0));
            Assert.Equal(10.0, mapper.ToRealValue(10));
            Assert.Equal(100.0, mapper.ToRealValue(100));
        }

        [Fact]
        public void ToControlUnits_ShouldRoundCorrectly()
        {
            // Arrange
            var mapper = new LinearScaleMapper(0, 10, 0.1);

            // Act
            int control1 = mapper.ToControlUnits(0.055); // Should round to 1
            int control2 = mapper.ToControlUnits(0.045); // Should round to 0

            // Assert
            Assert.Equal(1, control1);
            Assert.Equal(0, control2);
        }
    }

    public class LogScaleMapperTests
    {
        [Theory]
        [InlineData(1, 1000, 0.1, 1.0)]
        [InlineData(1, 1000, 0.1, 10.0)]
        [InlineData(1, 1000, 0.1, 100.0)]
        public void RealValueToControlUnitsAndBack_ShouldBeAccurate(double realMin, double realMax, double precision, double input)
        {
            // Arrange
            var mapper = new LogScaleMapper(realMin, realMax, precision);

            // Act
            int controlUnits = mapper.ToControlUnits(input);
            double output = mapper.ToRealValue(controlUnits);

            // Assert
            double tolerance = input * 0.05; // tolleranza relativa al 5%
            Assert.True(Math.Abs(input - output) <= tolerance,
                $"Expected {input}, got {output}, difference {Math.Abs(input - output)} exceeds tolerance {tolerance}");
        }

        [Fact]
        public void ToControlUnits_ShouldHandleBoundaries()
        {
            var mapper = new LogScaleMapper(1, 1000, 0.1);

            Assert.Equal(0, mapper.ToControlUnits(1));
            Assert.True(mapper.ToControlUnits(10) > 0);
        }
    }

    public class ExpScaleMapperTests
    {
        [Theory]
        [InlineData(0, 5, 0.1, 0.0)]
        [InlineData(0, 5, 0.1, 1.0)]
        [InlineData(0, 5, 0.1, 2.0)]
        public void RealValueToControlUnitsAndBack_ShouldBeAccurate(double realMin, double realMax, double precision, double input)
        {
            var mapper = new ExpScaleMapper(realMin, realMax, precision);

            int controlUnits = mapper.ToControlUnits(input);
            double output = mapper.ToRealValue(controlUnits);

            double tolerance = 0.05;
            Assert.True(Math.Abs(input - output) <= tolerance,
                $"Expected {input}, got {output}");
        }
    }
}