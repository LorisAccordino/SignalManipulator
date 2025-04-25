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
        private AudioVisualizer viewer;
        private DataStreamer wavePlot;
        private List<AudioFrame> frames = new List<AudioFrame>();
        private object lockObject = new object();

        public float Zoom { get; set; } = 1.0f;
        private float zoomMultiplier => viewer.SampleRate / (AudioEngine.CHUNK_SIZE * 100f);

        public WaveformViewerControl()
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
            viewer.OnFrameAvailable += UpdatePlotData;
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
            lock (lockObject) frames.Clear();
            //wavePlot.Clear();

            // Initialize
            wavePlot = formsPlot.Plot.Add.DataStreamer(viewer.SampleRate);
            formsPlot.Plot.Axes.SetLimitsX(0, viewer.SampleRate);
            formsPlot.Plot.Axes.SetLimitsY(-1, 1);

            wavePlot.ViewScrollLeft();
            wavePlot.ManageAxisLimits = false;
        }

        private void UpdatePlotData(AudioFrame frame)
        {
            lock (lockObject) frames.Add(frame);
        }

        private void UpdatePlot()
        {
            // Update waveform points
            lock (lockObject)
            {
                for (int i = 0; i < frames.Count; i++)
                    wavePlot.AddRange(frames[i].DoubleMono);

                frames.Clear();
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