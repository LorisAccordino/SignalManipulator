using NAudio.Wave;
using SignalManipulator.Logic.Core.Routing;
using SignalManipulator.Logic.Core.Sourcing;
using SignalManipulator.Logic.Effects;
using System;

namespace SignalManipulator.Logic.Core.Playback
{
    public class PlaybackController : IPlaybackController
    {
        private readonly IAudioSource loader;
        private readonly IPlaybackService playback;
        private readonly IAudioRouter router;
        private readonly EffectChain effects;

        public PlaybackController(IAudioSource loader, IPlaybackService playback, IAudioRouter router, EffectChain effects)
        {
            this.loader = loader;
            this.playback = playback;
            this.router = router;
            this.effects = effects;
        }

        // --- Commands ---
        public void Load(string path)
        {
            loader.Load(path);
            effects.SetSource(loader.SourceProvider);
        }

        public void Play() => playback.Play();
        public void Pause() => playback.Pause();
        public void Stop() => playback.Stop();
        public void Seek(TimeSpan pos) => loader.Seek(pos);

        // --- State ---
        public bool IsPlaying => router.CurrentDevice.PlaybackState == PlaybackState.Playing;
        public bool IsPaused => router.CurrentDevice.PlaybackState == PlaybackState.Paused;
        public bool IsStopped => router.CurrentDevice.PlaybackState == PlaybackState.Stopped;

        // --- Audio info ---
        public string FileName => loader.FileName;
        public TimeSpan CurrentTime => loader.CurrentTime;
        public TimeSpan TotalTime => loader.TotalTime;
        public WaveFormat WaveFormat => loader.SourceProvider?.WaveFormat ?? AudioEngine.DEFAULT_WAVE_FORMAT;
        public string WaveFormatDesc => WaveFormat.ToString();
        public int SampleRate => WaveFormat.SampleRate;

        // --- Parameters ---
        public double PlaybackSpeed
        {
            get => playback.Speed;
            set => playback.Speed = value;
        }
        public bool PreservePitch
        {
            get => playback.PreservePitch;
            set => playback.PreservePitch = value;
        }
    }
}