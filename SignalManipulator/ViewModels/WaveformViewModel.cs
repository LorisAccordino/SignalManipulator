using ScottPlot.WinForms;
using SignalManipulator.Logic.Data;
using SignalManipulator.Logic.Data.Channels;
using SignalManipulator.UI.Helpers;
using SignalManipulator.UI.Misc;
using SignalManipulator.UI.Plottables;
using SignalManipulator.UI.Plottables.Signals;

namespace SignalManipulator.ViewModels
{
    public partial class WaveformViewModel : BaseViewModel
    {
        // Waveform plots
        private List<Waveform> waveformPlots = new List<Waveform>();

        // Component references
        protected override FormsPlot FormsPlot => formsPlot;
        protected override AxisNavigator AxisNavigator => navigator.Navigator;

        public WaveformViewModel()
        {
            InitializeComponent();

            if (!DesignModeHelper.IsDesignMode)
                InitializeViewer(); // Manual call to the runtime setup
        }

        protected override void InitializeEvents()
        {
            // Other events
            secNum.ValueChanged += (_, v) => UpdateDataPeriod();
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

            // Set channel modes
            waveformPlots[0].Channel = AudioChannel.Mono;
            waveformPlots[1].Channel = AudioChannel.Left;
            waveformPlots[2].Channel = AudioChannel.Right;

            // Set the bounds
            Plot.Axes.SetLimitsY(-1, 1);
            UpdateDataPeriod();

            // Force the update
            NeedsRender = true;
            RenderPlot();

            // Hide or show streams
            ToggleChecks();
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

        protected override void ResizeBuffers()
        {
            waveformPlots.ForEach(w => w.ResizeBuffer(SampleRate));
        }

        protected override void ClearBuffers()
        {
            lock (RenderLock)
                waveformPlots.ForEach(x => x.Clear());
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

        protected override void ProcessData(AnalyzedAudioSlice data)
        {
            // Add samples to each channel
            waveformPlots.ForEach(w => w.AddData(data.Waveform));
        }

        protected override void UpdateDataPeriod()
        {
            int seconds = (int)secNum.Value;
            waveformPlots.ForEach(p => p.UpdatePeriod(seconds));
            AxisNavigator.SetCapacity(SampleRate * seconds);
        }
    }
}