using NAudio.Wave;

namespace SignalManipulator.Logic.Core.Buffering
{
    public class BufferedWaveManager : IBufferManager
    {
        private readonly BufferedWaveProvider buffer;
        public IWaveProvider OutputProvider => buffer;

        public BufferedWaveManager(WaveFormat fmt)
        {
            buffer = new BufferedWaveProvider(fmt);
        }

        public void AddSamples(byte[] buffer, int offset, int count) => 
            this.buffer.AddSamples(buffer, offset, count);

        public bool IsFull(int thresholdSamples) =>
            buffer.BufferedBytes > thresholdSamples * buffer.WaveFormat.AverageBytesPerSecond / buffer.WaveFormat.SampleRate;

        public void Clear() => buffer.ClearBuffer();
    }
}