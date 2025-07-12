using NAudio.Wave;
using SignalManipulator.Logic.Core.Routing;
using SignalManipulator.Logic.Core.Source;
using SignalManipulator.Logic.Effects;
using SignalManipulator.Logic.Info;
using SignalManipulator.Logic.Providers;

namespace SignalManipulator.Logic.Core.Playback
{
    public class PlaybackService
    {
        private readonly FileAudioSource audioSource = new FileAudioSource();
        private readonly AudioRouter router;
        private readonly EffectChain effects;
        private readonly AudioDataProvider audioDataProvider;

        // Properties
        private readonly PlaybackModifiers modifiers;
        public AudioInfo Info => audioSource.Info;
        public double Speed { get => modifiers.Speed; set => modifiers.Speed = value; }
        public bool PreservePitch { get => modifiers.PreservePitch; set => modifiers.PreservePitch = value; }
        public double Volume { get => modifiers.Volume; set => modifiers.Volume = value; }

        // Events
        public event EventHandler<AudioInfo> LoadCompleted;
        public event EventHandler OnStarted;
        public event EventHandler OnResume;
        public event EventHandler OnPaused;
        public event EventHandler OnStopped;
        public event EventHandler<bool> OnPlaybackStateChanged; // bool: playing?

        public PlaybackService(FileAudioSource audioSource, AudioRouter router, EffectChain effects, AudioDataProvider audioDataProvider)
        {
            this.audioSource = audioSource;
            this.router = router;
            this.effects = effects;
            this.audioDataProvider = audioDataProvider;

            modifiers = new PlaybackModifiers();

            effects.SetSource(modifiers.Output);
            router.PlaybackStopped += (s, e) => _Stop();
        }

        public void Load(string path)
        {
            audioSource.Load(path);
            modifiers.SetSource(audioSource.Info.SourceProvider);
            router.InitOutputs(audioDataProvider as IWaveProvider);
            Stop();
            LoadCompleted?.Invoke(this, Info);
        }

        public void Play()
        {
            if (router.CurrentDevice.PlaybackState == PlaybackState.Stopped) 
                OnStarted?.Invoke(this, EventArgs.Empty);

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
        }

        public void _Stop()
        {
            audioSource.Seek(TimeSpan.Zero);
            OnStopped?.Invoke(this, EventArgs.Empty);
            OnPlaybackStateChanged?.Invoke(this, false);
            effects.ResetAll();
            modifiers.Reset();
        }

        public void Seek(TimeSpan pos) => audioSource.Seek(pos);
    }
}