using SignalManipulator.Logic.Data;
using SignalManipulator.Logic.Info;

namespace SignalManipulator.Logic.Events
{
    public interface IAudioEventDispatcher
    {
        event EventHandler<AudioInfo>? OnLoad;
        public event EventHandler? OnStarted;
        event EventHandler? OnResume;
        event EventHandler? OnPaused;
        event EventHandler? OnStopped;
        event EventHandler<bool>? OnPlaybackStateChanged; // bool: isPlaying?

        event EventHandler<AnalyzedAudioSlice>? AudioDataReady;
    }
}