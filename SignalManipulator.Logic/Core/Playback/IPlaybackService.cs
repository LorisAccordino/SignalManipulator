using System;

namespace SignalManipulator.Logic.Core.Playback
{
    public interface IPlaybackService
    {
        double Speed { get; set; }
        bool PreservePitch { get; set; }
        event Action OnUpdate;
        event Action<float[]> OnDataAvailable;
        event EventHandler OnFinished;
        void Play();
        void Pause();
        void Stop();
    }
}
