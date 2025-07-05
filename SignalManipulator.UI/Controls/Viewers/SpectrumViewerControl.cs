using ScottPlot;
using ScottPlot.Collections;
using ScottPlot.Plottables;
using ScottPlot.WinForms;
using SignalManipulator.Logic.AudioMath.Smoothing;
using SignalManipulator.Logic.Models;
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

        // FFT data
        private SmootherSMA smootherSMA = new SmootherSMA(1);
        private SmootherEMA smootherEMA = new SmootherEMA(0.0);
        private readonly CircularBuffer<double> audioBuffer = new CircularBuffer<double>(FFT_SIZE);
        private double[] magnitudes = new double[FFT_SIZE];

        // Plotting
        private Signal spectrumPlot;
        //private volatile bool needsRender = false;

        // FFT properties
        private double BinSize => (double)SampleRate / FFT_SIZE;
        private int MaxFrequency => SampleRate / 2;

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
            smaNum.ValueChanged += (s, e) => { smootherSMA.SetHistoryLength((int)smaNum.Value); };
            emaNum.ValueChanged += (s, e) => { smootherEMA.SetAlpha((double)emaNum.Value); };
        }

        protected override void InitializePlot()
        {
            Plot.Title("FFT Spectrum");
            Plot.XLabel("Frequency (Hz)"); Plot.YLabel("Magnitude (dB)");

            // Set plotting
            spectrumPlot = Plot.Add.Signal(magnitudes);

            // Set the bounds
            Plot.Axes.SetLimitsY(0, MAX_MAGNITUDE_DB);
            AxisNavigator.SetCapacity(MaxFrequency);

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
            {
                audioBuffer.Clear();
                while (!audioBuffer.IsFull) audioBuffer.Add(0);

                Array.Clear(magnitudes);
            }

            NeedsRender = true;
        }

        protected override void ProcessFrame(WaveformFrame frame)
        {
            foreach (var sample in frame.DoubleMono)
                audioBuffer.Add(sample);

            // Get magnitudes computing the FFT from waveform
            var fft = FFTFrame.FromWaveform(audioBuffer.ToArray(), SampleRate);

            // Smooth values
            double[] emaSmoothed = smootherEMA.Smooth(fft.Magnitudes);
            double[] smaSmoothed = smootherSMA.Smooth(emaSmoothed);

            // Clamp values
            smaSmoothed.CopyTo(magnitudes, 0);
        }

        protected override void UpdateDataPeriod()
        {
            spectrumPlot.Data.Period = BinSize;
            AxisNavigator.SetCapacity(MaxFrequency);
        }
    }
}