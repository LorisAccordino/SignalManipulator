using SignalManipulator.Logic.Models;
using System;

namespace SignalManipulator.Logic.Events
{
    public interface IAudioEventDispatcher
    {
        event EventHandler<AudioInfo> OnLoad;
        event EventHandler OnResume;
        event EventHandler OnPaused;
        event EventHandler OnStopped;
        event EventHandler<bool> OnPlaybackStateChanged; // bool: isPlaying?

        event EventHandler OnUpdate;
        event EventHandler<WaveformFrame> WaveformReady;
        event EventHandler<FFTFrame> FFTReady;
    }
}