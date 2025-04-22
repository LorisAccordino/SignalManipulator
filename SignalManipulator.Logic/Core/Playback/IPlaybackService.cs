using System;

namespace SignalManipulator.Logic.Core.Playback
{
    public interface IPlaybackService
    {
        double Speed { get; set; }
        bool PreservePitch { get; set; }
        event EventHandler<byte[]> OnDataAvailable;
        event EventHandler OnFinished;
        void Start();
        void Pause();
        void Stop();
    }
}
