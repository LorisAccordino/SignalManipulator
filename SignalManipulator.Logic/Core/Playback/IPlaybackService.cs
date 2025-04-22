using System;

namespace SignalManipulator.Logic.Core.Playback
{
    public interface IPlaybackService
    {
        event EventHandler<byte[]> OnDataAvailable;
        event EventHandler OnFinished;
        void Start();
        void Pause();
        void Stop();
        void SetSpeed(float speed);
        void SetPitchPreserve(bool preserve);
    }
}
