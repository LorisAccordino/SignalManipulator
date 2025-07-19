using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace SignalManipulator.Logic.Core.Routing.Outputs
{
    public class MultiDeviceWasapiOut : MultiDeviceOut<WasapiOut>
    {
        protected override WasapiOut CreateOutputDevice(int deviceIndex, IWaveProvider provider)
        {
            var device = new MMDeviceEnumerator().EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active)[deviceIndex];
            var output = new WasapiOut(device, AudioClientShareMode.Shared, true, 200);
            output.Init(provider);
            return output;
        }

        protected override int DeviceCount =>
            new MMDeviceEnumerator().EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active).Count;

        protected override string GetDeviceName(int index) =>
            new MMDeviceEnumerator().EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active)[index].FriendlyName;
    }
}