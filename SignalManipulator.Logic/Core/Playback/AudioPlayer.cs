using NAudio.Wave;
using SignalManipulator.Logic.Core.Buffering;
using SignalManipulator.Logic.Core.Sourcing;
using System;

namespace SignalManipulator.Logic.Core.Playback
{
    /*
    public class AudioPlayer
    {
        // Properties
        public PlaybackState PlaybackState => audioRouter.CurrentDevice.PlaybackState;
        public bool IsPlaying => PlaybackState == PlaybackState.Playing;
        public bool IsPaused => PlaybackState == PlaybackState.Paused;
        public bool IsStopped => PlaybackState == PlaybackState.Stopped;

        public string GetCurrentTime(string format = @"mm\:ss\.fff") => audioFileReader?.CurrentTime.ToString(format);
        public int CurrentTime => (int)audioFileReader?.CurrentTime.TotalSeconds;

        public double PlaybackSpeed { get => playbackTimeStrechEffect.Speed; set => playbackTimeStrechEffect.Speed = value; }
        public bool PreservePitch { get => playbackTimeStrechEffect.PreservePitch; set => playbackTimeStrechEffect.PreservePitch = value; }


        // Events
        public event EventHandler OnLoad;
        public event EventHandler OnUpdate;
        public event EventHandler<byte[]> OnUpdateData;
        public event EventHandler OnStarted;
        public event EventHandler OnResume;
        public event EventHandler OnPaused;
        public event EventHandler OnStopped;
        public event EventHandler<bool> OnPlaybackStateChanged;


        // Audio providers & logic components
        public BufferedWaveProvider BufferedWaveProvider { get; private set; } = new BufferedWaveProvider(AudioEngine.DEFAULT_WAVE_FORMAT);
        public WaveFormat WaveFormat => BufferedWaveProvider?.WaveFormat ?? AudioEngine.DEFAULT_WAVE_FORMAT;
        public string WaveFormatDesc => WaveFormat.ToString();
        public int Duration => (int)audioFileReader.TotalTime.TotalSeconds;
        private AudioFileReader audioFileReader;
        private Thread playbackThread;
        private System.Timers.Timer updateTimer = new System.Timers.Timer(1000.0 / AudioEngine.TARGET_FPS);


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
            OnStarted += (s, e) => OnPlaybackStateChanged?.Invoke(this, true);
            OnPaused += (s, e) => OnPlaybackStateChanged?.Invoke(this, false);
            OnStopped += (s, e) => OnPlaybackStateChanged?.Invoke(this, false);
            updateTimer.Elapsed += (s, e) => OnUpdate?.Invoke(s, e);
        }


        public void LoadAudio(string path)
        {
            Stop(); // Stop previous if any
                    
            // Update providers
            audioFileReader = new AudioFileReader(path);
            effectChain.SourceProvider.InnerProvider = audioFileReader;

            // Audio inits and settings
            BufferedWaveProvider = new BufferedWaveProvider(audioFileReader.WaveFormat);
            audioRouter.InitOutputs(BufferedWaveProvider);
            OnLoad?.Invoke(this, EventArgs.Empty);
        }



        // State methods

        public void Play()
        {
            if (!IsAudioReady()) return;

            bool isStopped = IsStopped;
            audioRouter.CurrentDevice.Play();

            // Start playback thread
            if (isStopped)
            {
                playbackThread = new Thread(PlaybackThreadRoutine);
                playbackThread.Start();
                OnStarted?.Invoke(this, EventArgs.Empty);
            }

            updateTimer.Start();
            //OnStarted?.Invoke(this, EventArgs.Empty);
            OnResume?.Invoke(this, EventArgs.Empty);
        }

        public void Pause()
        {
            if (!IsAudioReady()) return;
            
            audioRouter.CurrentDevice.Pause();
            OnPaused?.Invoke(this, EventArgs.Empty);
        }

        public void Stop()
        {
            if (!IsAudioReady()) return;

            audioRouter.CurrentDevice.Stop();
            audioFileReader.CurrentTime = TimeSpan.Zero;
            BufferedWaveProvider.ClearBuffer();

            updateTimer.Stop();
            OnUpdate?.Invoke(this, EventArgs.Empty);
            OnStopped?.Invoke(this, EventArgs.Empty);
        }

        public void SetTimeTo(int time)
        {
            audioFileReader.CurrentTime = TimeSpan.FromSeconds(time);
        }


        private bool IsAudioReady()
        {
            return audioRouter.CurrentDevice != null && audioFileReader != null;
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
                OnUpdateData?.Invoke(this, buffer);

                // Wait for the buffer to be empty enough
                while (IsBufferFull() && !IsStopped) Thread.Sleep(10);
            }
        }
    }
    */

    public class AudioPlayer
    {
        private readonly IAudioSource loader;
        private readonly IBufferManager buffer;
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
        public WaveFormat WaveFormat => loader.SourceProvider.WaveFormat;
        public string WaveFormatDesc => WaveFormat.ToString();
        public IWaveProvider OutputProvider => buffer.OutputProvider;

        public double PlaybackSpeed { get => playback.Speed; set => playback.Speed = value; }
        public bool PreservePitch { get => playback.PreservePitch; set => playback.PreservePitch = value; }
       

        // Events
        public event EventHandler OnLoad;
        public event EventHandler OnUpdate;
        public event EventHandler<byte[]> OnUpdateData;
        public event EventHandler OnStarted;
        public event EventHandler OnResume;
        public event EventHandler OnPaused;
        public event EventHandler OnStopped;
        public event EventHandler<bool> OnPlaybackStateChanged;


        public AudioPlayer(AudioRouter router, EffectChain effects)
        {
            this.router = router;
            this.effects = effects;

            loader = new AudioFileLoader();
            buffer = new BufferedWaveManager(AudioEngine.DEFAULT_WAVE_FORMAT);
            playback = new PlaybackService(loader, buffer, effects);
            
            // Events wiring

        }

        public void Load(string path)
        {
            loader.Load(path);
            buffer.Clear();
            effects.SourceProvider.InnerProvider = loader.SourceProvider;
        }
        public void Play() => playback.Start();
        public void Pause() => playback.Pause();
        public void Stop() => playback.Stop();

        public void Seek(TimeSpan position) => loader.Seek(position);
    }
}