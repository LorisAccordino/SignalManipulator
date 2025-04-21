using MathNet.Numerics.IntegralTransforms;
using NAudio.Wave;
using System;
using System.Linq;
using System.Numerics;

namespace SignalManipulator.Logic.Utils
{
    public static class AudioMathHelper
    {
        public static double Clamp(double value, double min, double max)
        {
            return value < min ? min : (value > max ? max : value);
        }

        public static double LinearToDecibels(double linear)
        {
            if (linear <= 0)
                return double.NegativeInfinity; // Either -1000, if is preferred a finite limit

            return 20.0 * Math.Log10(linear);
        }

        public static double DecibelsToLinear(double decibels)
        {
            return Math.Pow(10.0, decibels / 20.0);
        }

        public static (double[] left, double[] right) SplitStereo(double[] interleaved)
        {
            int sampleCount = interleaved.Length / 2;
            double[] left = new double[sampleCount];
            double[] right = new double[sampleCount];

            for (int i = 0; i < sampleCount; i++)
            {
                left[i] = interleaved[2 * i];
                right[i] = interleaved[2 * i + 1];
            }

            return (left, right);
        }


        public static byte[] ConvertFloatToPcm(float[] input, WaveFormat format)
        {
            double[] asDouble = Array.ConvertAll(input, x => (double)x);
            return ConvertDoubleToPcm(asDouble, format);
        }

        public static float[] ConvertPcmToFloat(byte[] buffer, WaveFormat format)
        {
            double[] temp = ConvertPcmToDouble(buffer, format);
            float[] result = new float[temp.Length];

            for (int i = 0; i < temp.Length; i++)
                result[i] = (float)temp[i];

            return result;
        }

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

        public static byte[] ConvertDoubleToPcm(double[] input, WaveFormat format)
        {
            if (input == null || input.Length == 0)
                return Array.Empty<byte>();

            int bytesPerSample = format.BitsPerSample / 8;
            byte[] output = new byte[input.Length * bytesPerSample];

            for (int i = 0; i < input.Length; i++)
            {
                int offset = i * bytesPerSample;
                double sample = input[i];

                switch (format.Encoding)
                {
                    case WaveFormatEncoding.Pcm:
                        switch (format.BitsPerSample)
                        {
                            case 8:
                                // 8-bit PCM is unsigned
                                byte sample8 = (byte)(Clamp(sample, -1.0, 1.0) * 127 + 128);
                                output[offset] = sample8;
                                break;

                            case 16:
                                short sample16 = (short)(Clamp(sample, -1.0, 1.0) * 32767);
                                BitConverter.GetBytes(sample16).CopyTo(output, offset);
                                break;

                            case 24:
                                int sample24 = (int)(Clamp(sample, -1.0, 1.0) * 8388607);
                                output[offset] = (byte)(sample24 & 0xFF);
                                output[offset + 1] = (byte)((sample24 >> 8) & 0xFF);
                                output[offset + 2] = (byte)((sample24 >> 16) & 0xFF);
                                break;

                            case 32:
                                int sample32 = (int)(Clamp(sample, -1.0, 1.0) * 2147483647);
                                BitConverter.GetBytes(sample32).CopyTo(output, offset);
                                break;

                            default:
                                throw new NotSupportedException($"Unsupported PCM bit depth: {format.BitsPerSample}");
                        }
                        break;

                    case WaveFormatEncoding.IeeeFloat:
                        if (format.BitsPerSample == 32)
                        {
                            float floatSample = (float)sample;
                            BitConverter.GetBytes(floatSample).CopyTo(output, offset);
                        }
                        else if (format.BitsPerSample == 64)
                        {
                            BitConverter.GetBytes(sample).CopyTo(output, offset);
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

        public static double[] CalculateFFT(double[] samples, int sampleRate, out double[] frequencies)
        {
            // Convert to complex (imaginary = 0)
            Complex[] complexSamples = samples.Select(s => new Complex(s, 0)).ToArray();

            // Apply FFT in-place
            Fourier.Forward(complexSamples, FourierOptions.Matlab);

            int n = complexSamples.Length;
            int half = n / 2; // An half of the data is sufficient for the real spectrum

            // Magnitude (ampltiude for each frequency)
            double[] magnitudes = new double[half];
            frequencies = new double[half];

            for (int i = 0; i < half; i++)
            {
                magnitudes[i] = complexSamples[i].Magnitude;
                frequencies[i] = i * sampleRate / (double)n;
            }

            return magnitudes;
        }
    }
}