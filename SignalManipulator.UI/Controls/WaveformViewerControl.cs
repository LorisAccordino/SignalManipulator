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
        private List<double> waveform = new List<double>();

        public float Zoom { get; set; } = 1.0f;
        private float zoomMultiplier;

        public WaveformViewerControl()
        {
            InitializeComponent();

            if (!LicenseManager.UsageMode.Equals(LicenseUsageMode.Designtime))
            {
                viewer = AudioEngine.Instance.WaveformViewer;

                viewer.OnStarted += InitializePlot;
                viewer.OnStopped += InitializePlot;
                viewer.OnUpdate += UpdatePlot;
                viewer.OnWaveformUpdated += UpdatePlotData;

                zoomMultiplier = viewer.SampleRate / (AudioEngine.CHUNK_SIZE * 100f);
                Zoom = 1;

                InitializePlot();
            }
        }


        private void InitializePlot()
        {
            // Clear previous data
            formsPlot.Plot.Clear();
            waveform.Clear();
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
            this.waveform.AddRange(waveform);
        }

        private void UpdatePlot()
        {
            // Add waveform points
            for (int i = 0; i < waveform.Count; i++)
            {
                wavePlot.Add(waveform[i]);
            }
            waveform.Clear();

            // Update plot
            formsPlot.SafeInvoke(() =>
            {
                formsPlot.Refresh();
                formsPlot.Plot.Axes.SetLimitsX(viewer.SampleRate - (AudioEngine.CHUNK_SIZE * (Zoom * zoomMultiplier)), viewer.SampleRate);
            });
        }

        private void zoomSlider_Scroll(object sender, EventArgs e)
        {
            Zoom = zoomSlider.Value / 10.0f;
            zoomAmntLbl.Text = Zoom + "x";
        }
    }
}