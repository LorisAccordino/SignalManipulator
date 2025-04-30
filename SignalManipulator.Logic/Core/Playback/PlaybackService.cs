using NAudio.Wave;
using SignalManipulator.Logic.Core.Routing;
using SignalManipulator.Logic.Core.Sourcing;
using SignalManipulator.Logic.Effects;
using SignalManipulator.Logic.Models;
using SignalManipulator.Logic.Providers;
using System;
using System.Timers;

namespace SignalManipulator.Logic.Core.Playback
{
    public class PlaybackService : IPlaybackService
    {
        private readonly IAudioSource source;
        private readonly IAudioRouter router;
        private readonly EffectChain effects;
        private readonly AudioDataProvider audioDataProvider;

        private readonly Timer updateTimer;
        private readonly TimeStretchEffect timeStrech;

        // Properties
        public AudioInfo Info => source.Info;

        public double Speed { get => timeStrech.Speed; set => timeStrech.Speed = value; }
        public bool PreservePitch { get => timeStrech.PreservePitch; set => timeStrech.PreservePitch = value; }

        // Events
        public event EventHandler<AudioInfo> LoadCompleted;
        public event EventHandler OnResume;
        public event EventHandler OnPaused;
        public event EventHandler OnStopped;
        public event EventHandler<bool> OnPlaybackStateChanged; // bool: playing?
        public event EventHandler OnUpdate;

        public PlaybackService(IAudioSource source, IAudioRouter router, EffectChain effects, AudioDataProvider audioDataProvider)
        {
            this.source = source;
            this.router = router;
            this.effects = effects;
            this.audioDataProvider = audioDataProvider;

            this.effects.AddEffect<TimeStretchEffect>();
            timeStrech = effects.GetEffect<TimeStretchEffect>(0);

            updateTimer = new Timer(1000.0 / AudioEngine.TARGET_FPS);
            updateTimer.Elapsed += (s, e) => OnUpdate?.Invoke(this, e);
        }

        public void Load(string path)
        {
            source.Load(path);
            effects.SetSource(source.Info.SourceProvider);
            router.InitOutputs(audioDataProvider as IWaveProvider);
            Stop();
            LoadCompleted?.Invoke(this, Info);
        }

        public void Play()
        {
            updateTimer.Start();
            router.CurrentDevice.Play();
            OnResume?.Invoke(this, EventArgs.Empty);
            OnPlaybackStateChanged?.Invoke(this, true);
        }

        public void Pause()
        {
            router.CurrentDevice.Pause();
            OnPaused?.Invoke(this, EventArgs.Empty);
            OnPlaybackStateChanged?.Invoke(this, false);
        }

        public void Stop()
        {
            router.CurrentDevice.Stop();
            updateTimer.Stop();
            effects.ResetAll();
            source.Seek(TimeSpan.Zero);
            updateTimer.Stop();
            OnUpdate?.Invoke(this, EventArgs.Empty);
            OnStopped?.Invoke(this, EventArgs.Empty);
            OnPlaybackStateChanged?.Invoke(this, false);
        }

        public void Seek(TimeSpan pos) => source.Seek(pos);
    }
}