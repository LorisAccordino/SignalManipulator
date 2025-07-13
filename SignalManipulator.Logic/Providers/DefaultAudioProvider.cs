using NAudio.Wave;
using SignalManipulator.Logic.AudioMath;
using SignalManipulator.Logic.Core;

namespace SignalManipulator.Logic.Providers
{
    public class DefaultAudioProvider : ISampleProvider, IWaveProvider
    {
        public static readonly DefaultAudioProvider Empty = new DefaultAudioProvider();
        public WaveFormat WaveFormat => AudioEngine.WAVE_FORMAT;

        public int Read(float[] buffer, int offset, int count)
        {
            // It does nothing, write 0 samples in the fftBuffer
            buffer.Clear(offset, count);
            return 0;  // No samples read
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            // It does nothing, write 0 samples in the fftBuffer
            Array.Clear(buffer, offset, count);
            return 0;  // No samples read
        }
    }
}