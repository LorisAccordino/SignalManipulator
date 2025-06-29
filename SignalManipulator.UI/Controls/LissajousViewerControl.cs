﻿using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Events;
using SignalManipulator.Logic.AudioMath;
using System;
using System.Drawing;
using System.Windows.Forms;
using SignalManipulator.UI.Helpers;
using System.Diagnostics.CodeAnalysis;
using ScottPlot.Collections;
using SignalManipulator.UI.Misc;

namespace SignalManipulator.UI.Controls
{
    [ExcludeFromCodeCoverage]
    public partial class LissajousViewerControl : UserControl
    {
        private IAudioEventDispatcher audioEventDispatcher;
        private object lockObject = new object();

        private const int MAX_SAMPLES = 1024;
        private readonly double[] left = new double[MAX_SAMPLES];
        private readonly double[] right = new double[MAX_SAMPLES];
        private CircularBuffer<double> interleavedBuffer = new CircularBuffer<double>(MAX_SAMPLES * 2);

        // Track whether a redraw is needed
        private volatile bool needsRender = false;

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
            audioEventDispatcher.OnStopped += (s, e) => ClearBuffers();
            audioEventDispatcher.WaveformReady += (s, frame) => UpdatePlotData(frame.DoubleStereo);

            UIUpdateService.Instance.Register(RenderPlot);
        }

        private void InitializePlot()
        {
            // Init plot
            var plt = formsPlot.Plot;
            plt.Title("XY Stereo Oscilloscope");
            plt.XLabel("Left"); plt.YLabel("Right");
            plt.Axes.SquareUnits();
            plt.Axes.SetLimits(-1, 1, -1, 1);
            formsPlot.UserInputProcessor.Disable();

            // Setup scatter plot
            var xyPlot = plt.Add.Scatter(left, right);
            xyPlot.MarkerSize = 0; // Don't draw markers
            xyPlot.LineWidth = 1; // Thick enough
            
            // Clear buffers
            ClearBuffers();
        }

        private void ClearBuffers()
        {
            lock (lockObject)
            {
                interleavedBuffer.Clear();
                Array.Clear(left, 0, left.Length);
                Array.Clear(right, 0, right.Length);
            }

            // Force render
            needsRender = true;
        }

        private void UpdatePlotData(double[] waveform)
        {
            lock (lockObject)
            {
                foreach (var sample in waveform)
                    interleavedBuffer.Add(sample);

                if (interleavedBuffer.Count < MAX_SAMPLES * 2) 
                    return;

                interleavedBuffer.ToArray().SplitStereo(left, right);
            }

            needsRender = true;
        }

        private void RenderPlot()
        {
            if (!needsRender) return;

            lock (lockObject) formsPlot.Refresh();
            needsRender = false;
        }

        private void formsPlot_Resize(object sender, EventArgs e)
        {
            formsPlot.Size = new Size(formsPlot.Height, formsPlot.Height);
            formsPlot.Location = new Point((Width - formsPlot.Width) / 2, (Height - formsPlot.Height) / 2);
            needsRender = true;
        }
    }
}