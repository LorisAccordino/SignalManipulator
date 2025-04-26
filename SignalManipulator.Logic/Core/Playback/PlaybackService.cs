using NAudio.Wave;
using SignalManipulator.Logic.Core.Sourcing;
using SignalManipulator.Logic.Effects;
using SignalManipulator.Logic.Providers;
using System;

namespace SignalManipulator.Logic.Core.Playback
{
    public class PlaybackService : IPlaybackService
    {
        private readonly IAudioSource source;
        private readonly AudioRouter router;
        private readonly EffectChain effects;
        private readonly AudioDataProvider audioDataProvider;
        private readonly System.Timers.Timer updateTimer;
        private readonly TimeStretchEffect timeStrech;

        public double Speed { get => timeStrech.Speed; set => timeStrech.Speed = value; }
        public bool PreservePitch { get => timeStrech.PreservePitch; set => timeStrech.PreservePitch = value; }

        // Events
        public event Action OnResume;
        public event Action OnPaused;
        public event Action OnStopped;
        public event Action<bool> OnPlaybackStateChanged; // bool: playing?

        public event Action OnUpdate;
        //public event Action<AudioFrame> FrameReady;

        public PlaybackService(IAudioSource source, AudioRouter router, EffectChain effects, AudioDataProvider audioDataProvider)
        {
            this.source = source;
            this.router = router;
            this.effects = effects;
            this.audioDataProvider = audioDataProvider;

            this.effects.AddEffect<TimeStretchEffect>();
            timeStrech = effects.GetEffect<TimeStretchEffect>(0);

            // Insert an AudioDataProvider to catch the waveform
            //audioDataProvider = new AudioDataProvider(effects);
            //audioDataProvider.FrameReady += (frame) => FrameReady?.Invoke(frame);

            // Initialize the audio
            router.InitOutputs(this.audioDataProvider as IWaveProvider);

            updateTimer = new System.Timers.Timer(1000.0 / AudioEngine.TARGET_FPS);
            updateTimer.Elapsed += (s, e) => OnUpdate?.Invoke();
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
            OnStopped?.Invoke();
            OnPlaybackStateChanged?.Invoke(false);
        }
    }
}