using ScottPlot.Collections;
using ScottPlot.DataSources;
using ScottPlot.Plottables;

namespace SignalManipulator.UI.Controls.Plottables
{
    public abstract class BaseSignalPlot : Signal
    {
        protected CircularBuffer<double> buffer;
        protected double[] data;
        protected readonly object lockObject = new();

        protected int sampleRate;

        public BaseSignalPlot(int sampleRate, string label = "")
            : base(new SignalSourceDouble(new double[Math.Max(sampleRate, 1)], 1.0))
        {
            this.sampleRate = Math.Max(sampleRate, 1);
            ResizeBuffer(this.sampleRate);
            LegendText = label;
        }

        public virtual void ResizeBuffer(int newCapacity)
        {
            lock (lockObject)
            {
                buffer = new CircularBuffer<double>(newCapacity);
                data = new double[newCapacity];
                Data = new SignalSourceDouble(data, 1.0);
            }
        }

        public abstract void AddSamples(double[] samples);

        public virtual void ClearBuffer()
        {
            lock (lockObject)
            {
                buffer.Clear();
                while (!buffer.IsFull) buffer.Add(0);
                Array.Clear(data);
            }
        }

        public abstract void UpdatePeriod(int param); // param = either windowSeconds or fftSize
    }
}