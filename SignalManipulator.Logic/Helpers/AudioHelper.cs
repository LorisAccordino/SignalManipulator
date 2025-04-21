using NAudio.Wave;
using System;

namespace SignalManipulator.Logic.Helpers
{
    public static class AudioHelper
    {
        public static double[] ConvertPcmToDouble(byte[] buffer, WaveFormat format)
        {
            if (buffer == null || buffer.Length == 0)
                return Array.Empty<double>();

            int bytesPerSample = format.BitsPerSample / 8;
            int sampleCount = buffer.Length / bytesPerSample;
            double[] output = new double[sampleCount];

            for (int i = 0; i < sampleCount; i++)
            {
                int offset = i * bytesPerSample;

                switch (format.Encoding)
                {
                    case WaveFormatEncoding.Pcm:
                        switch (format.BitsPerSample)
                        {
                            case 8:
                                // 8-bit PCM is unsigned
                                byte sample8 = buffer[offset];
                                output[i] = (sample8 - 128) / 128.0;
                                break;

                            case 16:
                                short sample16 = BitConverter.ToInt16(buffer, offset);
                                output[i] = sample16 / 32768.0;
                                break;

                            case 24:
                                int sample24 = (buffer[offset + 2] << 16) | (buffer[offset + 1] << 8) | buffer[offset];
                                if ((sample24 & 0x800000) != 0) sample24 |= unchecked((int)0xFF000000); // sign extension
                                output[i] = sample24 / 8388608.0;
                                break;

                            case 32:
                                int sample32 = BitConverter.ToInt32(buffer, offset);
                                output[i] = sample32 / 2147483648.0;
                                break;

                            default:
                                throw new NotSupportedException($"Unsupported PCM bit depth: {format.BitsPerSample}");
                        }
                        break;

                    case WaveFormatEncoding.IeeeFloat:
                        if (format.BitsPerSample == 32)
                        {
                            float floatSample = BitConverter.ToSingle(buffer, offset);
                            output[i] = floatSample;
                        }
                        else if (format.BitsPerSample == 64)
                        {
                            output[i] = BitConverter.ToDouble(buffer, offset);
                        }
                        else
                        {
                            throw new NotSupportedException($"Unsupported IEEE float bit depth: {format.BitsPerSample}");
                        }
                        break;

                    default:
                        throw new NotSupportedException($"Unsupported encoding: {format.Encoding}");
                }
            }

            return output;
        }
    }
}