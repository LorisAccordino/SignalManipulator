using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Viewers;
using System.ComponentModel;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    public partial class LissajousViewerControl : UserControl
    {
        private AudioViewer viewer;

        public LissajousViewerControl()
        {
            InitializeComponent();

            if (!LicenseManager.UsageMode.Equals(LicenseUsageMode.Designtime))
            {
                viewer = AudioEngine.Instance.AudioViewer;

                InitializeEvents();
                InitializePlot();
            }
        }

        private void InitializeEvents()
        {

        }

        private void InitializePlot()
        {

        }
    }
}
