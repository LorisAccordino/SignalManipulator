using SignalManipulator.Logic.Models;
using System;

namespace SignalManipulator.Logic.Core.Playback
{
    public interface IPlaybackService
    {
        // Properties
        AudioInfo Info { get; }

        // Parameters
        double Speed { get; set; }
        bool PreservePitch { get; set; }

        // Events
        event Action<AudioInfo> LoadCompleted;
        event Action OnResume;
        event Action OnPaused;
        event Action OnStopped;
        event Action<bool> OnPlaybackStateChanged;
        event Action OnUpdate;

        // Methods
        void Load(string path);
        void Play();
        void Pause();
        void Stop();
        void Seek(TimeSpan pos);
    }
}
