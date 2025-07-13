using SignalManipulator.Logic.Core.Playback;
using SignalManipulator.Logic.Data;
using SignalManipulator.Logic.Info;
using SignalManipulator.Logic.Providers;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.Logic.Events
{
    [ExcludeFromCodeCoverage]
    public class AudioEventDispatcher
    {
        public event EventHandler<AudioInfo>? OnLoad;
        public event EventHandler? OnStarted;
        public event EventHandler? OnResume;
        public event EventHandler? OnPaused;
        public event EventHandler? OnStopped;
        public event EventHandler<bool>? OnPlaybackStateChanged;

        public event EventHandler<AnalyzedAudioSlice>? AudioDataReady;

        public AudioEventDispatcher(PlaybackService playbackService, AudioDataProvider audioDataProvider)
        {
            // File loading
            playbackService.LoadCompleted += (s, info) => OnLoad?.Invoke(s, info);

            // Playback state
            playbackService.OnStarted += (s, e) => OnStarted?.Invoke(s, e);
            playbackService.OnResume += (s, e) => OnResume?.Invoke(s, e);
            playbackService.OnPaused += (s, e) => OnPaused?.Invoke(s, e);
            playbackService.OnStopped += (s, e) => OnStopped?.Invoke(s, e);
            playbackService.OnPlaybackStateChanged += (s, playing) => OnPlaybackStateChanged?.Invoke(s, playing);

            // Frame & update
            audioDataProvider.AudioDataReady += (s, frame) => AudioDataReady?.Invoke(s, frame);
        }
    }
}