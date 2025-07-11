using SignalManipulator.UI.Helpers;
using System.Diagnostics.CodeAnalysis;
using ScottPlot.WinForms;
using SignalManipulator.UI.Misc;
using SignalManipulator.UI.Controls.User.Plottables;
using SignalManipulator.UI.Controls.User.Plottables.Radars;
using SignalManipulator.UI.Controls.User.Viewers;
using SignalManipulator.Logic.Data;

namespace SignalManipulator.UI.Controls.Viewers
{
    [ExcludeFromCodeCoverage]
    public partial class SurroundAnalyzerViewer : BaseViewer
    {
        // Component references
        private SurroundAnalyzer surroundAnalyzer;
        private AxisNavigator navigator = new AxisNavigator();
        protected override AxisNavigator AxisNavigator => navigator;
        protected override FormsPlot FormsPlot => formsPlot;

        public SurroundAnalyzerViewer()
        {
            InitializeComponent();

            if (!DesignModeHelper.IsDesignMode)
                InitializeViewer(); // Manual call to the runtime setup
        }

        protected override void InitializePlot()
        {
            // Init plot
            Plot.Title("Surround Analyzer");
            //Plot.XLabel("Left"); Plot.YLabel("Right");
            Plot.Axes.SquareUnits();
            Plot.Axes.SetLimits(-1, 1, -1, 1);
            AxisNavigator.Recalculate(); // Ensure to block the limits

            // Setup surround analyzer
            surroundAnalyzer = Plot.Add.SurroundAnalizer();
        }

        protected override void ClearBuffers()
        {
            lock (RenderLock)
                surroundAnalyzer.Clear();
            NeedsRender = true;
        }

        protected override void ProcessFrame(AnalyzedAudioSlice frame)
        {
            surroundAnalyzer.AddData(frame.Volume);
        }
    }
}