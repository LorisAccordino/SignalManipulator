using ScottPlot.Plottables;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Events;
using SignalManipulator.Logic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    public partial class WaveformViewerControl : UserControl
    {
        private IAudioEventDispatcher audioEventDispatcher;
        private DataStreamer wavePlot;
        private List<WaveformFrame> frames = new List<WaveformFrame>();
        private object lockObject = new object();

        private int sampleRate;
        private float zoomMultiplier => sampleRate / (AudioEngine.CHUNK_SIZE * 100f);
        public float Zoom { get; set; } = 1.0f;

        public WaveformViewerControl()
        {
            InitializeComponent();

            if (!LicenseManager.UsageMode.Equals(LicenseUsageMode.Designtime))
            {
                audioEventDispatcher = AudioEngine.Instance.AudioEventDispatcher;

                InitializeEvents();
                InitializePlot();
            }
        }

        private void InitializeEvents()
        {
            audioEventDispatcher.OnLoad += info => sampleRate = info.SampleRate;
            audioEventDispatcher.OnLoad += _ => ResetPlot();
            audioEventDispatcher.OnStopped += ResetPlot;
            audioEventDispatcher.OnUpdate += UpdatePlot;
            audioEventDispatcher.WaveformReady += UpdatePlotData;
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
            formsPlot.Refresh();
            lock (lockObject) frames.Clear();
            //wavePlot.Clear();

            // Initialize
            wavePlot = formsPlot.Plot.Add.DataStreamer(sampleRate);
            formsPlot.Plot.Axes.SetLimitsX(0, sampleRate);
            formsPlot.Plot.Axes.SetLimitsY(-1, 1);

            wavePlot.ViewScrollLeft();
            wavePlot.ManageAxisLimits = false;
        }

        private void UpdatePlotData(WaveformFrame frame)
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

            float width = sampleRate - (AudioEngine.CHUNK_SIZE * (Zoom * zoomMultiplier));
            formsPlot.SafeInvoke(() => formsPlot.Plot.Axes.SetLimitsX(width, sampleRate));
        }
    }
}