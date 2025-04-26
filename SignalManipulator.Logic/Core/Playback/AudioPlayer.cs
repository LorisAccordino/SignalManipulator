using NAudio.Wave;
using SignalManipulator.Logic.Core.Sourcing;
using System;

namespace SignalManipulator.Logic.Core.Playback
{
    /*
    public class AudioPlayer
    {
        private readonly IAudioSource loader;
        private readonly IPlaybackService playback;
        private readonly AudioRouter router;
        private readonly EffectChain effects;


        // Properties
        public PlaybackState PlaybackState => router.CurrentDevice.PlaybackState;
        public bool IsPlaying => PlaybackState == PlaybackState.Playing;
        public bool IsPaused => PlaybackState == PlaybackState.Paused;
        public bool IsStopped => PlaybackState == PlaybackState.Stopped;

        public string FileName => loader.FileName;
        public TimeSpan CurrentTime => loader.CurrentTime;
        public TimeSpan TotalTime => loader.TotalTime;
        public WaveFormat WaveFormat => loader.SourceProvider?.WaveFormat ?? AudioEngine.DEFAULT_WAVE_FORMAT;
        public int SampleRate => WaveFormat.SampleRate;
        public string WaveFormatDesc => WaveFormat.ToString();

        public double PlaybackSpeed { get => playback.Speed; set => playback.Speed = value; }
        public bool PreservePitch { get => playback.PreservePitch; set => playback.PreservePitch = value; }
       

        // Events
        public event Action OnLoad;
        public event Action OnResume;
        public event Action OnPaused;
        public event Action OnStopped;
        public event Action<bool> OnPlaybackStateChanged; // bool: playing?

        public event Action OnUpdate;
        public event Action<AudioFrame> FrameReady;


        public AudioPlayer(AudioRouter router, EffectChain effects)
        {
            loader = new AudioFileLoader();
            playback = new PlaybackService(loader, effects, router);
            this.router = router;
            this.effects = effects;

            // Events wiring
            playback.OnResume += () => OnResume?.Invoke();
            playback.OnPaused += () => OnPaused?.Invoke();
            playback.OnStopped += () => OnStopped?.Invoke();
            playback.OnPlaybackStateChanged += (playing) => OnPlaybackStateChanged?.Invoke(playing);
            playback.OnUpdate += () => OnUpdate?.Invoke();
            playback.FrameReady += (frame) => FrameReady?.Invoke(frame);
        }

        public void Load(string path)
        {
            loader.Load(path);
            effects.SetSource(loader.SourceProvider);
            OnLoad?.Invoke();
        }
        public void Play() => playback.Play();
        public void Pause() => playback.Pause();
        public void Stop() => playback.Stop();
        public void Seek(TimeSpan position) => loader.Seek(position);
    }
    */
}