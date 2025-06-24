using ScottPlot.Plottables;
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
        private DataStreamer waveformStream;
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
            audioEventDispatcher.OnStopped += (_, e) => HandleStop();
            audioEventDispatcher.OnUpdate += (_, e) => UpdatePlot();
            audioEventDispatcher.WaveformReady += (_, frame) => HandleWaveformReady(frame);

            zoomSlider1.ValueChanged += ZoomChanged;
        }

        private void InitializePlot()
        {
            formsPlot.Plot.Title("Signal Waveform");
            formsPlot.Plot.XLabel("Time");
            formsPlot.Plot.YLabel("Amplitude");
            formsPlot.UserInputProcessor.Disable();

            ResetWaveform();
        }

        private void HandleLoad(int newSampleRate)
        {
            if (newSampleRate != sampleRate)
            {
                sampleRate = newSampleRate;
                ResetWaveform();
            }
        }

        private void HandleStop()
        {
            ResetWaveform();
        }

        private void HandleWaveformReady(WaveformFrame frame)
        {
            pendingFrames.Enqueue(frame);
        }

        private void ResetWaveform()
        {
            formsPlot.Plot.Clear();
            formsPlot.Refresh();
            while (pendingFrames.TryDequeue(out _)) ;

            waveformStream = formsPlot.Plot.Add.DataStreamer(sampleRate);
            formsPlot.Plot.Axes.SetLimitsX(0, sampleRate);
            formsPlot.Plot.Axes.SetLimitsY(-1, 1);

            waveformStream.ViewScrollLeft();
            waveformStream.ManageAxisLimits = false;
        }

        private void UpdatePlot()
        {
            while (pendingFrames.TryDequeue(out var frame))
                waveformStream.AddRange(frame.DoubleMono);

            formsPlot.SafeInvoke(() => formsPlot.Refresh());
        }

        private void ZoomChanged(object sender, double value)
        {
            Zoom = value;
            double visibleSamples = sampleRate / Zoom;
            double start = sampleRate - visibleSamples;

            formsPlot.SafeInvoke(() => formsPlot.Plot.Axes.SetLimitsX(start, sampleRate));
        }
    }
}