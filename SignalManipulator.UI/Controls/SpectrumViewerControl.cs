using ScottPlot;
using ScottPlot.Plottables;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Events;
using SignalManipulator.Logic.Models;
using SignalManipulator.UI.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    public partial class SpectrumViewerControl : UserControl
    {
        private IAudioEventDispatcher audioEventDispatcher;
        private DataLogger spectrumPlot;
        private List<double> waveformBuffer = new List<double>();
        //private List<FrequencySpectrum> spectrumBuffer = new List<FrequencySpectrum>();
        private FFTFrame spectrum; // Spectrum: freqs, magnitudes
        private object lockObject = new object();

        private const int MAX_HZ = 20000;
        private const int MAX_DB = 125;

        private int sampleRate;

        private double[] smoothedMagnitudes;
        private double[] lastFrequencies;
        private const double SMOOTHING_FACTOR = 0.85; // from 0 to 1 (higher = smoother)

        public SpectrumViewerControl()
        {
            InitializeComponent();

            if (!DesignModeHelper.IsDesignMode)
            {
                audioEventDispatcher = AudioEngine.Instance.AudioEventDispatcher;

                InitializeEvents();
                InitializePlot();
            }
        }

        private void InitializeEvents()
        {
            audioEventDispatcher.OnLoad += (s, info) => sampleRate = info.SampleRate;
            audioEventDispatcher.OnLoad += (s, e) => ResetPlot();
            audioEventDispatcher.OnStopped += (s, e) => ResetPlot();
            audioEventDispatcher.OnUpdate += (s, e) => UpdatePlot();
            audioEventDispatcher.WaveformReady += (s, frame) => UpdatePlotData(frame.DoubleMono);
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
                FFTFrame rawSpectrum = FFTFrame.FromFFT(waveformBuffer.ToArray(), sampleRate);

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
                spectrum = new FFTFrame(lastFrequencies, smoothedMagnitudes.ToArray());
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

            // Update plot
            formsPlot.SafeInvoke(() => formsPlot.Refresh());
        }
    }
}
