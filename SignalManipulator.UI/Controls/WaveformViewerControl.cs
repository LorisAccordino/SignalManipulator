using ScottPlot;
using ScottPlot.Collections;
using ScottPlot.Plottables;
using SignalManipulator.Logic.AudioMath;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Events;
using SignalManipulator.Logic.Models;
using SignalManipulator.UI.Helpers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    [ExcludeFromCodeCoverage]
    public partial class WaveformViewerControl : UserControl
    {
        private IAudioEventDispatcher audioEventDispatcher;

        /*
        private DataStreamer stereoStream;
        private DataStreamer leftStream;
        private DataStreamer rightStream;
        */

        // Buffers for the last one second of audio
        private CircularBuffer<double> circularBuffer;
        private double[] stereoBuffer;


        //private double[] stereoBuffer;
        //private Queue<double> stereoQueue;
        //private int capacity;

        /*private readonly int windowSeconds = 1;
        private int writeIndex = 0;*/

        // The “signal” plot
        private Signal stereoSignal;


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





            // Allocate the circular buffer based on the sampleRate
            //stereoBuffer = new double[sampleRate * windowSeconds];
            //capacity = sampleRate;
            //stereoBuffer = new double[capacity];
            //stereoQueue = new Queue<double>(capacity);

            circularBuffer = new CircularBuffer<double>(sampleRate);
            stereoBuffer = new double[sampleRate];

            // Add each signal
            stereoSignal = plt.Add.Signal(stereoBuffer);
            stereoSignal.LegendText = "Stereo Mix";

            //stereoSignal = plt.Add.Signal(Generate.Sin(44100));
            //stereoSignal.Color = ScottPlot.Colors.Black;

            /*
            // Set the axis: X from 0 to windowSeconds
            plt.Axes.SetLimitsX(0, windowSeconds * sampleRate);
            plt.Axes.SetLimitsY(-1, 1);
            */





            /*
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
            */

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
            // Clear pending data
            while (pendingFrames.TryDequeue(out _));

            // Clear buffers
            circularBuffer.Clear();
            while (!circularBuffer.IsFull) circularBuffer.Add(0);
            Array.Clear(stereoBuffer, 0, stereoBuffer.Length);

            // Back to the initial bounds
            formsPlot.Plot.Axes.SetLimitsX(0, sampleRate);
            formsPlot.Refresh();
        }

        private void ToggleStreamsVisibility()
        {
            bool mono = monoCheckBox.Checked;
            stereoSignal.IsVisible = !mono;
            //leftStream.IsVisible = mono;
            //rightStream.IsVisible = mono;
            formsPlot.Refresh();
        }

        private void UpdatePlot()
        {
            while (pendingFrames.TryDequeue(out var frame))
            {


                // 1) Copia i nuovi frame nel buffer circolare
                foreach (double sample in frame.DoubleMono)
                {
                    circularBuffer.Add(sample);
                }
                /*
                // 2) Ricostruisci l’array in ordine lineare (tail-to-head)
                double[] display = new double[stereoBuffer.Length];
                int tail = writeIndex;
                int part = stereoBuffer.Length - tail;
                Array.Copy(stereoBuffer, tail, display, 0, part);
                Array.Copy(stereoBuffer, 0, display, part, tail);
                */

                //stereoBuffer = stereoQueue.ToArray();
                Array.Copy(circularBuffer.ToArray(), stereoBuffer, Math.Min(circularBuffer.Count, stereoBuffer.Length));

                // 3) Aggiorna il SignalPlot e renderizza
                //stereoSignal.Update(display);
                //formsPlot.Render();






                // Add always to the stereo mix
                //stereoStream.AddRange(frame.DoubleMono);

                // Only mono: split and add samples
                if (monoCheckBox.Checked)
                {
                    double[] stereo = frame.DoubleStereo;
                    int half = stereo.Length / 2;
                    double[] left = new double[half], right = new double[half];
                    stereo.SplitStereo(left, right);

                    //leftStream.AddRange(left);
                    //rightStream.AddRange(right);
                }
            }

            // Unique UI refresh
            formsPlot.SafeAsyncInvoke(() => formsPlot.Refresh());
        }

        private void ZoomChanged(object sender, double value)
        {
            Zoom = value;
            double visible = sampleRate / Zoom;
            double start = sampleRate - visible;
            formsPlot.SafeAsyncInvoke(() => formsPlot.Plot.Axes.SetLimitsX(start, sampleRate));
        }
    }
}