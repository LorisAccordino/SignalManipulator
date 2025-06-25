using ScottPlot.Plottables;
using SignalManipulator.Logic.AudioMath;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Events;
using SignalManipulator.Logic.Models;
using SignalManipulator.UI.Helpers;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    [ExcludeFromCodeCoverage]
    public partial class WaveformViewerControl : UserControl
    {
        private IAudioEventDispatcher audioEventDispatcher;

        private DataStreamer stereoStream;
        private DataStreamer leftStream;
        private DataStreamer rightStream;

        private readonly ConcurrentQueue<WaveformFrame> pendingFrames = new ConcurrentQueue<WaveformFrame>();
        private int sampleRate = AudioEngine.SAMPLE_RATE;

        public double Zoom { get; set; } = 1.0;

        public WaveformViewerControl()
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
            audioEventDispatcher.OnLoad += (_, info) => HandleLoad(info.SampleRate);
            audioEventDispatcher.OnStopped += (_, e) => ClearAllStreams();
            audioEventDispatcher.OnUpdate += (_, e) => UpdatePlot();
            audioEventDispatcher.WaveformReady += (_, frame) => pendingFrames.Enqueue(frame);

            zoomSlider.ValueChanged += ZoomChanged;
            monoCheckBox.CheckedChanged += (_, e) => ToggleStreamsVisibility();
        }

        private void InitializePlot()
        {
            var plt = formsPlot.Plot;
            plt.Title("Signal Waveform");
            plt.XLabel("Time"); plt.YLabel("Amplitude");
            formsPlot.UserInputProcessor.Disable();

            // Create each streamer
            stereoStream = plt.Add.DataStreamer(sampleRate);
            stereoStream.LegendText = "Stereo";
            stereoStream.ViewScrollLeft();
            stereoStream.ManageAxisLimits = false;

            leftStream = plt.Add.DataStreamer(sampleRate);
            leftStream.LegendText = "Left";
            leftStream.ViewScrollLeft();
            leftStream.ManageAxisLimits = false;

            rightStream = plt.Add.DataStreamer(sampleRate);
            rightStream.LegendText = "Right";
            rightStream.ViewScrollLeft();
            rightStream.ManageAxisLimits = false;

            plt.Axes.SetLimitsX(0, sampleRate);
            plt.Axes.SetLimitsY(-1, 1);
            plt.ShowLegend();

            ToggleStreamsVisibility(); // Hide or show streams
            ClearAllStreams();         // Start from scratch
        }

        private void HandleLoad(int newSampleRate)
        {
            if (newSampleRate == sampleRate) return;
            sampleRate = newSampleRate;

            // Reset all the streamers with the new sample rate
            formsPlot.Plot.Clear();
            InitializePlot();
            formsPlot.Refresh();
        }

        private void ClearAllStreams()
        {
            // Clear data
            while (pendingFrames.TryDequeue(out _));
            stereoStream.Clear();
            leftStream.Clear();
            rightStream.Clear();

            // Back to the initial bounds
            formsPlot.Plot.Axes.SetLimitsX(0, sampleRate);
            formsPlot.Refresh();
        }

        private void ToggleStreamsVisibility()
        {
            bool mono = monoCheckBox.Checked;
            stereoStream.IsVisible = !mono;
            leftStream.IsVisible = mono;
            rightStream.IsVisible = mono;
            formsPlot.Refresh();
        }

        private void UpdatePlot()
        {
            while (pendingFrames.TryDequeue(out var frame))
            {
                // Add always to the stereo mix
                stereoStream.AddRange(frame.DoubleMono);

                // Only mono: split and add samples
                if (monoCheckBox.Checked)
                {
                    double[] stereo = frame.DoubleStereo;
                    int half = stereo.Length / 2;
                    double[] left = new double[half], right = new double[half];
                    stereo.SplitStereo(left, right);

                    leftStream.AddRange(left);
                    rightStream.AddRange(right);
                }
            }

            // Unique UI refresh
            formsPlot.SafeInvoke(() => formsPlot.Refresh());
        }

        private void ZoomChanged(object sender, double value)
        {
            Zoom = value;
            double visible = sampleRate / Zoom;
            double start = sampleRate - visible;
            formsPlot.SafeInvoke(() => formsPlot.Plot.Axes.SetLimitsX(start, sampleRate));
        }
    }
}