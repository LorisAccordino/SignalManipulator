using NAudio.Wave;
using SignalManipulator.Logic.Core.Routing.Drivers;
using SignalManipulator.Logic.Core.Routing.Outputs;
using SignalManipulator.Logic.Providers;

namespace SignalManipulator.Logic.Core.Routing
{
    public class AudioRouter : IAudioOutput, IDriverSwitchable, IDisposable
    {
        // References
        private IAudioOutput output;
        private AudioDriverType currentDriver;
        private IWaveProvider? currentProvider;

        // Properties
        public float Volume { get => output.Volume; set => output.Volume = value; }
        public WaveFormat OutputWaveFormat => output.OutputWaveFormat;

        // State properties
        public PlaybackState PlaybackState => output.PlaybackState;
        public bool IsPlaying => PlaybackState == PlaybackState.Playing;
        public bool IsPaused => PlaybackState == PlaybackState.Paused;
        public bool IsStopped => PlaybackState == PlaybackState.Stopped;

        // Events
        public event EventHandler? OnStarted;
        public event EventHandler? OnResume;
        public event EventHandler? OnPaused;
        public event EventHandler? OnStopped;
        public event EventHandler<StoppedEventArgs>? PlaybackStopped;
        public event EventHandler<bool>? OnPlaybackStateChanged;

        public AudioRouter(AudioDriverType driver = AudioDriverType.WaveOut)
        {
            currentProvider = new DefaultAudioProvider();
            SetDriver(driver);
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            OnStarted += (s, e) => OnPlaybackStateChanged?.Invoke(s, true);
            OnResume += (s, e) => OnPlaybackStateChanged?.Invoke(s, true);
            OnPaused += (s, e) => OnPlaybackStateChanged?.Invoke(s, false);
            OnStopped += (s, e) => OnPlaybackStateChanged?.Invoke(s, false);
        }

        public void Init(IWaveProvider waveProvider) => output.Init(currentProvider = waveProvider);
        public void ChangeDevice(int index) => output.ChangeDevice(index);
        public string[] GetOutputDevices() => output.GetOutputDevices();


        // --- Playback methods ---
        public void Play()
        {
            if (IsPlaying) return;
            if (IsStopped) OnStarted?.Invoke(this, EventArgs.Empty);
            if (IsPaused) OnResume?.Invoke(this, EventArgs.Empty);
            output.Play();
        }

        public void Pause()
        {
            if (IsPlaying)
            {
                Pause();
                OnPaused?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Stop()
        {
            if (IsPlaying)
            {
                output.Stop();
                OnStopped?.Invoke(this, EventArgs.Empty);
            }
        }
        // ------------------------


        public void SetDriver(AudioDriverType newDriver) => SwitchDriver(newDriver);
        public AudioDriverType GetDriver() => currentDriver;

        public void SetDriverWithFallback(params AudioDriverType[] fallbackDrivers)
        {
            Exception? lastException = null;

            foreach (var driver in fallbackDrivers)
            {
                try
                {
                    SwitchDriver(driver, true);
                    return;
                }
                catch (Exception ex)
                {
                    lastException = ex;
                }
            }

            throw new InvalidOperationException("No suitable audio driver could be initialized.", lastException);
        }
        private void SwitchDriver(AudioDriverType driver, bool keepState = true)
        {
            bool wasPlaying = keepState && output?.PlaybackState == PlaybackState.Playing;
            output?.Stop();
            output?.Dispose();

            output = AudioOutputFactory.Create(driver);
            output.PlaybackStopped += (s, e) => PlaybackStopped?.Invoke(s, e);
            currentDriver = driver;

            if (currentProvider != null)
            {
                output.Init(currentProvider);
                if (wasPlaying)
                    output.Play();
            }
        }

        public void Dispose() => output.Dispose();
    }
}