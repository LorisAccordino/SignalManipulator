using SignalManipulator.UI.Helpers;
using ScottPlot.WinForms;
using SignalManipulator.UI.Misc;
using SignalManipulator.UI.Controls.User.Plottables;
using SignalManipulator.UI.Controls.User.Plottables.Scatters;
using SignalManipulator.UI.Controls.User.Viewers;
using SignalManipulator.Logic.Data;
using SignalManipulator.Logic.Data.Channels;

namespace SignalManipulator.UI.Controls.Viewers
{
    public partial class LissajousViewer : BaseViewer
    {
        
        private const int MAX_SAMPLES = 1024;

        // Component references
        private Lissajous lissajousPlot;
        private AxisNavigator navigator = new AxisNavigator(1);
        protected override AxisNavigator AxisNavigator => navigator;
        protected override FormsPlot FormsPlot => formsPlot;

        public LissajousViewer()
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
                lissajousPlot.Clear();
            NeedsRender = true;
        }

        protected override void ProcessFrame(AnalyzedAudioSlice frame)
        {
            lissajousPlot.AddSamples(frame.Waveform.DoubleSamples[AudioChannel.Stereo]);
        }
    }
}