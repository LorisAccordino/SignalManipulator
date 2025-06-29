using NAudio.Wave;
using System;

namespace SignalManipulator.Logic.Core.Routing
{
    public interface IAudioRouter : IDisposable
    {
        event EventHandler PlaybackStopped;

        WaveOutEvent CurrentDevice { get; }

        void InitOutputs(ISampleProvider outputProvider);
        void InitOutputs(IWaveProvider outputProvider);
        void ChangeDevice(int newDevice);
        string[] GetOutputDevices();
    }
}