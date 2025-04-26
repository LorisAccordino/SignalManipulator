using ScottPlot.Plottables;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Core.Events;
using SignalManipulator.Logic.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    public partial class LissajousViewerControl : UserControl
    {
        private AudioEventDispatcher audioEventDispatcher;
        private Scatter xyPlot;
        private List<double> waveformBuffer = new List<double>();
        private object lockObject = new object();

        private const int MAX_SAMPLES = 1024;
        private double[] left = new double[MAX_SAMPLES];
        private double[] right = new double[MAX_SAMPLES];

        public LissajousViewerControl()
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
            audioEventDispatcher.OnLoad += ResetPlot;
            audioEventDispatcher.OnStopped += ResetPlot;
            audioEventDispatcher.OnUpdate += UpdatePlot;
            //viewer.OnSpectrumUpdated += UpdatePlotData;
            audioEventDispatcher.FrameReady += (frame) => UpdatePlotData(frame.DoubleStereo);
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

        //double[] xs = { 0.5, 0.5, 0.5 }; // stessa X
        //double[] ys = { 0.25, 0.5, 0.75 }; // Y diversi

        private void ResetPlot()
        {
            // Clear previous data
            formsPlot.Plot.Clear();
            lock (lockObject) waveformBuffer.Clear();
            //spectrumPlot.Clear();

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

                (double[] tmpLeft, double[] tmpRight) = waveformBuffer.ToArray().SplitStereo();
                waveformBuffer.Clear();

                Array.Copy(tmpLeft, left, tmpLeft.Length);
                Array.Copy(tmpRight, right, tmpRight.Length);
            }

            formsPlot.SafeInvoke(() => formsPlot.Refresh());
        }

        private void formsPlot_Resize(object sender, EventArgs e)
        {
            formsPlot.Size = new Size(formsPlot.Height, formsPlot.Height);
            formsPlot.Location = new Point((Width - formsPlot.Width) / 2, (Height - formsPlot.Height) / 2);
        }
    }
}