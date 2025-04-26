using SignalManipulator.Logic.Utils;
using System;

namespace SignalManipulator.Logic.Core.Events
{
    public interface IAudioEventDispatcher
    {
        event Action OnLoad;
        event Action OnResume;
        event Action OnPaused;
        event Action OnStopped;
        event Action<bool> OnPlaybackStateChanged; // bool: isPlaying?

        event Action OnUpdate;
        event Action<AudioFrame> FrameReady;
        event Action<FrequencySpectrum> SpectrumReady;
    }
}