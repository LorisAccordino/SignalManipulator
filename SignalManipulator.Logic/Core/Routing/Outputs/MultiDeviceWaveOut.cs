using NAudio.Wave;

namespace SignalManipulator.Logic.Core.Routing.Outputs
{
    public class MultiDeviceWaveOut : MultiDeviceOut<WaveOut>
    {
        protected override WaveOut CreateOutputDevice(int deviceIndex, IWaveProvider provider)
        {
            var output = new WaveOut
            {
                DeviceNumber = deviceIndex,
                DesiredLatency = 150,
                NumberOfBuffers = 3
            };
            output.Init(provider);
            return output;
        }
    }
}