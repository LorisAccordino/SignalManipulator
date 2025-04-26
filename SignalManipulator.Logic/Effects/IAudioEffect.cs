using NAudio.Wave;

namespace SignalManipulator.Logic.Effects
{
    public interface IAudioEffect : ISampleProvider
    {
        string Name { get; }
        void SetSource(ISampleProvider newSourceProvider);
        void Reset();
    }
}