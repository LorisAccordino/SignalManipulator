using NAudio.Wave;
using SignalManipulator.Logic.Core.Routing;
using SignalManipulator.Logic.Core.Sourcing;
using SignalManipulator.Logic.Effects;
using SignalManipulator.Logic.Models;
using SignalManipulator.Logic.Providers;
using System;

namespace SignalManipulator.Logic.Core.Playback
{
    public class PlaybackService : IPlaybackService
    {
        private readonly IAudioSource source;
        private readonly IAudioRouter router;
        private readonly EffectChain effects;
        private readonly AudioDataProvider audioDataProvider;

        private readonly System.Timers.Timer updateTimer;
        private readonly TimeStretchEffect timeStrech;

        // Properties
        public AudioInfo Info => source.Info;

        public double Speed { get => timeStrech.Speed; set => timeStrech.Speed = value; }
        public bool PreservePitch { get => timeStrech.PreservePitch; set => timeStrech.PreservePitch = value; }

        // Events
        public event Action<AudioInfo> LoadCompleted;
        public event Action OnResume;
        public event Action OnPaused;
        public event Action OnStopped;
        public event Action<bool> OnPlaybackStateChanged; // bool: playing?

        public event Action OnUpdate;

        public PlaybackService(IAudioSource source, IAudioRouter router, EffectChain effects, AudioDataProvider audioDataProvider)
        {
            this.source = source;
            this.router = router;
            this.effects = effects;
            this.audioDataProvider = audioDataProvider;

            this.effects.AddEffect<TimeStretchEffect>();
            timeStrech = effects.GetEffect<TimeStretchEffect>(0);

            updateTimer = new System.Timers.Timer(1000.0 / AudioEngine.TARGET_FPS);
            updateTimer.Elapsed += (s, e) => OnUpdate?.Invoke();
        }

        public void Load(string path)
        {
            source.Load(path);
            effects.SetSource(source.Info.SourceProvider);
            router.InitOutputs(audioDataProvider as IWaveProvider);
            Stop();
            LoadCompleted?.Invoke(Info);
        }

        public void Play()
        {
            updateTimer.Start();
            router.CurrentDevice.Play();
            OnResume?.Invoke();
            OnPlaybackStateChanged?.Invoke(true);
        }

        public void Pause()
        {
            router.CurrentDevice.Pause();
            OnPaused?.Invoke();
            OnPlaybackStateChanged?.Invoke(false);
        }

        public void Stop()
        {
            router.CurrentDevice.Stop();
            updateTimer.Stop();
            effects.ResetAll();
            source.Seek(TimeSpan.Zero);
            updateTimer.Stop();
            OnUpdate?.Invoke();
            OnStopped?.Invoke();
            OnPlaybackStateChanged?.Invoke(false);
        }

        public void Seek(TimeSpan pos) => source.Seek(pos);
    }
}