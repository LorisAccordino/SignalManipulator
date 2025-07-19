using NAudio.Wave;
using SignalManipulator.Logic.Core.Routing.Outputs;
using SignalManipulator.Logic.Providers;

namespace SignalManipulator.Logic.Core.Routing
{
    public class AudioRouter : IAudioOutput, IDriverSwitchable, IDisposable
    {
        public event EventHandler<StoppedEventArgs>? PlaybackStopped;

        private IAudioOutput output;
        private AudioDriverType currentDriver;
        private IWaveProvider? currentProvider;

        public AudioRouter(AudioDriverType driver = AudioDriverType.WaveOut)
        {
            currentProvider = new DefaultAudioProvider();
            SetDriver(driver);
        }

        public PlaybackState PlaybackState => output.PlaybackState;
        public float Volume { get => output.Volume; set => output.Volume = value; }
        public WaveFormat OutputWaveFormat => output.OutputWaveFormat;

        public void Init(IWaveProvider waveProvider) => output.Init(currentProvider = waveProvider);
        public void ChangeDevice(int index) => output.ChangeDevice(index);
        public string[] GetOutputDevices() => output.GetOutputDevices();

        public void Play() => output.Play();
        public void Pause() => output.Pause();
        public void Stop() => output.Stop();

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