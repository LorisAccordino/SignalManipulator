using ScottPlot;
using ScottPlot.WinForms;
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
        private const int FFT_SIZE = 8192;                  // Must be power of 2
        private const int MAX_MAGNITUDE_DB = 125;

        // Spectrum plots
        private List<SpectrumPlot> spectrumPlots;

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
        }

        protected override void InitializePlot()
        {
            Plot.Title("FFT Spectrum");
            Plot.XLabel("Frequency (Hz)"); Plot.YLabel("Magnitude (dB)");

            // Set plotting
            spectrumPlots[0] = Plot.Add.Spectrum(SampleRate, FFT_SIZE, "Stereo");
            spectrumPlots[1] = Plot.Add.Spectrum(SampleRate, FFT_SIZE, "Left");
            spectrumPlots[2] = Plot.Add.Spectrum(SampleRate, FFT_SIZE, "Right");

            // Set the bounds
            Plot.Axes.SetLimitsY(0, MAX_MAGNITUDE_DB);
            UpdateDataPeriod();

            // Force the update
            NeedsRender = true;
            RenderPlot();

            // Clear buffers
            ClearBuffers();
        }

        protected override void ResetUI()
        {
            // Checkbox and numeric up-down
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