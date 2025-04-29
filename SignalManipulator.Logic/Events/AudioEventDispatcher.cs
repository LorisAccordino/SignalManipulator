using SignalManipulator.Logic.Models;
using SignalManipulator.Logic.Core.Playback;
using SignalManipulator.Logic.Providers;
using System;

namespace SignalManipulator.Logic.Events
{
    public class AudioEventDispatcher : IAudioEventDispatcher
    {
        public event Action<AudioInfo> OnLoad;
        public event Action OnResume;
        public event Action OnPaused;
        public event Action OnStopped;
        public event Action<bool> OnPlaybackStateChanged;

        public event Action OnUpdate;
        public event Action<WaveformFrame> WaveformReady;
        public event Action<FFTFrame> FFTReady;

        public AudioEventDispatcher(IPlaybackService playbackService, AudioDataProvider dataProvider)
        {
            // File loading
            playbackService.LoadCompleted += (info) => OnLoad?.Invoke(info);

            // Playback state
            playbackService.OnResume += () => OnResume?.Invoke();
            playbackService.OnPaused += () => OnPaused?.Invoke();
            playbackService.OnStopped += () => OnStopped?.Invoke();
            playbackService.OnPlaybackStateChanged += state => OnPlaybackStateChanged?.Invoke(state);

            // Frame & update
            playbackService.OnUpdate += () => OnUpdate?.Invoke();
            dataProvider.WaveformReady += frame => WaveformReady?.Invoke(frame);
            dataProvider.FFTReady += spec => FFTReady?.Invoke(spec);
        }
    }
}