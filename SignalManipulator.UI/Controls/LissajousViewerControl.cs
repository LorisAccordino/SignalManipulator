using ScottPlot.Plottables;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Events;
using SignalManipulator.Logic.AudioMath;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SignalManipulator.UI.Helpers;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.UI.Controls
{
    [ExcludeFromCodeCoverage]
    public partial class LissajousViewerControl : UserControl
    {
        private IAudioEventDispatcher audioEventDispatcher;
        private Scatter xyPlot;
        private List<double> waveformBuffer = new List<double>();
        private object lockObject = new object();

        private const int MAX_SAMPLES = 1024;
        private double[] left = new double[MAX_SAMPLES];
        private double[] right = new double[MAX_SAMPLES];

        public LissajousViewerControl()
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
            audioEventDispatcher.OnLoad += (s, e) => ResetPlot();
            audioEventDispatcher.OnStopped += (s, e) => ResetPlot();
            audioEventDispatcher.OnUpdate += (s, e) => UpdatePlot();
            audioEventDispatcher.WaveformReady += (s, frame) => UpdatePlotData(frame.DoubleStereo);
        }

        private void InitializePlot()
        {
            formsPlot.Plot.Title("XY Stereo Oscilloscope");
            formsPlot.Plot.XLabel("Left");
            formsPlot.Plot.YLabel("Right");
            formsPlot.Plot.Axes.SquareUnits();
            formsPlot.UserInputProcessor.Disable();

            ResetPlot();
        }

        private void ResetPlot()
        {
            // Clear previous data
            formsPlot.Plot.Clear();
            lock (lockObject) waveformBuffer.Clear();

            // Initialize
            xyPlot = formsPlot.Plot.Add.Scatter(left, right);
            formsPlot.Plot.Axes.SetLimits(-1, 1, -1, 1);
        }

        private void UpdatePlotData(double[] waveform)
        {
            lock (lockObject)
            {
                waveformBuffer.AddRange(waveform);

                // If too many samples, remove the older ones
                if (waveformBuffer.Count > MAX_SAMPLES)
                {
                    int excess = waveformBuffer.Count - MAX_SAMPLES;
                    waveformBuffer.RemoveRange(0, excess);
                }
            }
        }

        private void UpdatePlot()
        {
            lock (lockObject)
            {
                if (waveformBuffer.Count < MAX_SAMPLES) return;
                waveformBuffer.ToArray().SplitStereo(left, right);
                waveformBuffer.Clear();
            }

            // Update plot
            formsPlot.SafeInvoke(() => formsPlot.Refresh());
        }

        private void formsPlot_Resize(object sender, EventArgs e)
        {
            formsPlot.Size = new Size(formsPlot.Height, formsPlot.Height);
            formsPlot.Location = new Point((Width - formsPlot.Width) / 2, (Height - formsPlot.Height) / 2);
        }
    }
}