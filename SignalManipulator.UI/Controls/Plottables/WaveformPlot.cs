using ScottPlot.Collections;
using ScottPlot.DataSources;
using ScottPlot.Plottables;
using SignalManipulator.Logic.Core;

namespace SignalManipulator.UI.Controls.Plottables
{
    public class WaveformPlot : Signal
    {
        // Data
        private CircularBuffer<double> buffer = new CircularBuffer<double>(AudioEngine.SAMPLE_RATE);
        private double[] data;

        public WaveformPlot(int sampleRate) : this(sampleRate, "") { }
        public WaveformPlot(int sampleRate, string channelName) : base(new SignalSourceDouble(new double[sampleRate], 1.0))
        {
            sampleRate = Math.Max(sampleRate, 1);
            ResizeBuffer(sampleRate);
            LegendText = channelName;
        }

        public void ResizeBuffer(int sampleRate)
        {
            buffer = new CircularBuffer<double>(sampleRate);
            data = new double[sampleRate];
            Data = new SignalSourceDouble(data, 1.0);
        }

        public void AddSamples(double[] samples)
        {
            for (int i = 0; i < samples.Length; i += (int)Data.Period)
                buffer.Add(samples[i]);

            buffer.CopyTo(data, 0);
        }

        public void UpdatePeriod(double windowSeconds)
        {
            Data.Period = windowSeconds;
        }

        public void ClearBuffer()
        {
            buffer.Clear();
            while (!buffer.IsFull) buffer.Add(0);
            Array.Clear(data);
        }
    }
}