using SignalManipulator.Logic.Core;
using System;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    public partial class AudioRouterControl : UserControl
    {
        private AudioRouter audioRouter;

        public AudioRouterControl()
        {
            InitializeComponent();
        }

        private void AudioRouterControl_Load(object sender, EventArgs e)
        {
            audioRouter = AudioEngine.Instance.AudioRouter;
            LoadOutputDevices();
        }

        private void LoadOutputDevices()
        {
            devicesCmbx.Items.Clear(); // Clear list
            devicesCmbx.Items.AddRange(audioRouter.GetOutputDevices()); // Add devices
            devicesCmbx.SelectedIndex = 0; // Select the first one as default
        }

        private void devicesCmbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            audioRouter.ChangeDevice(devicesCmbx.SelectedIndex);
        }
    }
}
