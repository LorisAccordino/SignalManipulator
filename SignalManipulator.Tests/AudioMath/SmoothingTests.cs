using SignalManipulator.Logic.AudioMath.Smoothing;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.Tests.AudioMath
{
    [ExcludeFromCodeCoverage]
    public class SmoothingTests
    {
        [Fact]
        public void SmootherEMA_Constructor_Throws_On_Invalid_Alpha()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new SmootherEMA(-0.1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new SmootherEMA(1.1));
        }

        [Fact]
        public void SmootherEMA_Smooths_Correctly()
        {
            var smoother = new SmootherEMA(0.5);
            double[] input = [1, 2, 3, 4];
            double[] result = smoother.Smooth(input);

            // First time: output should be same as input because previous is initialized as input
            Assert.Equal(input, result);

            // Second pass with new input
            double[] newInput = [2, 3, 4, 5];
            double[] expected = new double[input.Length];
            for (int i = 0; i < input.Length; i++)
                expected[i] = 0.5 * result[i] + 0.5 * newInput[i];

            double[] result2 = smoother.Smooth(newInput);
            for (int i = 0; i < expected.Length; i++)
                Assert.Equal(expected[i], result2[i], precision: 6);
        }

        [Fact]
        public void SmootherEMA_Updates_Alpha()
        {
            var smoother = new SmootherEMA(0.1);
            smoother.Set(0.9); // Much more weight to previous value

            var input = new double[] { 1, 1, 1 };
            smoother.Smooth(input); // Warm-up
            var result = smoother.Smooth([0, 0, 0]);

            // Values should remain close to 1 since alpha is high (0.9)
            Assert.All(result, v => Assert.True(v > 0.5));
        }

        [Fact]
        public void SmootherSMA_Throws_On_Invalid_Length()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new SmootherSMA(0));
        }

        [Fact]
        public void SmootherSMA_Smooths_Correctly()
        {
            var smoother = new SmootherSMA(2);
            double[] input1 = [1, 2, 3];
            double[] input2 = [3, 4, 5];

            double[] result1 = smoother.Smooth(input1);
            Assert.Equal(input1, result1); // First result equals input

            double[] result2 = smoother.Smooth(input2);
            var expected = new double[] { (1 + 3) / 2.0, (2 + 4) / 2.0, (3 + 5) / 2.0 };
            for (int i = 0; i < expected.Length; i++)
                Assert.Equal(expected[i], result2[i], precision: 6);
        }

        [Fact]
        public void SmootherSMA_Respects_History_Length()
        {
            var smoother = new SmootherSMA(3);
            smoother.Smooth([1.0, 1.0, 1.0]);
            smoother.Smooth([2.0, 2.0, 2.0]);
            smoother.Smooth([3.0, 3.0, 3.0]);
            var result = smoother.Smooth([4.0, 4.0, 4.0]);

            // Expected average over last 3 arrays: [2.0, 2.0, 2.0] (2+3+4)/3
            Assert.All(result, v => Assert.Equal(3.0, v, 6)); // (3+4+2)/3 = 3
        }

        [Fact]
        public void SmootherSMA_Can_Change_History_Dynamically()
        {
            var smoother = new SmootherSMA(5);
            smoother.Smooth([1.0, 2.0]);
            smoother.Set(1); // Now only average current array
            var result = smoother.Smooth([3.0, 4.0]);

            Assert.Equal([3.0, 4.0], result);
        }
    }
}