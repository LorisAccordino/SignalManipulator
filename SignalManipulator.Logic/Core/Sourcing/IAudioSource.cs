using NAudio.Wave;
using System;

namespace SignalManipulator.Logic.Core.Sourcing
{
    public interface IAudioSource : IDisposable
    {
        // Properties
        ISampleProvider SourceProvider { get; }
        string FileName { get; }
        TimeSpan CurrentTime { get; }
        TimeSpan TotalTime { get; }

        // Events
        event Action LoadCompleted;

        // Methods
        void Load(string path);
        void Seek(TimeSpan position);
    }
}