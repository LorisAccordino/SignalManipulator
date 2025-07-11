using SignalManipulator.Logic.AudioMath;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.Tests.AudioMath
{
    [ExcludeFromCodeCoverage]
    public class ArrayMathTests
    {
        [Fact]
        public void Clear_FloatArray_ShouldZeroValues()
        {
            float[] data = [1f, 2f, 3f];
            data.Clear();
            Assert.All(data, x => Assert.Equal(0f, x));
        }

        [Fact]
        public void Clear_DoubleArray_ShouldZeroValues()
        {
            double[] data = [1.0, 2.0, 3.0];
            data.Clear();
            Assert.All(data, x => Assert.Equal(0.0, x));
        }

        [Fact]
        public void Clear_FloatArrayRange_ShouldZeroSubset()
        {
            float[] data = [1f, 2f, 3f, 4f];
            data.Clear(1, 2); // only clear 2.0 and 3.0
            Assert.Equal([1f, 0f, 0f, 4f], data);
        }

        [Fact]
        public void Clear_DoubleArrayRange_ShouldZeroSubset()
        {
            double[] data = [1.0, 2.0, 3.0, 4.0];
            data.Clear(1, 2); // only clear 2.0 and 3.0
            Assert.Equal([1.0, 0.0, 0.0, 4.0], data);
        }

        [Fact]
        public void Add_FloatArrays_ShouldSumElementwise()
        {
            float[] a = [1f, 2f, 3f];
            float[] b = [0.5f, 1f, -1f];
            a.Add(b);
            Assert.Equal([1.5f, 3f, 2f], a);
        }

        [Fact]
        public void Sub_FloatArrays_ShouldSubtractElementwise()
        {
            float[] a = [3f, 4f, 5f];
            float[] b = [1f, 2f, 3f];
            a.Sub(b);
            Assert.Equal([2f, 2f, 2f], a);
        }

        [Fact]
        public void Mul_ShouldMultiplyByScalar()
        {
            float[] a = [1f, -2f, 3f];
            a.Mul(2f);
            Assert.Equal([2f, -4f, 6f], a);
        }

        [Fact]
        public void Div_ShouldDivideByScalar()
        {
            float[] a = [2f, 4f, 6f];
            a.Div(2f);
            Assert.Equal([1f, 2f, 3f], a);
        }

        [Fact]
        public void Div_ByZero_ShouldThrow()
        {
            float[] a = [1f, 2f];
            Assert.Throws<DivideByZeroException>(() => a.Div(0f));
        }

        [Fact]
        public void Normalize_ShouldScaleToMax1()
        {
            float[] a = [2f, -4f, 1f];
            a.Normalize();
            float[] expected = [0.5f, -1f, 0.25f];
            Assert.Equal(expected[0], a[0], 4);
            Assert.Equal(expected[1], a[1], 4);
            Assert.Equal(expected[2], a[2], 4);
        }

        [Fact]
        public void Normalize_ShouldDoNothingOnEmptyArray()
        {
            float[] a = [];
            a.Normalize();
            float[] expected = [];
            Assert.Equal(expected, a);
        }


        [Fact]
        public void Normalize_ZeroArray_ShouldStayZero()
        {
            float[] a = [0f, 0f, 0f];
            a.Normalize();
            Assert.All(a, x => Assert.Equal(0f, x));
        }

        [Fact]
        public void FadeIn_ShouldRampUpLinearly()
        {
            float[] a = [1f, 1f, 1f, 1f];
            a.FadeIn();
            Assert.Equal([0f, 1f / 3f, 2f / 3f, 1f], a, new FloatComparer(4));
        }

        [Fact]
        public void FadeOut_ShouldRampDownLinearly()
        {
            float[] a = [1f, 1f, 1f, 1f];
            a.FadeOut();
            Assert.Equal([1f, 2f / 3f, 1f / 3f, 0f], a, new FloatComparer(4));
        }

        [Fact]
        public void Clamp_FloatArray_ShouldLimitValues()
        {
            float[] a = [-10f, 0.5f, 5f];
            a.Clamp(0f, 1f);
            Assert.Equal([0f, 0.5f, 1f], a);
        }

        [Fact]
        public void Clamp_DoubleArray_ShouldLimitValues()
        {
            double[] a = [-5.0, 0.1, 2.5];
            a.Clamp(0.0, 1.0);
            Assert.Equal([0.0, 0.1, 1.0], a);
        }


        // Helper class for float array equality with tolerance
        private class FloatComparer : IEqualityComparer<float>
        {
            private readonly int _precision;
            public FloatComparer(int precision) => _precision = precision;

            public bool Equals(float x, float y) =>
                Math.Round(x, _precision) == Math.Round(y, _precision);

            public int GetHashCode(float obj) => obj.GetHashCode();
        }
    }
}