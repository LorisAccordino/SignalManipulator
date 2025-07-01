using NAudio.Wave;
using SignalManipulator.Logic.Core.Routing;
using SignalManipulator.Logic.Core.Sourcing;
using SignalManipulator.Logic.Effects;
using SignalManipulator.Logic.Effects.RubberBand;
using SignalManipulator.Logic.Models;
using SignalManipulator.Logic.Providers;

namespace SignalManipulator.Logic.Core.Playback
{
    public class PlaybackService : IPlaybackService
    {
        private readonly IAudioSource source;
        private readonly IAudioRouter router;
        private readonly EffectChain effects;
        private readonly AudioDataProvider audioDataProvider;

        // Playback "effects"
        private readonly RBTimeStretchEffect timeStretch;
        private readonly VolumeEffect volumeManager;

        // Properties
        public AudioInfo Info => source.Info;

        public double Speed { get => timeStretch.Speed; set => timeStretch.Speed = value; }
        public bool PreservePitch { get => timeStretch.PreservePitch; set => timeStretch.PreservePitch = value; }
        public double Volume { get => volumeManager.Volume; set => volumeManager.Volume = value; }
        

        // Events
        public event EventHandler<AudioInfo> LoadCompleted;
        public event EventHandler OnResume;
        public event EventHandler OnPaused;
        public event EventHandler OnStopped;
        public event EventHandler<bool> OnPlaybackStateChanged; // bool: playing?

        public PlaybackService(IAudioSource source, IAudioRouter router, EffectChain effects, AudioDataProvider audioDataProvider)
        {
            this.source = source;
            this.router = router;
            this.effects = effects;
            this.audioDataProvider = audioDataProvider;

            this.effects.AddEffect<RBTimeStretchEffect>();
            this.effects.AddEffect<VolumeEffect>();
            timeStretch = effects.GetEffect<RBTimeStretchEffect>(0);
            volumeManager = effects.GetEffect<VolumeEffect>(1);

            router.PlaybackStopped += (s, e) => _Stop();
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
            source.Seek(TimeSpan.Zero);
            OnStopped?.Invoke(this, EventArgs.Empty);
            OnPlaybackStateChanged?.Invoke(this, false);
            effects.ResetAll();
        }

        public void Seek(TimeSpan pos) => source.Seek(pos);
    }
}