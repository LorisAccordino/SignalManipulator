using ScottPlot.Plottables;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Viewers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    public partial class WaveformViewerControl : UserControl
    {
        private WaveformViewer viewer;
        private DataStreamer wavePlot;
        private List<double[]> waveformBuffer = new List<double[]>();
        private object lockObject = new object();

        public float Zoom { get; set; } = 1.0f;
        private float zoomMultiplier => viewer.SampleRate / (AudioEngine.CHUNK_SIZE * 100f);

        public WaveformViewerControl()
        {
            InitializeComponent();

            if (!LicenseManager.UsageMode.Equals(LicenseUsageMode.Designtime))
            {
                viewer = AudioEngine.Instance.WaveformViewer;

                InitializeEvents();
                InitializePlot();
            }
        }

        private void InitializeEvents()
        {
            viewer.OnStarted += ResetPlot;
            viewer.OnStopped += ResetPlot;
            viewer.OnUpdate += UpdatePlot;
            viewer.OnWaveformUpdated += UpdatePlotData;
        }

        private void InitializePlot()
        {
            formsPlot.Plot.Title("Signal Waveform");
            formsPlot.Plot.XLabel("Time");
            formsPlot.Plot.YLabel("Amplitude");
            formsPlot.UserInputProcessor.Disable();
            
            ResetPlot();
        }

        private void ResetPlot()
        {
            // Clear previous data
            formsPlot.Plot.Clear();
            lock (lockObject) waveformBuffer.Clear();
            //wavePlot.Clear();

            // Initialize
            wavePlot = formsPlot.Plot.Add.DataStreamer(viewer.SampleRate);
            formsPlot.Plot.Axes.SetLimitsX(0, viewer.SampleRate);
            formsPlot.Plot.Axes.SetLimitsY(-1, 1);

            wavePlot.ViewScrollLeft();
            wavePlot.ManageAxisLimits = false;
        }

        private void UpdatePlotData(double[] waveform)
        {
            lock (lockObject) waveformBuffer.Add(waveform);
        }

        private void UpdatePlot()
        {
            // Update waveform points
            lock (lockObject)
            {
                for (int i = 0; i < waveformBuffer.Count; i++) 
                    wavePlot.AddRange(waveformBuffer[i]);

                waveformBuffer.Clear();
            }

            // Update plot
            formsPlot.SafeInvoke(() => formsPlot.Refresh());
        }

        private void zoomSlider_Scroll(object sender, EventArgs e)
        {
            Zoom = (zoomSlider.Maximum + zoomSlider.Minimum - zoomSlider.Value) / 10.0f;
            zoomAmntLbl.Text = Zoom + "x";

            float width = viewer.SampleRate - (AudioEngine.CHUNK_SIZE * (Zoom * zoomMultiplier));
            formsPlot.SafeInvoke(() => formsPlot.Plot.Axes.SetLimitsX(width, viewer.SampleRate));
        }
    }
}