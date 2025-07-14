using NAudio.Wave;

namespace SignalManipulator.Logic.Core.Routing
{
    public class AudioRouter
    {
        public event EventHandler? PlaybackStopped;

        private Dictionary<int, WaveOutEvent> devices = new Dictionary<int, WaveOutEvent>();
        private int currentDeviceIndex = -1;
        private bool wasPlaying = false;
        private bool playbackAlreadyStopped = false;

        private WaveOutEvent CurrentDevice
        {
            get
            {
                if (currentDeviceIndex < 0 || !devices.ContainsKey(currentDeviceIndex))
                    throw new InvalidOperationException("No device selected");
                return devices[currentDeviceIndex];
            }
        }

        public PlaybackState PlaybackState => CurrentDevice.PlaybackState;

        // Init the output based on the provider, overwriting the previous ones
        public void InitOutputs(ISampleProvider outputProvider) 
            => InitOutputs(outputProvider.ToWaveProvider());
        public void InitOutputs(IWaveProvider outputProvider)
        {
            if (outputProvider == null) return;

            // Dispose old devices
            foreach (var w in devices.Values)
            {
                w.PlaybackStopped -= OnPlaybackStopped;
                w.Dispose();
            }
            devices.Clear();
            playbackAlreadyStopped = false;
            wasPlaying = false;

            // Output
            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
                devices[i] = new WaveOutEvent() { DesiredLatency = 150, NumberOfBuffers = 3, DeviceNumber = i };
                devices[i].Init(outputProvider);
                devices[i].PlaybackStopped += OnPlaybackStopped;
            }

            // Select the first one as default
            ChangeDevice(0);
        }

        public void ChangeDevice(int newDevice)
        {
            if (devices.Count <= 0 || !devices.ContainsKey(newDevice))
                return;

            // Save previous state
            if (currentDeviceIndex >= 0)
            {
                var oldDevice = CurrentDevice;
                wasPlaying = oldDevice.PlaybackState == PlaybackState.Playing;
                oldDevice.Pause();
            }

            currentDeviceIndex = newDevice;

            // Resume only if it was playing
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
            return Enumerable.Range(0, WaveOut.DeviceCount)
                             .Select(i => $"{i}: {WaveOut.GetCapabilities(i).ProductName}")
                             .ToArray();
        }

        public void Dispose()
        {
            foreach (var w in devices.Values)
            {
                w.PlaybackStopped -= OnPlaybackStopped;
                w.Dispose();
            }
            devices.Clear();
        }
    }
}