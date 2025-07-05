using System.Windows.Forms;

namespace SignalManipulator.UI.Misc
{
    public partial class ZoomPanControl : UserControl
    {
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

        // Wrapping Navigator properties
        public AxisNavigator Navigator => navigator;
        private AxisNavigator navigator = new AxisNavigator(1);

        
        public double Zoom { get => navigator.Zoom; set => navigator.SetZoom(zoomSlider.Value = value); }
        public double Pan { get => navigator.Pan; set => navigator.SetPan(panSlider.Value = value); }

        public ZoomPanControl()
        {
            InitializeComponent();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            zoomSlider.ValueChanged += (_, v) => { navigator.SetZoom(v); };
            panSlider.ValueChanged += (_, v) => { navigator.SetPan(v); };
        }
    }
}
