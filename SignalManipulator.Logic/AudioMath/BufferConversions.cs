using System.Runtime.InteropServices;

namespace SignalManipulator.Logic.AudioMath
{
    public static class BufferConversions
    {
        public static float[] AsFloats(this byte[] bytes) =>
            bytes.Length % 4 != 0
                ? throw new ArgumentException("Byte array length must be a multiple of 4")
                : [.. MemoryMarshal.Cast<byte, float>(bytes)];

        public static void AsFloats(this byte[] bytes, float[] floats)
        {
            if (bytes.Length % 4 != 0)
                throw new ArgumentException("Byte array length must be a multiple of 4");

            MemoryMarshal.Cast<byte, float>(bytes).CopyTo(floats);
        }

        public static byte[] AsBytes(this float[] floats) =>
            [.. MemoryMarshal.AsBytes(floats.AsSpan())];

        public static void AsBytes(this float[] floats, byte[] bytes) =>
            MemoryMarshal.AsBytes(floats.AsSpan()).CopyTo(bytes);

        public static void CopyToFloats(this byte[] bytes, float[] floats, int floatOffset = 0, int floatCount = -1)
        {
            floatCount = floatCount < 0 ? floats.Length - floatOffset : floatCount;

            if (bytes.Length < floatCount * 4)
                throw new ArgumentException("Not enough bytes to copy the requested floats");

            var sourceSpan = MemoryMarshal.Cast<byte, float>(bytes.AsSpan(..(floatCount * 4)));
            sourceSpan.CopyTo(floats.AsSpan(floatOffset, floatCount));
        }

        public static void CopyToBytes(this float[] floats, byte[] bytes, int byteOffset = 0, int byteCount = -1)
        {
            if (byteOffset % 4 != 0)
                throw new ArgumentException("byteOffset must be a multiple of 4");

            byteCount = byteCount < 0 ? bytes.Length - byteOffset : byteCount;

            if (byteCount % 4 != 0)
                throw new ArgumentException("byteCount must be a multiple of 4");

            int floatCount = byteCount / 4;
            if (floatCount > floats.Length)
                throw new ArgumentException("Not enough floats to copy");

            var floatSpan = floats.AsSpan(0, floatCount);
            MemoryMarshal.AsBytes(floatSpan).CopyTo(bytes.AsSpan(byteOffset, byteCount));
        }

        public static void Copy(this float[] source, int sourceOffset, float[] dest, int destOffset, int count) =>
            source.AsSpan(sourceOffset, count).CopyTo(dest.AsSpan(destOffset, count));

        public static void Copy(this double[] source, int sourceOffset, double[] dest, int destOffset, int count) =>
            source.AsSpan(sourceOffset, count).CopyTo(dest.AsSpan(destOffset, count));

        public static void Copy(this byte[] source, int sourceOffset, byte[] dest, int destOffset, int count) =>
            source.AsSpan(sourceOffset, count).CopyTo(dest.AsSpan(destOffset, count));
    }
}