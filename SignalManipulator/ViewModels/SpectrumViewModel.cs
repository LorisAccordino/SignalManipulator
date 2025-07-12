using ScottPlot;
using ScottPlot.WinForms;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Data;
using SignalManipulator.Logic.Data.Channels;
using SignalManipulator.UI.Helpers;
using SignalManipulator.UI.Misc;
using SignalManipulator.UI.Plottables;
using SignalManipulator.UI.Plottables.Signals;

namespace SignalManipulator.ViewModels
{
    public partial class SpectrumViewModel : BaseViewModel
    {
        // Spectrum plots
        private List<Spectrum> spectrumPlots = new List<Spectrum>();

        // Component references
        protected override FormsPlot FormsPlot => formsPlot;
        protected override AxisNavigator AxisNavigator => navigator.Navigator;

        public SpectrumViewModel()
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
            fftSizeCmbx.Text = AudioEngine.FFT_SIZE.ToString();
            fftSizeCmbx.SelectedIndexChanged += (_, e) => spectrumPlots.ForEach(s => s.ResizeBuffer(AudioEngine.CurrentFFTSize = int.Parse(fftSizeCmbx.Text)));
        }

        protected override void InitializePlot()
        {
            Plot.Title("FFT Spectrum");
            Plot.XLabel("Frequency (Hz)"); Plot.YLabel("Magnitude (dB)");

            // Set plotting
            spectrumPlots.Add(Plot.Add.Spectrum(SampleRate, AudioEngine.FFT_SIZE, "Stereo"));
            spectrumPlots.Add(Plot.Add.Spectrum(SampleRate, AudioEngine.FFT_SIZE, "Left"));
            spectrumPlots.Add(Plot.Add.Spectrum(SampleRate, AudioEngine.FFT_SIZE, "Right"));

            // Set channel modes
            spectrumPlots[0].Channel = AudioChannel.Stereo;
            spectrumPlots[1].Channel = AudioChannel.Left;
            spectrumPlots[2].Channel = AudioChannel.Right;

            // Set the bounds
            Plot.Axes.SetLimitsY(0, AudioEngine.MAX_MAGNITUDE_DB);
            UpdateDataPeriod();

            // Force the update
            NeedsRender = true;
            RenderPlot();

            // Hide or show streams
            ToggleChecks();
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
                spectrumPlots.ForEach(s => s.Clear());
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

        protected override void ProcessFrame(AnalyzedAudioSlice frame)
        {
            spectrumPlots.ForEach(s => s.AddData(frame.FFT));
        }

        protected override void UpdateDataPeriod()
        {
            spectrumPlots.ForEach(s => s.UpdatePeriod(SampleRate));
            AxisNavigator.SetCapacity(SampleRate / 2);
        }
    }
}