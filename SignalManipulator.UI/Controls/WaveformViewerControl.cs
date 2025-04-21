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

        //public WaveformViewerControl Instance;

        public WaveformViewerControl()
        {
            //Instance = this;

            InitializeComponent();

            if (!LicenseManager.UsageMode.Equals(LicenseUsageMode.Designtime))
            {
                viewer = AudioEngine.Instance.WaveformViewer;
                viewer.OnUpdate += UpdatePlot;
                viewer.OnWaveformUpdated += UpdatePlotData;

                InitializePlot();
            }
        }


        private void InitializePlot()
        {
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
            // Add waveform
            for (int i = 0; i < waveform.Count; i++)
            {
                wavePlot.Add(waveform[i]);
            }
            //wavePlot.AddRange(waveform.ToArray());
            waveform.Clear();

            // Update plot
            formsPlot.SafeInvoke(() =>
            {
                formsPlot.Refresh();
                float zoomMultiplier = viewer.SampleRate / (AudioEngine.CHUNK_SIZE * 100f);
                formsPlot.Plot.Axes.SetLimitsX(viewer.SampleRate - (AudioEngine.CHUNK_SIZE * (zoom * zoomMultiplier)), viewer.SampleRate);
            });
        }
    }
}