using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using SignalManipulator.Logic.AudioMath.Objects;

namespace SignalManipulator.Tests.AudioMath
{
    [ExcludeFromCodeCoverage]
    public class CardioidTests
    {
        [Fact]
        public void Radius_Should_Be_Max_At_Zero_For_Cosine()
        {
            var cardioid = new Cardioid(1.0, 0.0, Cardioid.CardioidMode.Cosine);
            double radius = cardioid.GetRadius(0);
            Assert.Equal(1.0, radius, 6);
        }

        [Fact]
        public void Radius_Should_Be_Zero_At_Pi_For_Cosine()
        {
            var cardioid = new Cardioid(1.0, 0.0, Cardioid.CardioidMode.Cosine);
            double radius = cardioid.GetRadius(Math.PI);
            Assert.Equal(0.0, radius, 6);
        }

        [Fact]
        public void Radius_Should_Use_Sine_Mode()
        {
            var cardioid = new Cardioid(1.0, 0.0, Cardioid.CardioidMode.Sine);
            double r0 = cardioid.GetRadius(0); // sin(0) = 0 -> r = 0.5
            double rPiOver2 = cardioid.GetRadius(Math.PI / 2); // sin(pi/2) = 1 -> r = 1
            Assert.Equal(0.5, r0, 6);
            Assert.Equal(1.0, rPiOver2, 6);
        }

        [Theory]
        [InlineData(Cardioid.CardioidMode.Cosine, 0.0, 1.0)]          // cos(0) = 1 → radius = 1
        [InlineData(Cardioid.CardioidMode.NegativeCosine, 0.0, 0.0)]  // -cos(0) = -1 → radius = 0
        [InlineData(Cardioid.CardioidMode.Sine, Math.PI / 2, 1.0)]    // sin(pi/2) = 1 → radius = 1
        public void GetRadius_ShouldMatchExpected_ForEachMode(Cardioid.CardioidMode mode, double theta, double expected)
        {
            var cardioid = new Cardioid(1.0, 0.0, mode);
            double radius = cardioid.GetRadius(theta);
            Assert.Equal(expected, radius, 5);
        }

        [Fact]
        public void GetRadius_ShouldFallbackToCosine_OnUnknownMode()
        {
            var cardioid = new Cardioid(1.0, 0.0);

            // Forcing an unknown enum value (e.g., casting 999) to test the default case
            var unknownMode = (Cardioid.CardioidMode)999;
            cardioid.Mode = unknownMode;

            double radius = cardioid.GetRadius(0.0); // cos(0) = 1 → radius = 1
            Assert.Equal(1.0, radius, 5);
        }

        [Fact]
        public void GetPoint_Returns_Correct_Vector()
        {
            var cardioid = new Cardioid(1.0, 0.0);
            Vector2 point = cardioid.GetPoint(0);
            Assert.Equal(1.0f, point.X, 6);
            Assert.Equal(0.0f, point.Y, 6);
        }

        [Fact]
        public void GetPoints_Returns_Correct_Length()
        {
            var cardioid = new Cardioid(1.0, 0.0);
            var points = cardioid.GetPoints(180);
            Assert.Equal(181, points.Count); // 0 to 180 inclusive
        }

        [Fact]
        public void SoftMax_Returns_Zero_If_Empty()
        {
            double result = Cardioid.SoftMax([]);
            Assert.Equal(0.0, result);
        }

        [Fact]
        public void SoftMax_Returns_Correct_Value()
        {
            double result = Cardioid.SoftMax([1.0, 0.0], softness: 10);
            Assert.InRange(result, 0.9, 1.0); // SoftMax skews toward the highest
        }

        [Fact]
        public void Merge_Two_Cardioids_Returns_Correct_Length()
        {
            var a = new Cardioid(1.0, 0.0);
            var b = new Cardioid(0.5, Math.PI);
            var result = Cardioid.Merge(a, b, 100);
            Assert.Equal(101, result.Count); // inclusive
        }

        [Fact]
        public void Merge_Multiple_Cardioids_Returns_Correct_Length()
        {
            var c1 = new Cardioid(1.0, 0.0);
            var c2 = new Cardioid(0.5, Math.PI / 2);
            var c3 = new Cardioid(0.75, Math.PI);
            var result = Cardioid.Merge([c1, c2, c3], 256);
            Assert.Equal(257, result.Count);
        }

        [Fact]
        public void Merge_Should_Respect_Resolution()
        {
            var a = new Cardioid(1.0, 0.0);
            var b = new Cardioid(1.0, 0.0);
            int resolution = 64;
            var result = Cardioid.Merge(a, b, resolution);
            Assert.Equal(resolution + 1, result.Count); // Inclusive
        }
    }
}