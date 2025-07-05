using ScottPlot.WinForms;
using SignalManipulator.Logic.Models;
using SignalManipulator.UI.Controls.Plottables;
using SignalManipulator.UI.Helpers;
using SignalManipulator.UI.Misc;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls.Viewers
{
    [ExcludeFromCodeCoverage]
    public partial class WaveformViewerControl : BaseViewerControl
    {
        // Window config
        private const int MAX_WINDOWS_SECONDS = 10;       // Maximum limit
        private int windowSeconds = 1;                    // Current shown seconds

        // Waveform plots
        private List<WaveformPlot> waveformPlots = new List<WaveformPlot>();

        // Component references
        protected override FormsPlot FormsPlot => formsPlot;
        protected override AxisNavigator AxisNavigator => navigator.Navigator;

        public WaveformViewerControl()
        {
            InitializeComponent();

            if (!DesignModeHelper.IsDesignMode)
                InitializeViewer(); // Manual call to the runtime setup
        }

        protected override void InitializeEvents()
        {
            // Other events
            secNum.ValueChanged += (_, v) => { windowSeconds = (int)secNum.Value; UpdateDataPeriod(); };
            secNum.Maximum = MAX_WINDOWS_SECONDS;
            monoCheckBox.CheckedChanged += (_, e) => ToggleStreams();
        }

        protected override void InitializePlot()
        {
            // Init plot
            Plot.Title("Signal Waveform");
            Plot.XLabel("Time (samples)"); Plot.YLabel("Amplitude");

            // Add each waveform to the plot
            waveformPlots.Add(Plot.Add.Waveform(SampleRate, "Stereo"));
            waveformPlots.Add(Plot.Add.Waveform(SampleRate, "Left"));
            waveformPlots.Add(Plot.Add.Waveform(SampleRate, "Right"));
            Plot.ShowLegend();

            // Set the bounds
            Plot.Axes.SetLimitsY(-1, 1);
            UpdateDataPeriod();

            // Force the update
            NeedsRender = true;
            RenderPlot();

            // Final setups
            ToggleStreams(); // Hide or show streams
            ClearBuffers();  // Start from scratch
        }

        protected override void ResetUI()
        {
            // Checkbox and numeric up-down
            monoCheckBox.CheckState = CheckState.Unchecked;
            secNum.Value = 1;

            // Navigator
            navigator.Zoom = 1;
            navigator.Pan = 0;
        }

        protected override void EnableUI(bool enable)
        {
            settingsPanel.Enabled = enable;
        }

        protected override void ResampleBuffers()
        {
            // Resample the waveforms
            foreach (var w in waveformPlots)
                w.ResampleBuffer(SampleRate);
        }

        protected override void ClearBuffers()
        {
            lock (RenderLock)
            {
                // Clear waveform buffers
                foreach (var w in waveformPlots)
                    w.ClearBuffer();
            }

            NeedsRender = true;
        }

        private void ToggleStreams()
        {
            bool mono = monoCheckBox.Checked;

            lock (RenderLock)
            {
                waveformPlots[0].IsVisible = !mono; // Stereo
                waveformPlots[1].IsVisible = mono;  // Left
                waveformPlots[2].IsVisible = mono;  // Right
            }

            NeedsRender = true;
        }

        protected override void ProcessFrame(WaveformFrame frame)
        {
            // Add samples to each channel
            waveformPlots[0].AddSamples(frame.DoubleMono);
            waveformPlots[1].AddSamples(frame.DoubleSplitStereo.Left);
            waveformPlots[2].AddSamples(frame.DoubleSplitStereo.Right);
        }

        protected override void UpdateDataPeriod()
        {
            foreach (var w in waveformPlots)
                w.UpdatePeriod(windowSeconds);

            AxisNavigator.SetCapacity(SampleRate * windowSeconds);
        }
    }
}