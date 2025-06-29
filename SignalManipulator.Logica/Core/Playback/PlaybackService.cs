using NAudio.Wave;
using SignalManipulator.Logic.Core.Routing;
using SignalManipulator.Logic.Core.Sourcing;
using SignalManipulator.Logic.Effects;
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
        //private readonly TimeStretchEffect timeStrech;
        private readonly VolumeEffect volumeManager;

        // Properties
        public AudioInfo Info => source.Info;

        //public double Speed { get => timeStrech.Speed; set => timeStrech.Speed = value; }
        //public bool PreservePitch { get => timeStrech.PreservePitch; set => timeStrech.PreservePitch = value; }
        public double Speed { get; set; } = 1.0;
        public bool PreservePitch { get; set; } = false;
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

            //this.effects.AddEffect<TimeStretchEffect>();
            this.effects.AddEffect<VolumeEffect>();
            //timeStrech = effects.GetEffect<TimeStretchEffect>(0);
            volumeManager = effects.GetEffect<VolumeEffect>(0);
            //volumeManager = effects.GetEffect<VolumeEffect>(1);

            router.PlaybackStopped += (s, e) => Stop();
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
            effects.ResetAll();
            source.Seek(TimeSpan.Zero);
            OnStopped?.Invoke(this, EventArgs.Empty);
            OnPlaybackStateChanged?.Invoke(this, false);
        }

        public void Seek(TimeSpan pos) => source.Seek(pos);
    }
}