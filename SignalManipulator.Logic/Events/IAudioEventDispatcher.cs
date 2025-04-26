using SignalManipulator.Logic.Models;
using System;

namespace SignalManipulator.Logic.Events
{
    public interface IAudioEventDispatcher
    {
        event Action OnLoad;
        event Action OnResume;
        event Action OnPaused;
        event Action OnStopped;
        event Action<bool> OnPlaybackStateChanged; // bool: isPlaying?

        event Action OnUpdate;
        event Action<WaveformFrame> WaveformReady;
        event Action<FFTFrame> FFTReady;
    }
}