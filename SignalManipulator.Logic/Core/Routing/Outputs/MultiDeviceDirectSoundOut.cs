using NAudio.Wave;

namespace SignalManipulator.Logic.Core.Routing.Outputs
{
    public class MultiDeviceDirectSoundOut : MultiDeviceOut<DirectSoundOut>
    {
        protected override DirectSoundOut CreateOutputDevice(int deviceIndex, IWaveProvider provider)
        {
            var deviceGuid = DirectSoundOut.Devices.ElementAt(deviceIndex).Guid;
            var output = new DirectSoundOut(deviceGuid, 65); // Latency in ms
            output.Init(provider);
            return output;
        }

        protected override int DeviceCount => DirectSoundOut.Devices.Count();

        protected override string GetDeviceName(int index) => DirectSoundOut.Devices.ElementAt(index).Description;
    }
}
