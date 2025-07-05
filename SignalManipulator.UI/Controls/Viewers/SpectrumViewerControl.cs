using ScottPlot;
using ScottPlot.WinForms;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Models;
using SignalManipulator.UI.Controls.Plottables;
using SignalManipulator.UI.Helpers;
using SignalManipulator.UI.Misc;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.UI.Controls.Viewers
{
    [ExcludeFromCodeCoverage]
    public partial class SpectrumViewerControl : BaseViewerControl
    {
        // FFT configuration and visualization
        //private const int FFT_SIZE = 8192;                  // Must be power of 2
        private const int MAX_MAGNITUDE_DB = 125;

        // Spectrum plots
        private List<SpectrumPlot> spectrumPlots = new List<SpectrumPlot>();

        // Component references
        protected override FormsPlot FormsPlot => formsPlot;
        protected override AxisNavigator AxisNavigator => navigator.Navigator;

        public SpectrumViewerControl()
        {
            InitializeComponent();

            if (!DesignModeHelper.IsDesignMode)
                InitializeViewer(); // Manual call to the runtime setup
        }

        protected override void InitializeEvents()
        {
            // Other events
            smaNum.ValueChanged += (s, e) => { spectrumPlots.ForEach(s => s.SetSMA((int)smaNum.Value)); };
            emaNum.ValueChanged += (s, e) => { spectrumPlots.ForEach(s => s.SetEMA((int)emaNum.Value)); };

            stereoMixRadBtn.CheckedChanged += (_, e) => ToggleChecks();
        }

        protected override void InitializePlot()
        {
            Plot.Title("FFT Spectrum");
            Plot.XLabel("Frequency (Hz)"); Plot.YLabel("Magnitude (dB)");

            // Set plotting
            spectrumPlots.Add(Plot.Add.Spectrum(SampleRate, AudioEngine.FFT_SIZE, "Stereo"));
            spectrumPlots.Add(Plot.Add.Spectrum(SampleRate, AudioEngine.FFT_SIZE, "Left"));
            spectrumPlots.Add(Plot.Add.Spectrum(SampleRate, AudioEngine.FFT_SIZE, "Right"));

            // Set the bounds
            Plot.Axes.SetLimitsY(0, MAX_MAGNITUDE_DB);
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
            // Radio buttons and numeric up-downs
            stereoMixRadBtn.Checked = true;
            stereoSplitRadBtn.Checked = false;
            smaNum.Value = 1;
            emaNum.Value = 0M;

            // Navigator
            navigator.Zoom = 1;
            navigator.Pan = -1;
        }

        protected override void EnableUI(bool enable)
        {
            settingsPanel.Enabled = enable;
        }

        protected override void ClearBuffers()
        {
            lock (RenderLock)
                spectrumPlots.ForEach(s => s.ClearBuffer());
            NeedsRender = true;
        }

        private void ToggleChecks()
        {
            bool stereroMix = stereoMixRadBtn.Checked;

            lock (RenderLock)
            {
                spectrumPlots[0].IsVisible = stereroMix; // Stereo
                spectrumPlots[1].IsVisible = !stereroMix;  // Left
                spectrumPlots[2].IsVisible = !stereroMix;  // Right
            }

            NeedsRender = true;
        }

        protected override void ProcessFrame(WaveformFrame frame)
        {
            spectrumPlots[0].AddSamples(frame.DoubleMono);
            spectrumPlots[1].AddSamples(frame.DoubleSplitStereo.Left);
            spectrumPlots[2].AddSamples(frame.DoubleSplitStereo.Right);
        }

        protected override void UpdateDataPeriod()
        {
            spectrumPlots.ForEach(s => s.UpdatePeriod(SampleRate));
            AxisNavigator.SetCapacity(SampleRate / 2);
        }
    }
}