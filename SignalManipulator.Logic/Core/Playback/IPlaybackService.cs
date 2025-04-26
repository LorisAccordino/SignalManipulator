using System;

namespace SignalManipulator.Logic.Core.Playback
{
    public interface IPlaybackService
    {
        // Parameters
        double Speed { get; set; }
        bool PreservePitch { get; set; }

        // Events
        event Action OnResume;
        event Action OnPaused;
        event Action OnStopped;
        event Action<bool> OnPlaybackStateChanged;
        event Action OnUpdate;

        // Methods
        void Play();
        void Pause();
        void Stop();
    }
}
