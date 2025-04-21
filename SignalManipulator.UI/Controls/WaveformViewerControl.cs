using ScottPlot.Plottables;
using SignalManipulator.Logic.Core;
using System;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    public partial class WaveformViewerControl : UserControl
    {
        private AudioEngine audioEngine;

        private DataStreamer wavePlot;
        private double[] waveform;

        public WaveformViewerControl Instance;

        public WaveformViewerControl()
        {
            Instance = this;

            InitializeComponent();
        }

        private void WaveformViewerControl_Load(object sender, EventArgs e)
        {
            InitializePlot();

            audioEngine = AudioEngine.Instance;
            audioEngine.AudioPlayer.OnUpdate += (s, _) => UpdatePlot();
            audioEngine.AudioPlayer.OnUpdateData += (s, buf) => UpdateWaveform(buf);

            waveform = new double[AudioEngine.CHUNK_SIZE];
        }


        private void InitializePlot()
        {
            wavePlot = formsPlot.Plot.Add.DataStreamer(AudioEngine.SAMPLE_RATE);
            formsPlot.Plot.Axes.SetLimitsX(0, AudioEngine.SAMPLE_RATE);
            wavePlot.ViewScrollLeft();
            wavePlot.ManageAxisLimits = false;
        }

        private byte[] latestBuffer = Array.Empty<byte>();

        public void UpdateWaveform(byte[] buffer)
        {
            latestBuffer = buffer;
        }

        private void UpdatePlot()
        {
            if (latestBuffer.Length == 0) return;

            // Convert to float PCM data
            int sampleCount = latestBuffer.Length / 2; // 16-bit
            waveform = new double[sampleCount];

            for (int i = 0; i < sampleCount; i++)
            {
                short sample = BitConverter.ToInt16(latestBuffer, i * 2);
                waveform[i] = sample / 32768.0;
            }

            wavePlot.AddRange(waveform);
            formsPlot.Refresh();
            formsPlot.Plot.Axes.AutoScaleX();
        }
    }
}
