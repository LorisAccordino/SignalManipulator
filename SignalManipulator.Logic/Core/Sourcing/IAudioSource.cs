using SignalManipulator.Logic.Info;

namespace SignalManipulator.Logic.Core.Sourcing
{
    public interface IAudioSource : IDisposable
    {
        // Audio info
        AudioInfo Info { get; }

        // Methods
        void Load(string path);
        void Seek(TimeSpan position);
    }
}