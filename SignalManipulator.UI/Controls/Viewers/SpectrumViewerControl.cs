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
        private SpectrumPlot spectrumPlot;

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
            smaNum.ValueChanged += (s, e) => { spectrumPlot.SetSMA((int)smaNum.Value); };
            emaNum.ValueChanged += (s, e) => { spectrumPlot.SetEMA((int)emaNum.Value); };
        }

        protected override void InitializePlot()
        {
            Plot.Title("FFT Spectrum");
            Plot.XLabel("Frequency (Hz)"); Plot.YLabel("Magnitude (dB)");

            // Set plotting
            spectrumPlot = Plot.Add.Spectrum(SampleRate, FFT_SIZE);

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
                spectrumPlot.ClearBuffer();
            NeedsRender = true;
        }

        protected override void ProcessFrame(WaveformFrame frame)
        {
            spectrumPlot.AddSamples(frame.DoubleMono);
        }

        protected override void UpdateDataPeriod()
        {
            spectrumPlot.UpdatePeriod(SampleRate);
            AxisNavigator.SetCapacity(SampleRate / 2);
        }
    }
}