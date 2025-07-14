using NAudio.Wave;
using SignalManipulator.Logic.Core.Routing;
using SignalManipulator.Logic.Data;
using SignalManipulator.Logic.Info;

namespace SignalManipulator.Logic.Core.Playback
{
    public class AudioPlayer
    {
        private readonly PlaybackService playback;
        private readonly AudioRouter router;

        public AudioPlayer(PlaybackService playback, AudioRouter router)
        {
            this.playback = playback;
            this.router = router;

            InitializeEvents();
        }

        private void InitializeEvents()
        {
            // File loading
            playback.LoadCompleted += (s, info) => OnLoad?.Invoke(s, info);

            // Playback state
            playback.OnStarted += (s, e) => OnStarted?.Invoke(s, e);
            playback.OnResume += (s, e) => OnResume?.Invoke(s, e);
            playback.OnPaused += (s, e) => OnPaused?.Invoke(s, e);
            playback.OnStopped += (s, e) => OnStopped?.Invoke(s, e);
            playback.OnPlaybackStateChanged += (s, playing) => OnPlaybackStateChanged?.Invoke(s, playing);
        }

        // --- Events ---
        public event EventHandler<AudioInfo>? OnLoad;
        public event EventHandler? OnStarted;
        public event EventHandler? OnResume;
        public event EventHandler? OnPaused;
        public event EventHandler? OnStopped;
        public event EventHandler<bool>? OnPlaybackStateChanged;

        public event EventHandler<AnalyzedAudioSlice>? AudioDataReady;

        // --- Commands ---
        public void Load(string path) => playback.Load(path);
        public void Play() { if (!IsPlaying) playback.Play(); }
        public void Pause() { if (IsPlaying) playback.Pause(); }
        public void Stop() => playback.Stop();
        public void Seek(double time) => Seek(TimeSpan.FromSeconds(time));
        public void Seek(TimeSpan pos) => playback.Seek(pos);

        // --- State ---
        public bool IsPlaying => router.CurrentDevice.PlaybackState == PlaybackState.Playing;
        public bool IsPaused => router.CurrentDevice.PlaybackState == PlaybackState.Paused;
        public bool IsStopped => router.CurrentDevice.PlaybackState == PlaybackState.Stopped;

        // --- Audio info ---
        public AudioInfo Info => playback.Info;

        // --- Modifiers ---
        public double PlaybackSpeed { get => playback.Speed; set => playback.Speed = value; }
        public bool PreservePitch { get => playback.PreservePitch; set => playback.PreservePitch = value; }
        public double Volume { get => playback.Volume; set => playback.Volume = value; }
    }
}