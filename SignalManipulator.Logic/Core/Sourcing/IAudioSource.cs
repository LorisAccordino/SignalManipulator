using NAudio.Wave;
using SignalManipulator.Logic.Models;
using System;

namespace SignalManipulator.Logic.Core.Sourcing
{
    public interface IAudioSource : IDisposable
    {
        // Audio info
        AudioInfo Info { get; }

        // Events
        event Action<AudioInfo> LoadCompleted;

        // Methods
        void Load(string path);
        void Seek(TimeSpan position);
    }
}