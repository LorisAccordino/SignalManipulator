using SignalManipulator.Logic.Models;
using SignalManipulator.Logic.Core.Playback;
using SignalManipulator.Logic.Providers;
using System;

namespace SignalManipulator.Logic.Events
{
    public class AudioEventDispatcher : IAudioEventDispatcher
    {
        public event EventHandler<AudioInfo> OnLoad;
        public event EventHandler OnStarted;
        public event EventHandler OnResume;
        public event EventHandler OnPaused;
        public event EventHandler OnStopped;
        public event EventHandler<bool> OnPlaybackStateChanged;

        public event EventHandler<WaveformFrame> WaveformReady;
        public event EventHandler<FFTFrame> FFTReady;

        public AudioEventDispatcher(IPlaybackService playbackService, AudioDataProvider dataProvider)
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
            dataProvider.WaveformReady += (s, frame) => WaveformReady?.Invoke(s, frame);
            dataProvider.FFTReady += (s, frame) => FFTReady?.Invoke(s, frame);
        }
    }
}