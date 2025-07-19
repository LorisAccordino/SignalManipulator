using NAudio.Wave;

namespace SignalManipulator.Logic.Core.Routing.Outputs
{
    public class MultiDeviceAsioOut : MultiDeviceOut<AsioOut>
    {
        protected override AsioOut CreateOutputDevice(int deviceIndex, IWaveProvider provider)
        {
            var driverName = AsioOut.GetDriverNames()[deviceIndex];
            var output = new AsioOut(driverName);
            output.Init(provider);
            return output;
        }

        protected override int DeviceCount => AsioOut.GetDriverNames().Length;

        protected override string GetDeviceName(int index) => AsioOut.GetDriverNames()[index];

        public override float Volume
        {
            get => throw new NotSupportedException("Volume not supported by ASIO drivers.");
            set => throw new NotSupportedException("Volume not supported by ASIO drivers.");
        }
    }
}