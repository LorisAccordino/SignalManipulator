using NAudio.Wave;
using System;

namespace SignalManipulator.Logic.Core.Sourcing
{
    public interface IAudioSource : IDisposable
    {
        IWaveProvider SourceProvider { get; }
        string FileName { get; }
        TimeSpan CurrentTime { get; }
        TimeSpan TotalTime { get; }
        void Load(string path);
        void Seek(TimeSpan position);
    }
}