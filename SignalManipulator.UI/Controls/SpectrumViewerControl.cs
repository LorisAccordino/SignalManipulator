using ScottPlot;
using ScottPlot.Plottables;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Utils;
using SignalManipulator.Logic.Viewers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    public partial class SpectrumViewerControl : UserControl
    {
        private AudioVisualizer viewer;
        private DataLogger spectrumPlot;
        private List<double> waveformBuffer = new List<double>();
        //private List<FrequencySpectrum> spectrumBuffer = new List<FrequencySpectrum>();
        private FrequencySpectrum spectrum; // Spectrum: freqs, magnitudes
        private object lockObject = new object();

        private const int MAX_HZ = 20000;
        private const int MAX_DB = 125;

        private double[] smoothedMagnitudes;
        private double[] lastFrequencies;
        private const double SMOOTHING_FACTOR = 0.85; // from 0 to 1 (higher = smoother)

        public SpectrumViewerControl()
        {
            InitializeComponent();

            if (!LicenseManager.UsageMode.Equals(LicenseUsageMode.Designtime))
            {
                viewer = AudioEngine.Instance.AudioViewer;

                InitializeEvents();
                InitializePlot();
            }
        }

        private void InitializeEvents()
        {
            viewer.OnStarted += ResetPlot;
            viewer.OnStopped += ResetPlot;
            viewer.OnUpdate += UpdatePlot;
            //viewer.OnSpectrumUpdated += UpdatePlotData;
            viewer.OnFrameAvailable += (frame) => UpdatePlotData(frame.DoubleMono);
        }

        private void InitializePlot()
        {
            formsPlot.Plot.Title("FFT Spectrum");
            formsPlot.Plot.XLabel("Frequency (Hz)");
            formsPlot.Plot.YLabel("Magnitude (dB)");
            formsPlot.UserInputProcessor.Disable();

            ResetPlot();
        }

        private void ResetPlot()
        {
            // Clear previous data
            formsPlot.Plot.Clear();
            lock (lockObject) spectrum = null;
            //spectrumPlot.Clear();

            // Initialize
            spectrumPlot = formsPlot.Plot.Add.DataLogger();
            formsPlot.Plot.Axes.SetLimitsX(0, MAX_HZ);
            formsPlot.Plot.Axes.SetLimitsY(0, MAX_DB);

            spectrumPlot.ViewFull();
            spectrumPlot.ManageAxisLimits = false;
        }

        //private void UpdatePlotData(FrequencySpectrum spectrum)
        private void UpdatePlotData(double[] monoWaveform)
        {
            //lock (lockObject) this.spectrum = spectrum;
            lock (lockObject) waveformBuffer.AddRange(monoWaveform);
            //lock (lockObject) spectrumBuffer.Add(spectrum);
        }

        private void CalculateSmoothedFFT()
        {
            lock (lockObject)
            {
                // Calculate FFT from waveform data
                FrequencySpectrum rawSpectrum = FrequencySpectrum.FromFFT(waveformBuffer.ToArray(), viewer.SampleRate);

                // Initialize if null
                if (smoothedMagnitudes == null || smoothedMagnitudes.Length != rawSpectrum.Magnitudes.Length)
                {
                    smoothedMagnitudes = (double[])rawSpectrum.Magnitudes.Clone();
                    lastFrequencies = rawSpectrum.Frequencies;
                }
                else
                {
                    // Apply EMA smoothing
                    for (int i = 0; i < rawSpectrum.Magnitudes.Length; i++)
                    {
                        smoothedMagnitudes[i] =
                            SMOOTHING_FACTOR * smoothedMagnitudes[i] +
                            (1 - SMOOTHING_FACTOR) * rawSpectrum.Magnitudes[i];
                    }
                }

                // Create the new spectrum
                spectrum = new FrequencySpectrum(lastFrequencies, smoothedMagnitudes.ToArray());
            }
        }


        private void UpdatePlot()
        {
            // Clear previous if any
            //spectrumPlot.Clear();
            //waveformBuffer.Clear();

            lock (lockObject)
            {
                //if (spectrum == null) return;
                if (spectrum == null) CalculateSmoothedFFT();

                for (int i = 0; i < spectrum.Frequencies.Length; i++)
                {
                    Coordinates coordinates = new Coordinates(spectrum.Frequencies[i], spectrum.Magnitudes[i]);

                    if (spectrumPlot.Data.Coordinates.Count > i)
                        spectrumPlot.Data.Coordinates[i] = coordinates;
                    else
                        spectrumPlot.Add(coordinates);
                }

                spectrum = null;
                waveformBuffer.Clear();
                //spectrumBuffer.Clear();
            }

            formsPlot.SafeInvoke(() => formsPlot.Refresh());
        }
    }
}
