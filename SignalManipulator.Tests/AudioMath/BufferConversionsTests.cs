using SignalManipulator.Logic.AudioMath;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.Tests.AudioMath
{
    [ExcludeFromCodeCoverage]
    public class BufferConversionsTests
    {
        [Fact]
        public void AsFloats_ValidInput_ReturnsCorrectFloats()
        {
            byte[] bytes = new byte[4 * 3];
            for (int i = 0; i < 3; i++)
                BitConverter.GetBytes((float)(i + 1)).CopyTo(bytes, i * 4);

            float[] floats = bytes.AsFloats();

            Assert.Equal(3, floats.Length);
            for (int i = 0; i < floats.Length; i++)
                Assert.Equal(i + 1, floats[i]);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public void AsFloats_InvalidLength_ThrowsArgumentException(int length)
        {
            byte[] bytes = new byte[length];
            var ex = Assert.Throws<ArgumentException>(bytes.AsFloats);
            Assert.Contains("multiple of 4", ex.Message);
        }

        [Fact]
        public void AsFloats_IntoArray_ValidCopiesCorrectly()
        {
            byte[] bytes = new byte[4 * 2];
            BitConverter.GetBytes(3.14f).CopyTo(bytes, 0);
            BitConverter.GetBytes(2.71f).CopyTo(bytes, 4);

            float[] floats = new float[2];
            bytes.AsFloats(floats);

            Assert.Equal(3.14f, floats[0]);
            Assert.Equal(2.71f, floats[1]);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void AsFloats_IntoArray_InvalidLength_ThrowsArgumentException(int length)
        {
            byte[] bytes = new byte[length];
            float[] floats = new float[1];
            var ex = Assert.Throws<ArgumentException>(() => bytes.AsFloats(floats));
            Assert.Contains("multiple of 4", ex.Message);
        }

        [Fact]
        public void AsBytes_ReturnsCorrectByteArray()
        {
            float[] floats = [1.5f, -2.5f];
            byte[] bytes = floats.AsBytes();

            Assert.Equal(floats.Length * 4, bytes.Length);

            // Reconstruct the floats from the bytes to verify
            for (int i = 0; i < floats.Length; i++)
            {
                float val = BitConverter.ToSingle(bytes, i * 4);
                Assert.Equal(floats[i], val);
            }
        }

        [Fact]
        public void AsBytes_CopyToByteArray_CopiesCorrectly()
        {
            float[] floats = [7.7f, 8.8f];
            byte[] bytes = new byte[8];
            floats.AsBytes(bytes);

            for (int i = 0; i < floats.Length; i++)
            {
                float val = BitConverter.ToSingle(bytes, i * 4);
                Assert.Equal(floats[i], val);
            }
        }

        [Fact]
        public void CopyToFloats_Valid_CopiesCorrectly()
        {
            byte[] bytes = new byte[4 * 3];
            BitConverter.GetBytes(1.1f).CopyTo(bytes, 0);
            BitConverter.GetBytes(2.2f).CopyTo(bytes, 4);
            BitConverter.GetBytes(3.3f).CopyTo(bytes, 8);

            float[] dest = new float[4];
            dest[0] = 9f; // To verify offset

            bytes.CopyToFloats(dest, 1, 3);
            bytes.CopyToFloats(dest, 1);

            Assert.Equal(9f, dest[0]);
            Assert.Equal(1.1f, dest[1]);
            Assert.Equal(2.2f, dest[2]);
            Assert.Equal(3.3f, dest[3]);
        }

        [Fact]
        public void CopyToFloats_NotEnoughBytes_Throws()
        {
            byte[] bytes = new byte[3]; // Less than 4*1 = 4
            float[] dest = new float[1];
            var ex = Assert.Throws<ArgumentException>(() => bytes.CopyToFloats(dest, 0, 1));
            Assert.Contains("Not enough bytes", ex.Message);
        }

        [Theory]
        [InlineData(1)] // byteOffset not multiple of 4
        [InlineData(3)]
        public void CopyToBytes_InvalidByteOffset_Throws(int byteOffset)
        {
            float[] floats = new float[4];
            byte[] bytes = new byte[16];
            var ex = Assert.Throws<ArgumentException>(() => floats.CopyToBytes(bytes, byteOffset, 8));
            Assert.Contains("byteOffset must be a multiple of 4", ex.Message);
        }

        [Theory]
        [InlineData(6)] // byteCount not multiple of 4
        [InlineData(10)]
        public void CopyToBytes_InvalidByteCount_Throws(int byteCount)
        {
            float[] floats = new float[4];
            byte[] bytes = new byte[16];
            var ex = Assert.Throws<ArgumentException>(() => floats.CopyToBytes(bytes, 0, byteCount));
            Assert.Contains("byteCount must be a multiple of 4", ex.Message);
        }

        [Fact]
        public void CopyToBytes_NotEnoughFloats_Throws()
        {
            float[] floats = new float[2];
            byte[] bytes = new byte[16];
            // byteCount = 16, then floatCount = 4, but floats.Length = 2 -> exception
            var ex = Assert.Throws<ArgumentException>(() => floats.CopyToBytes(bytes, 0, 16));
            Assert.Contains("Not enough floats", ex.Message);
        }

        [Fact]
        public void Copy_FloatArrays_CopiesCorrectly()
        {
            float[] source = [1, 2, 3, 4, 5];
            float[] dest = new float[5];

            source.Copy(1, dest, 2, 2);

            Assert.Equal(0, dest[0]);
            Assert.Equal(0, dest[1]);
            Assert.Equal(2, dest[2]);
            Assert.Equal(3, dest[3]);
            Assert.Equal(0, dest[4]);
        }

        [Fact]
        public void Copy_DoubleArrays_CopiesCorrectly()
        {
            double[] source = [1, 2, 3, 4, 5];
            double[] dest = new double[5];

            source.Copy(0, dest, 1, 3);

            Assert.Equal(0, dest[0]);
            Assert.Equal(1, dest[1]);
            Assert.Equal(2, dest[2]);
            Assert.Equal(3, dest[3]);
            Assert.Equal(0, dest[4]);
        }

        [Fact]
        public void Copy_ByteArrays_CopiesCorrectly()
        {
            byte[] source = [1, 2, 3, 4, 5];
            byte[] dest = new byte[5];

            source.Copy(2, dest, 0, 3);

            Assert.Equal(3, dest[0]);
            Assert.Equal(4, dest[1]);
            Assert.Equal(5, dest[2]);
            Assert.Equal(0, dest[3]);
            Assert.Equal(0, dest[4]);
        }

        [Fact]
        public void CopyToBytes_WithFloatOffset_CopiesCorrectly()
        {
            float[] floats = [10f, 20f, 30f, 40f];
            byte[] bytes = new byte[16];

            // Copy 2 floats starting from floats[1] into bytes at offset 0 (8 bytes)
            floats.CopyToBytes(bytes, 0, 8);

            // Ensure bytes beyond copied floats are zero
            for (int i = 8; i < 16; i++)
                Assert.Equal(0, bytes[i]);

            floats.CopyToBytes(bytes, 0);

            float val0 = BitConverter.ToSingle(bytes, 0);
            float val1 = BitConverter.ToSingle(bytes, 4);

            Assert.Equal(10f, val0);
            Assert.Equal(20f, val1);
        }
    }
}