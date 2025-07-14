using NAudio.Wave;
using SignalManipulator.Logic.Core.Routing;
using SignalManipulator.Logic.Core.Source;
using SignalManipulator.Logic.Data;
using SignalManipulator.Logic.Info;

namespace SignalManipulator.Logic.Core.Playback
{
    public class PlaybackService : ISampleProvider
    {
        // References
        private readonly FileAudioSource audioSource = new FileAudioSource();
        private readonly AudioRouter router;

        // Properties
        private readonly PlaybackModifiers modifiers = new PlaybackModifiers();
        public WaveFormat WaveFormat => Info.WaveFormat;
        public AudioInfo Info => audioSource.Info;
        public double Speed { get => modifiers.Speed; set => modifiers.Speed = value; }
        public bool PreservePitch { get => modifiers.PreservePitch; set => modifiers.PreservePitch = value; }
        public double Volume { get => modifiers.Volume; set => modifiers.Volume = value; }

        // Events
        public event EventHandler<AudioInfo>? LoadCompleted;
        public event EventHandler? OnStarted;
        public event EventHandler? OnResume;
        public event EventHandler? OnPaused;
        public event EventHandler? OnStopped;
        public event EventHandler<bool>? OnPlaybackStateChanged; // bool: playing?

        public event EventHandler<AnalyzedAudioSlice>? AudioDataReady;

        public PlaybackService(FileAudioSource audioSource, AudioRouter router)
        {
            this.audioSource = audioSource;
            this.router = router;

            InitializeEvents();
        }

        private void InitializeEvents()
        {
            router.PlaybackStopped += (s, e) => _Stop();

            OnStarted += (s, e) => OnPlaybackStateChanged?.Invoke(s, true);
            OnResume += (s, e) => OnPlaybackStateChanged?.Invoke(s, true);
            OnPaused += (s, e) => OnPlaybackStateChanged?.Invoke(s, false);
            OnStopped += (s, e) => OnPlaybackStateChanged?.Invoke(s, false);
        }

        public void Load(string path)
        {
            audioSource.Load(path);
            modifiers.SetSource(audioSource.Info.SourceProvider);
            Stop();
            LoadCompleted?.Invoke(this, Info);
        }

        public void Play()
        {
            if (router.CurrentDevice.PlaybackState == PlaybackState.Stopped) 
                OnStarted?.Invoke(this, EventArgs.Empty);

            router.CurrentDevice.Play();
            OnResume?.Invoke(this, EventArgs.Empty);;
        }

        public void Pause()
        {
            router.CurrentDevice.Pause();
            OnPaused?.Invoke(this, EventArgs.Empty);
        }

        public void Stop() => router.CurrentDevice.Stop();

        private void _Stop() // Always called after Stop()
        {
            audioSource.Seek(TimeSpan.Zero);
            OnStopped?.Invoke(this, EventArgs.Empty);
            modifiers.Reset();
        }

        public void Seek(TimeSpan pos) => audioSource.Seek(pos);

        public int Read(float[] samples, int offset, int count) => modifiers.Read(samples, offset, count);
    }
}