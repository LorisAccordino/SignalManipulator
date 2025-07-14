using NAudio.Wave;

namespace SignalManipulator.Logic.Effects
{
    public interface IAudioEffect : ISampleProvider
    {
        bool Bypass { get; set; }
        void SetSource(ISampleProvider newSourceProvider);
        int Process(float[] samples, int offset, int count);
        void Reset();
    }
}