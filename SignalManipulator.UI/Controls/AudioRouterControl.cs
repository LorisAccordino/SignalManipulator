using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Core.Routing;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    public partial class AudioRouterControl : UserControl
    {
        private IAudioRouter audioRouter;

        public AudioRouterControl()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                audioRouter = AudioEngine.Instance.AudioRouter;
                LoadOutputDevices();
            }
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