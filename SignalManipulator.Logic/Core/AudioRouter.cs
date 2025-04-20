using NAudio.Wave;
using System.Collections.Generic;

namespace SignalManipulator.Logic.Core
{
    public class AudioRouter
    {
        // Outputs
        public WaveOutEvent CurrentDevice => outputDevices[currentDeviceIndex];
        private Dictionary<int, WaveOutEvent> outputDevices = new Dictionary<int, WaveOutEvent>();
        private int currentDeviceIndex = -1;

        // AudioEngine reference
        private AudioEngine audioEngine;

        public AudioRouter(AudioEngine audioEngine)
        {
            this.audioEngine = audioEngine;
            InitOutputs();
            ChangeDevice(0);
        }

        public void InitOutputs()
        {
            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
                var wo = new WaveOutEvent { DeviceNumber = i };
                wo.Init(audioEngine.BufferedWaveProvider);

                outputDevices[i] = wo;
                //providers[i] = bufferedWaveProvider;
            }
        }

        public void ChangeDevice(int newDevice)
        {
            if (currentDeviceIndex >= 0)
                outputDevices[currentDeviceIndex].Pause();

            outputDevices[newDevice].Play();
            currentDeviceIndex = newDevice;
        }

        public string[] GetOutputDevices()
        {
            string[] devices = new string[WaveOut.DeviceCount];
            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
                var caps = WaveOut.GetCapabilities(i);
                devices[i] = $"{i}: {caps.ProductName}";
            }
            return devices;
        }
    }
}
