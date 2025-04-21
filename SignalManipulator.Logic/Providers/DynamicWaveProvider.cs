using NAudio.Wave;
using SignalManipulator.Logic.Core;

namespace SignalManipulator.Logic.Providers
{
    public class DynamicWaveProvider : IWaveProvider
    {
        public IWaveProvider InnerProvider { get; set; }

        public WaveFormat WaveFormat => InnerProvider?.WaveFormat ?? AudioEngine.DEFAULT_WAVE_FORMAT;

        public int Read(byte[] buffer, int offset, int count)
        {
            return InnerProvider?.Read(buffer, offset, count) ?? 0;
        }
    }
}