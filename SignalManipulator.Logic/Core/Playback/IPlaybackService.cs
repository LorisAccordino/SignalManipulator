using System;

namespace SignalManipulator.Logic.Core.Playback
{
    public interface IPlaybackService
    {
        double Speed { get; set; }
        bool PreservePitch { get; set; }
        event Action OnResume;
        event Action OnPaused;
        event Action OnStopped;
        event Action<bool> OnPlaybackStateChanged;
        event Action OnUpdate;
        //event Action<AudioFrame> FrameReady;
        void Play();
        void Pause();
        void Stop();
    }
}
