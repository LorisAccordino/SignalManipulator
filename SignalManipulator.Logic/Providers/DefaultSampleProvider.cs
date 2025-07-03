using NAudio.Wave;
using SignalManipulator.Logic.AudioMath;
using SignalManipulator.Logic.Core;

namespace SignalManipulator.Logic.Providers
{
    public class DefaultSampleProvider : ISampleProvider
    {
        public static readonly DefaultSampleProvider Empty = new DefaultSampleProvider();
        public WaveFormat WaveFormat => AudioEngine.WAVE_FORMAT;

        public int Read(float[] buffer, int offset, int count)
        {
            // It does nothing, write 0 samples in the buffer
            buffer.Clear(offset, count);
            return 0;  // No samples read
        }
    }
}