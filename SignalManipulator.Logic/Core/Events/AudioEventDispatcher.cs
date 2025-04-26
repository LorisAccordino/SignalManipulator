using SignalManipulator.Logic.Core.Playback;
using SignalManipulator.Logic.Core.Sourcing;
using SignalManipulator.Logic.Providers;
using SignalManipulator.Logic.Utils;
using System;

namespace SignalManipulator.Logic.Core.Events
{
    public class AudioEventDispatcher : IAudioEventDispatcher
    {
        public event Action OnLoad;
        public event Action OnResume;
        public event Action OnPaused;
        public event Action OnStopped;
        public event Action<bool> OnPlaybackStateChanged;

        public event Action OnUpdate;
        public event Action<AudioFrame> FrameReady;
        public event Action<FrequencySpectrum> SpectrumReady;

        public AudioEventDispatcher(IAudioSource audioSource, IPlaybackService playbackService, AudioDataProvider dataProvider)
        {
            // File loading
            audioSource.LoadCompleted += () => OnLoad?.Invoke();

            // Playback state
            playbackService.OnResume += () => OnResume?.Invoke();
            playbackService.OnPaused += () => OnPaused?.Invoke();
            playbackService.OnStopped += () => OnStopped?.Invoke();
            playbackService.OnPlaybackStateChanged += state => OnPlaybackStateChanged?.Invoke(state);

            // Frame & update
            playbackService.OnUpdate += () => OnUpdate?.Invoke();
            dataProvider.FrameReady += frame => FrameReady?.Invoke(frame);
            dataProvider.SpectrumReady += spec => SpectrumReady?.Invoke(spec);
        }
    }
}