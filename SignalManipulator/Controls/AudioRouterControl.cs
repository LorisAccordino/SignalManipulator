using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Core.Routing;
using SignalManipulator.UI.Helpers;

namespace SignalManipulator.Controls
{
    public partial class AudioRouterControl : UserControl
    {
        private AudioRouter audioRouter;

        public AudioRouterControl()
        {
            InitializeComponent();

            if (!DesignModeHelper.IsDesignMode)
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