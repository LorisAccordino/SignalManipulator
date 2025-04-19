using NAudio.Wave;
using System;
using System.Windows.Forms;

namespace SignalManipulator.UI.Modules
{
    public partial class AudioRouter : UserControl
    {
        public static AudioRouter Instance { get; private set; }

        public AudioRouter()
        {
            Instance = this;
            InitializeComponent();
            LoadOutputDevices();
        }

        private void LoadOutputDevices()
        {
            devicesCmbx.Items.Clear();
            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
                var caps = WaveOut.GetCapabilities(i);
                devicesCmbx.Items.Add($"{i}: {caps.ProductName}");
            }
            devicesCmbx.SelectedIndex = 0; // Select the first one as default
        }

        private void ChangeOutputDevice(int newDeviceIndex)
        {
            AudioPlayer.Instance.ChangeDevice(newDeviceIndex);
        }

        private void devicesCmbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeOutputDevice(devicesCmbx.SelectedIndex);
        }
    }
}
