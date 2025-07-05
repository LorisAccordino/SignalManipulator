using System.Drawing;
using SignalManipulator.UI.Helpers;
using System.Diagnostics.CodeAnalysis;
using SignalManipulator.Logic.Models;
using ScottPlot.WinForms;
using SignalManipulator.UI.Misc;
using SignalManipulator.UI.Controls.Plottables.Scatters;
using SignalManipulator.UI.Controls.Plottables;

namespace SignalManipulator.UI.Controls.Viewers
{
    [ExcludeFromCodeCoverage]
    public partial class LissajousViewerControl : BaseViewerControl
    {
        private LissajousPlot lissajousPlot;
        private const int MAX_SAMPLES = 1024;

        // Component references
        protected override FormsPlot FormsPlot => formsPlot;
        private AxisNavigator navigator = new AxisNavigator(1);
        protected override AxisNavigator AxisNavigator => navigator;

        public LissajousViewerControl()
        {
            InitializeComponent();

            if (!DesignModeHelper.IsDesignMode)
                InitializeViewer(); // Manual call to the runtime setup
        }

        protected override void InitializePlot()
        {
            // Init plot
            Plot.Title("XY Stereo Oscilloscope");
            Plot.XLabel("Left"); Plot.YLabel("Right");
            Plot.Axes.SquareUnits();
            Plot.Axes.SetLimits(-1, 1, -1, 1);
            AxisNavigator.Recalculate(); // Ensure to block the limits

            // Setup scatter plot
            lissajousPlot = Plot.Add.Lissajous(MAX_SAMPLES);
        }

        protected override void ClearBuffers()
        {
            lock (RenderLock)
                lissajousPlot.ClearBuffer();
            NeedsRender = true;
        }

        protected override void ProcessFrame(WaveformFrame frame)
        {
            lissajousPlot.AddSamples(frame.DoubleStereo);
        }

        private void Plot_Resize(object sender, EventArgs e)
        {
            formsPlot.Size = new Size(formsPlot.Height, formsPlot.Height);
            formsPlot.Location = new Point((Width - formsPlot.Width) / 2, (Height - formsPlot.Height) / 2);
            NeedsRender = true;
        }
    }
}