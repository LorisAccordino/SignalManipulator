using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.AudioMath;
using System.Drawing;
using SignalManipulator.UI.Helpers;
using System.Diagnostics.CodeAnalysis;
using ScottPlot.Collections;
using SignalManipulator.Logic.Models;
using ScottPlot.WinForms;
using SignalManipulator.UI.Misc;

namespace SignalManipulator.UI.Controls.Viewers
{
    [ExcludeFromCodeCoverage]
    public partial class LissajousViewerControl : BaseViewerControl
    {
        private const int MAX_SAMPLES = 1024;
        private readonly double[] left = new double[MAX_SAMPLES];
        private readonly double[] right = new double[MAX_SAMPLES];
        private CircularBuffer<double> interleavedBuffer = new CircularBuffer<double>(MAX_SAMPLES * 2);

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
            var xyPlot = Plot.Add.Scatter(left, right);
            xyPlot.MarkerSize = 0; // Don't draw markers
            xyPlot.LineWidth = 1; // Thick enough
            
            // Clear buffers
            ClearBuffers();
        }

        protected override void ClearBuffers()
        {
            lock (RenderLock)
            {
                interleavedBuffer.Clear();
                Array.Clear(left, 0, left.Length);
                Array.Clear(right, 0, right.Length);
            }

            // Force render
            NeedsRender = true;
        }

        protected override void ProcessFrame(WaveformFrame frame)
        {
            foreach (var sample in frame.DoubleStereo)
                interleavedBuffer.Add(sample);

            if (interleavedBuffer.Count < MAX_SAMPLES * 2)
                return;

            interleavedBuffer.ToArray().SplitStereo(left, right);
        }

        private void Plot_Resize(object sender, EventArgs e)
        {
            formsPlot.Size = new Size(formsPlot.Height, formsPlot.Height);
            formsPlot.Location = new Point((Width - formsPlot.Width) / 2, (Height - formsPlot.Height) / 2);
            NeedsRender = true;
        }
    }
}