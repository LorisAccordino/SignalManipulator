using NAudio.Wave;
using SignalManipulator.Logic.Effects;
using System;
using System.Threading;

namespace SignalManipulator.Logic.Core
{
    public class AudioPlayer
    {
        // Properties
        public PlaybackState PlaybackState => audioRouter.CurrentDevice.PlaybackState;
        public bool IsPlaying => PlaybackState == PlaybackState.Playing;
        public bool IsPaused => PlaybackState == PlaybackState.Paused;
        public bool IsStopped => PlaybackState == PlaybackState.Stopped;

        public string GetCurrentTime(string format = @"mm\:ss\.fff") => audioFileReader?.CurrentTime.ToString(format);

        public double PlaybackSpeed { get => playbackTimeStrechEffect.Speed; set => playbackTimeStrechEffect.Speed = value; }
        public bool PreservePitch { get => playbackTimeStrechEffect.PreservePitch; set => playbackTimeStrechEffect.PreservePitch = value; }


        // Events
        public event EventHandler OnLoad;
        public event EventHandler OnUpdate;
        public event EventHandler OnStarted;
        public event EventHandler OnPaused;
        public event EventHandler OnStopped;
        public event EventHandler<bool> OnPlaybackStateChanged;


        // Audio providers & logic components
        public BufferedWaveProvider BufferedWaveProvider { get; private set; } = new BufferedWaveProvider(AudioEngine.WAVE_FORMAT);
        private AudioFileReader audioFileReader;
        private Thread playbackThread;
        private System.Timers.Timer updateTimer = new System.Timers.Timer(1.0 / AudioEngine.TARGET_FPS);


        // Audio modules references
        private AudioEngine audioEngine;
        private AudioRouter audioRouter => audioEngine.AudioRouter;
        private EffectChain effectChain => audioEngine.EffectChain;

        private TimeStretchEffect playbackTimeStrechEffect;

        public AudioPlayer(AudioEngine audioEngine)
        {
            this.audioEngine = audioEngine;

            // Playback rate "effect"
            effectChain.AddEffect<TimeStretchEffect>();
            playbackTimeStrechEffect = effectChain.GetEffect(0) as TimeStretchEffect;
            PlaybackSpeed = 1.0;
            PreservePitch = false;

            InitializePlaybackEvents();
        }

        private void InitializePlaybackEvents()
        {
            OnStarted += (s, e) => OnPlaybackStateChanged.Invoke(this, true);
            OnPaused += (s, e) => OnPlaybackStateChanged.Invoke(this, false);
            OnStopped += (s, e) => OnPlaybackStateChanged.Invoke(this, false);
            updateTimer.Elapsed += (s, e) => OnUpdate.Invoke(s, e);
        }


        public void LoadAudio(string path)
        {
            LoadAudio(new AudioFileReader(path));
            OnLoad?.Invoke(this, EventArgs.Empty);
        }

        public void LoadAudio(AudioFileReader audioFileReader)
        {
            Stop(); // Stop previous if any            
            this.audioFileReader = audioFileReader;
            effectChain.SourceProvider.InnerProvider = audioFileReader;

            // Audio inits and settings
            BufferedWaveProvider = new BufferedWaveProvider(this.audioFileReader.WaveFormat);
            audioRouter.InitOutputs(BufferedWaveProvider);
        }



        // State methods

        public void Play()
        {
            if (audioRouter.CurrentDevice == null || audioFileReader == null) return;

            bool isStopped = IsStopped;
            audioRouter.CurrentDevice.Play();

            // Start playback thread
            if (isStopped)
            {
                playbackThread = new Thread(PlaybackThreadRoutine);
                playbackThread.Start();
            }

            updateTimer.Start();
            OnStarted?.Invoke(this, EventArgs.Empty);
        }

        public void Pause()
        {
            if (audioRouter.CurrentDevice == null || audioFileReader == null) return;
            
            audioRouter.CurrentDevice.Pause();
            OnPaused?.Invoke(this, EventArgs.Empty);
        }

        public void Stop()
        {
            if (audioRouter.CurrentDevice == null || audioFileReader == null) return;

            audioRouter.CurrentDevice.Stop();
            audioFileReader.CurrentTime = TimeSpan.Zero;
            BufferedWaveProvider.ClearBuffer();

            updateTimer.Stop();
            OnStopped?.Invoke(this, EventArgs.Empty);
            OnUpdate?.Invoke(this, EventArgs.Empty);
        }

        public bool IsBufferFull(int samples = 44100)
        {
            return BufferedWaveProvider.BufferedBytes > samples * PlaybackSpeed;
        }


        // Thread routine
        private void PlaybackThreadRoutine()
        {
            byte[] buffer = new byte[AudioEngine.CHUNK_SIZE];
            //while (audioFile.Read(buffer, 0, buffer.Length) > 0 && !IsStopped)
            while (!IsStopped)
            {
                // Effects
                effectChain.ProcessEffects(buffer);

                // Add samples to the buffer
                BufferedWaveProvider.AddSamples(buffer, 0, buffer.Length);

                // Wait for the buffer to be empty enough
                while (IsBufferFull() && !IsStopped) Thread.Sleep(10);
            }
        }
    }
}
