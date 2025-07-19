using NAudio.Wave;

namespace SignalManipulator.Logic.Core.Routing.Outputs
{
    public abstract class MultiDeviceOut<TOut> : IAudioOutput, IDisposable where TOut : IWavePlayer
    {
        public event EventHandler<StoppedEventArgs>? PlaybackStopped;

        private Dictionary<int, TOut> devices = new Dictionary<int, TOut>();
        private int currentDeviceIndex = -1;
        private bool wasPlaying = false;
        private bool playbackAlreadyStopped = false;
        private IWaveProvider? currentWaveProvider;

        protected abstract TOut CreateOutputDevice(int deviceIndex, IWaveProvider provider);

        protected virtual int DeviceCount => WaveOut.DeviceCount;
        protected virtual string GetDeviceName(int index) => WaveOut.GetCapabilities(index).ProductName;

        protected TOut CurrentDevice
        {
            get
            {
                if (currentDeviceIndex < 0 || !devices.TryGetValue(currentDeviceIndex, out var device))
                    throw new InvalidOperationException("No audio output device selected.");
                return device;
            }
        }

        public PlaybackState PlaybackState => CurrentDevice.PlaybackState;

        public virtual float Volume
        {
            get => CurrentDevice.Volume;
            set
            {
                foreach (var device in devices.Values)
                    device.Volume = value;
            }
        }

        public WaveFormat OutputWaveFormat => currentWaveProvider?.WaveFormat ?? throw new InvalidOperationException("Output not initialized");

        public void Init(IWaveProvider waveProvider)
        {
            if (waveProvider == null)
                throw new ArgumentNullException(nameof(waveProvider));

            Dispose(); // Dispose old devices if any

            currentWaveProvider = waveProvider;
            playbackAlreadyStopped = false;
            wasPlaying = false;

            for (int i = 0; i < DeviceCount; i++)
            {
                var output = CreateOutputDevice(i, waveProvider);
                output.PlaybackStopped += OnPlaybackStopped;
                devices[i] = output;
            }

            ChangeDevice(0); // Default to first device
        }

        public void ChangeDevice(int newDeviceIndex)
        {
            if (!devices.ContainsKey(newDeviceIndex))
                return;

            if (currentDeviceIndex >= 0)
            {
                wasPlaying = CurrentDevice.PlaybackState == PlaybackState.Playing;
                CurrentDevice.Pause();
            }

            currentDeviceIndex = newDeviceIndex;

            if (wasPlaying)
                CurrentDevice.Play();
        }

        public void Play() => CurrentDevice.Play();

        public void Pause() => CurrentDevice.Pause();

        public void Stop()
        {
            wasPlaying = false;
            playbackAlreadyStopped = false;
            CurrentDevice.Stop();
        }

        private void OnPlaybackStopped(object? sender, StoppedEventArgs e)
        {
            if (playbackAlreadyStopped) return;
            playbackAlreadyStopped = true;
            PlaybackStopped?.Invoke(sender, e);
        }

        public string[] GetOutputDevices()
        {
            return Enumerable.Range(0, DeviceCount)
                             .Select(i => $"{i}: {GetDeviceName(i)}")
                             .ToArray();
        }

        public void Dispose()
        {
            foreach (var device in devices.Values)
            {
                device.PlaybackStopped -= OnPlaybackStopped;
                device.Dispose();
            }
            devices.Clear();
        }
    }
}