using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Events;
using SignalManipulator.Logic.Models;
using SignalManipulator.UI.Controls.Plottables;
using SignalManipulator.UI.Helpers;
using SignalManipulator.UI.Misc;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    [ExcludeFromCodeCoverage]
    public partial class WaveformViewerControl : UserControl
    {
        // References
        private IAudioEventDispatcher audioEventDispatcher;
        private UIUpdateService UIupdateService;

        // Window config
        private const int MAX_WINDOWS_SECONDS = 10;       // Maximum limit
        private int windowSeconds = 1;                    // Current shown seconds

        // Audio & buffer
        private readonly ConcurrentQueue<WaveformFrame> pendingFrames = new ConcurrentQueue<WaveformFrame>();
        private int sampleRate = AudioEngine.SAMPLE_RATE;
        private readonly object lockObject = new object();

        // Waveform plots
        private List<WaveformPlot> waveformPlots = new List<WaveformPlot>();

        // UI dirty flag
        private volatile bool needsRender = false;

        public WaveformViewerControl()
        {
            InitializeComponent();

            if (!DesignModeHelper.IsDesignMode)
            {
                audioEventDispatcher = AudioEngine.Instance.AudioEventDispatcher;
                UIupdateService = UIUpdateService.Instance;

                InitializePlot();
                InitializeEvents();
            }
        }

        private void InitializeEvents()
        {
            // Main events
            audioEventDispatcher.OnLoad += OnLoad;
            audioEventDispatcher.OnStarted += OnStarted;
            audioEventDispatcher.OnStopped += OnStopped;

            // Update event
            UIupdateService.Register(RenderPlot);

            // Force stop event to init purpose
            OnStopped(this, EventArgs.Empty);

            // Other events
            secNum.ValueChanged += (_, v) => { windowSeconds = (int)secNum.Value; UpdateDataPeriod(); };
            secNum.Maximum = MAX_WINDOWS_SECONDS;
            monoCheckBox.CheckedChanged += (_, e) => ToggleStreams();
        }

        public void OnLoad(object? sender, AudioInfo info)
        {
            sampleRate = info.SampleRate; 
            ResampleBuffers();
        }

        public void OnStarted(object? sender, EventArgs e)
        {
            audioEventDispatcher.WaveformReady += ProcessFrame;
            settingsPanel.Enabled = true; // Enable UI
        }

        public void OnStopped(object? sender, EventArgs e)
        {
            audioEventDispatcher.WaveformReady -= ProcessFrame;
            ClearBuffers();
            ResetUI();
        }

        private void ResetUI()
        {
            // Checkbox and numeric up-down
            monoCheckBox.CheckState = CheckState.Unchecked;
            secNum.Value = 1;

            // Navigator
            navigator.Zoom = 1;
            navigator.Pan = 0;

            // Disable UI
            settingsPanel.Enabled = false;
        }

        private void InitializePlot()
        {
            // Init plot
            var plt = formsPlot.Plot;
            plt.Title("Signal Waveform");
            plt.XLabel("Time (samples)"); plt.YLabel("Amplitude");
            formsPlot.UserInputProcessor.Disable();

            // Add each waveform to the plot
            waveformPlots.Add(plt.Add.Waveform(sampleRate, "Stereo"));
            waveformPlots.Add(plt.Add.Waveform(sampleRate, "Left"));
            waveformPlots.Add(plt.Add.Waveform(sampleRate, "Right"));
            plt.ShowLegend();

            // Set the bounds
            plt.Axes.SetLimitsY(-1, 1);
            navigator.Capacity = sampleRate;

            // Force the update
            needsRender = true;
            RenderPlot();

            // Final setups
            ToggleStreams(); // Hide or show streams
            ClearBuffers();  // Start from scratch
        }

        private void ResampleBuffers()
        {
            // Resample the waveforms
            foreach (var w in waveformPlots)
                w.ResampleBuffer(sampleRate);

            // Update data period
            UpdateDataPeriod();
        }

        private void ClearBuffers()
        {
            // Clear pending data
            while (pendingFrames.TryDequeue(out _));

            lock (lockObject)
            {
                // Clear waveform buffers
                foreach (var w in waveformPlots)
                    w.ClearBuffer();
            }

            needsRender = true;
        }

        private void ToggleStreams()
        {
            bool mono = monoCheckBox.Checked;

            lock (lockObject)
            {
                waveformPlots[0].IsVisible = !mono; // Stereo
                waveformPlots[1].IsVisible = mono;  // Left
                waveformPlots[2].IsVisible = mono;  // Right
            }

            needsRender = true;
        }

        private void ProcessFrame(object? sender, WaveformFrame frame)
        {
            lock (lockObject)
            {
                // Add samples to each channel
                waveformPlots[0].AddSamples(frame.DoubleMono);
                waveformPlots[1].AddSamples(frame.DoubleSplitStereo.Left);
                waveformPlots[2].AddSamples(frame.DoubleSplitStereo.Right);
            }

            needsRender = true;
        }

        private void RenderPlot()
        {
            lock (lockObject)
            {
                if (navigator.NeedsUpdate)
                {
                    navigator.Recalculate();
                    formsPlot.Plot.Axes.SetLimitsX(navigator.Start, navigator.End);
                    needsRender = true;
                }

                if (needsRender)
                    formsPlot.Refresh();
            }

            needsRender = false;
        }

        private void UpdateDataPeriod()
        {
            foreach (var w in waveformPlots)
                w.UpdatePeriod(windowSeconds);

            navigator.Capacity = sampleRate * windowSeconds;
        }
    }
}