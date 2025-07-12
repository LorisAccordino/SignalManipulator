using SignalManipulator.UI.Helpers;
using ScottPlot.WinForms;
using SignalManipulator.UI.Misc;
using SignalManipulator.Logic.Data;
using SignalManipulator.UI.Plottables;
using SignalManipulator.UI.Plottables.Radars;

namespace SignalManipulator.ViewModels
{
    public partial class SurroundAnalyzerViewModel : BaseViewModel
    {
        // Component references
        private SurroundAnalyzer surroundAnalyzer;
        private AxisNavigator navigator = new AxisNavigator();
        protected override AxisNavigator AxisNavigator => navigator;
        protected override FormsPlot FormsPlot => formsPlot;

        public SurroundAnalyzerViewModel()
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