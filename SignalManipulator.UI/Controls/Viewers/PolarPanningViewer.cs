using System.Drawing;
using SignalManipulator.UI.Helpers;
using System.Diagnostics.CodeAnalysis;
using SignalManipulator.Logic.Models;
using ScottPlot.WinForms;
using SignalManipulator.UI.Misc;
using SignalManipulator.UI.Controls.Plottables;

namespace SignalManipulator.UI.Controls.Viewers
{
    [ExcludeFromCodeCoverage]
    public partial class PolarPanningViewer : BaseViewerControl
    {
        private PolarPanningPlot panningPlot;

        // Component references
        protected override FormsPlot FormsPlot => formsPlot;
        private AxisNavigator navigator = new AxisNavigator(1);
        protected override AxisNavigator AxisNavigator => navigator;

        public PolarPanningViewer()
        {
            InitializeComponent();

            if (!DesignModeHelper.IsDesignMode)
                InitializeViewer(); // Manual call to the runtime setup
        }

        protected override void InitializePlot()
        {
            // Init plot
            Plot.Title("Polar Panning");
            //Plot.XLabel("Left"); Plot.YLabel("Right");
            Plot.Axes.SquareUnits();
            Plot.Axes.SetLimits(-1, 1, -1, 1);
            AxisNavigator.Recalculate(); // Ensure to block the limits

            // Setup polar panning plot
            panningPlot = new PolarPanningPlot(Plot); 
            Plot.Add.Plottable(panningPlot);
        }

        protected override void ClearBuffers()
        {
            lock (RenderLock)
                panningPlot.ClearBuffer();
            NeedsRender = true;
        }

        protected override void ProcessFrame(WaveformFrame frame)
        {
            panningPlot.AddSamples(frame.DoubleStereo);
        }

        private void Plot_Resize(object sender, EventArgs e)
        {
            formsPlot.Size = new Size(formsPlot.Height, formsPlot.Height);
            formsPlot.Location = new Point((Width - formsPlot.Width) / 2, (Height - formsPlot.Height) / 2);
            NeedsRender = true;
        }
    }
}