using ScottPlot.Plottables;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Core.Events;
using SignalManipulator.Logic.Core.Playback;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    public partial class WaveformViewerControl : UserControl
    {
        private PlaybackController playback;
        private AudioEventDispatcher audioEventDispatcher;
        private DataStreamer wavePlot;
        private List<AudioFrame> frames = new List<AudioFrame>();
        private object lockObject = new object();

        public float Zoom { get; set; } = 1.0f;
        private float zoomMultiplier => playback.SampleRate / (AudioEngine.CHUNK_SIZE * 100f);

        public WaveformViewerControl()
        {
            InitializeComponent();

            if (!LicenseManager.UsageMode.Equals(LicenseUsageMode.Designtime))
            {
                playback = AudioEngine.Instance.PlaybackController;
                audioEventDispatcher = AudioEngine.Instance.AudioEventDispatcher;

                InitializeEvents();
                InitializePlot();
            }
        }

        private void InitializeEvents()
        {
            audioEventDispatcher.OnLoad += ResetPlot;
            audioEventDispatcher.OnStopped += ResetPlot;
            audioEventDispatcher.OnUpdate += UpdatePlot;
            audioEventDispatcher.FrameReady += UpdatePlotData;
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
            wavePlot = formsPlot.Plot.Add.DataStreamer(playback.SampleRate);
            formsPlot.Plot.Axes.SetLimitsX(0, playback.SampleRate);
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

            float width = playback.SampleRate - (AudioEngine.CHUNK_SIZE * (Zoom * zoomMultiplier));
            formsPlot.SafeInvoke(() => formsPlot.Plot.Axes.SetLimitsX(width, playback.SampleRate));
        }
    }
}