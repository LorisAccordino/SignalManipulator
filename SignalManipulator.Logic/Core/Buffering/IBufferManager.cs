using NAudio.Wave;

namespace SignalManipulator.Logic.Core.Buffering
{
    public interface IBufferManager
    {
        IWaveProvider OutputProvider { get; }
        void AddSamples(byte[] buffer, int offset, int count);
        bool IsFull(int thresholdSamples);
        void Clear();
    }
}