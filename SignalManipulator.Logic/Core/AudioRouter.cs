using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalManipulator.Logic.Core
{
    public class AudioRouter
    {
        private Dictionary<int, WaveOutEvent> devices = new Dictionary<int, WaveOutEvent>();
        private int currentDeviceIndex = -1;

        public WaveOutEvent CurrentDevice
        {
            get
            {
                if (currentDeviceIndex < 0 || !devices.ContainsKey(currentDeviceIndex))
                    throw new InvalidOperationException("No device selected");
                return devices[currentDeviceIndex];
            }
        }


        // Init the output based on the provider, overwriting the previous ones
        public void InitOutputs(IWaveProvider outputProvider)
        {
            // Dispose the older WaveOutEvent
            foreach (var w in devices.Values)
                w.Dispose();
            devices.Clear();

            // Output
            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
                devices[i] = new WaveOutEvent() { DesiredLatency = 150, NumberOfBuffers = 3, DeviceNumber = i };
                devices[i].Init(outputProvider);
            }

            // Select as default the first one
            if (devices.Count > 0) ChangeDevice(0);
        }

        public void ChangeDevice(int newDevice)
        {
            if (currentDeviceIndex >= 0)
                devices[currentDeviceIndex].Pause();

            devices[newDevice].Play();
            currentDeviceIndex = newDevice;
        }

        public string[] GetOutputDevices()
        {
            return Enumerable.Range(0, WaveOut.DeviceCount)
                             .Select(i => $"{i}: {WaveOut.GetCapabilities(i).ProductName}")
                             .ToArray();
        }

        public void Dispose()
        {
            foreach (var w in devices.Values) w.Dispose();
            devices.Clear();
        }
    }
}