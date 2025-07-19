using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace SignalManipulator.Logic.Core.Routing.Drivers
{
    public static class AudioDriverHelper
    {
        public static readonly AudioDriverType[] PreferredOrder = new[]
        {
            AudioDriverType.WaveOut,
            AudioDriverType.DirectSound,
            AudioDriverType.Wasapi,
            AudioDriverType.Asio
        };

        public static AudioDriverType[] GetAvailableDrivers()
        {
            var available = new List<AudioDriverType>();

            if (WaveOut.DeviceCount > 0)
                available.Add(AudioDriverType.WaveOut);

            if (DirectSoundOut.Devices.Count() > 0)
                available.Add(AudioDriverType.DirectSound);

            if (new MMDeviceEnumerator().EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active).Count > 0)
                available.Add(AudioDriverType.Wasapi);

            if (AsioOut.GetDriverNames().Length > 0)
                available.Add(AudioDriverType.Asio);

            return available.ToArray();
        }
    }
}