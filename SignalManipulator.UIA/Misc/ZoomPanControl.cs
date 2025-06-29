using System.Windows.Forms;

namespace SignalManipulator.UI.Misc
{
    public partial class ZoomPanControl : UserControl
    {
        public AxisNavigator Navigator = new AxisNavigator(1);

        public double ZoomMin
        {
            get => zoomSlider.Minimum; set => zoomSlider.Minimum = value;
        }

        public double ZoomPrecision
        {
            get => zoomSlider.Precision; set => zoomSlider.Precision = value;
        }

        public double ZoomMax
        {
            get => zoomSlider.Maximum; set => zoomSlider.Maximum = value;
        }

        public ZoomPanControl()
        {
            InitializeComponent();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            zoomSlider.ValueChanged += (_, v) => { Navigator.SetZoom(v); };
            panSlider.ValueChanged += (_, v) => { Navigator.SetPan(v); };
        }
    }
}
