using ScottPlot.WinForms;
using SignalManipulator.Logic.Models;
using SignalManipulator.UI.Controls.Plottables;
using SignalManipulator.UI.Helpers;
using SignalManipulator.UI.Misc;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.UI.Controls.Viewers
{
    [ExcludeFromCodeCoverage]
    public partial class WaveformViewerControl : BaseViewerControl
    {
        // Window config
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
            stereoMixRadBtn.CheckedChanged += (_, e) => ToggleChecks();
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
            ToggleChecks(); // Hide or show streams
            ClearBuffers();  // Start from scratch
        }

        protected override void ResetUI()
        {
            // Radio buttonds and numeric up-down
            stereoMixRadBtn.Checked = true;
            stereoSplitRadBtn.Checked = false;
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
            waveformPlots.ForEach(w => w.ResampleBuffer(SampleRate));
        }

        protected override void ClearBuffers()
        {
            lock (RenderLock)
                waveformPlots.ForEach(x => x.ClearBuffer());
            NeedsRender = true;
        }

        private void ToggleChecks()
        {
            bool stereroMix = stereoMixRadBtn.Checked;

            lock (RenderLock)
            {
                waveformPlots[0].IsVisible = stereroMix; // Stereo
                waveformPlots[1].IsVisible = !stereroMix;  // Left
                waveformPlots[2].IsVisible = !stereroMix;  // Right
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
            waveformPlots.ForEach(x => x.UpdatePeriod(windowSeconds));
            AxisNavigator.SetCapacity(SampleRate * windowSeconds);
        }
    }
}