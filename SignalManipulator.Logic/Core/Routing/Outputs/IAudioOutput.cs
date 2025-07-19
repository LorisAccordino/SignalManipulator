using NAudio.Wave;

namespace SignalManipulator.Logic.Core.Routing.Outputs
{
    public interface IAudioOutput : IWavePlayer
    {
        void ChangeDevice(int index);
        string[] GetOutputDevices();
    }
}