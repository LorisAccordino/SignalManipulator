using NAudio.Wave;
using SignalManipulator.Logic.Core;

namespace SignalManipulator.Logic.Providers
{
    public class DefaultSampleProvider : ISampleProvider
    {
        public WaveFormat WaveFormat => AudioEngine.WAVE_FORMAT;

        public int Read(float[] buffer, int offset, int count)
        {
            // It does nothing, write 0 samples in the buffer
            Array.Clear(buffer, offset, count);
            return 0;  // No samples read
        }
    }
}