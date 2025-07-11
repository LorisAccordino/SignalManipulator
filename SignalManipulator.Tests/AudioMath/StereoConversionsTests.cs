using SignalManipulator.Logic.AudioMath;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.Tests.AudioMath
{
    [ExcludeFromCodeCoverage]
    public class StereoConversionsTests
    {
        [Fact]
        public void ToDouble_ShouldConvertFloatArrayToDoubleArray()
        {
            float[] input = [1f, 2f, 3f];
            double[] result = input.ToDouble();

            Assert.Equal(input.Length, result.Length);
            for (int i = 0; i < input.Length; i++)
                Assert.Equal(input[i], result[i], 5);
        }

        [Fact]
        public void ToFloat_ShouldConvertDoubleArrayToFloatArray()
        {
            double[] input = [1.1, 2.2, 3.3];
            float[] result = input.ToFloat();

            Assert.Equal(input.Length, result.Length);
            for (int i = 0; i < input.Length; i++)
                Assert.Equal((float)input[i], result[i], 5);
        }

        [Fact]
        public void ToMono_Float_ShouldReturnCorrectAverage()
        {
            float[] stereo = [1f, 3f, 2f, 4f];
            float[] mono = stereo.ToMono();

            Assert.Equal(2, mono.Length);
            Assert.Equal(2f, mono[0]);
            Assert.Equal(3f, mono[1]);
        }

        [Fact]
        public void ToMono_Double_ShouldReturnCorrectAverage()
        {
            double[] stereo = [1.0, 3.0, 2.0, 4.0];
            double[] mono = stereo.ToMono();

            Assert.Equal(2, mono.Length);
            Assert.Equal(2.0, mono[0]);
            Assert.Equal(3.0, mono[1]);
        }

        [Fact]
        public void SplitStereo_Float_ShouldSplitChannelsCorrectly()
        {
            float[] stereo = [1f, 2f, 3f, 4f];
            float[] left = new float[2];
            float[] right = new float[2];

            stereo.SplitStereo(left, right);

            Assert.Equal([1f, 3f], left);
            Assert.Equal([2f, 4f], right);
        }

        [Fact]
        public void SplitStereo_Double_ShouldSplitChannelsCorrectly()
        {
            double[] stereo = [1.0, 2.0, 3.0, 4.0];
            double[] left = new double[2];
            double[] right = new double[2];

            stereo.SplitStereo(left, right);

            Assert.Equal([1.0, 3.0], left);
            Assert.Equal([2.0, 4.0], right);
        }

        [Fact]
        public void SplitStereo_Float_InvalidLength_ShouldThrow()
        {
            float[] stereo = [1f, 2f];
            float[] left = new float[2];
            float[] right = new float[2];

            Assert.Throws<ArgumentException>(() => stereo.SplitStereo(left, right, -1));
            Assert.Throws<ArgumentException>(() => stereo.SplitStereo(left, right, 2));
        }

        [Fact]
        public void SplitStereo_Double_InvalidLength_ShouldThrow()
        {
            double[] stereo = [1.0, 2.0];
            double[] left = new double[2];
            double[] right = new double[2];

            Assert.Throws<ArgumentException>(() => stereo.SplitStereo(left, right, -1));
            Assert.Throws<ArgumentException>(() => stereo.SplitStereo(left, right, 2));
        }

        [Fact]
        public void CombineStereo_ShouldCombineCorrectly()
        {
            float[] left = [1f, 3f];
            float[] right = [2f, 4f];
            float[] stereo = new float[4];

            stereo.CombineStereo(left, right);

            Assert.Equal([1f, 2f, 3f, 4f], stereo);
        }

        [Fact]
        public void CombineStereo_WithInvalidLength_ShouldThrow()
        {
            float[] stereo = new float[3];
            float[] left = [1f, 2f];
            float[] right = [3f, 4f];

            Assert.Throws<ArgumentException>(() => stereo.CombineStereo(left, right, -1));
            Assert.Throws<ArgumentException>(() => stereo.CombineStereo(left, right, 2));
        }

        [Fact]
        public void ToStereo_ShouldReturnInterleavedArray()
        {
            float[] left = [1f, 3f];
            float[] right = [2f, 4f];
            float[] stereo = (left, right).ToStereo();

            Assert.Equal([1f, 2f, 3f, 4f], stereo);
        }

        [Fact]
        public void CombineStereo_FromTuple_ShouldWork()
        {
            float[] left = [5f, 7f];
            float[] right = [6f, 8f];
            float[] stereo = new float[4];

            (left, right).CombineStereo(stereo);

            Assert.Equal([5f, 6f, 7f, 8f], stereo);
        }
    }
}