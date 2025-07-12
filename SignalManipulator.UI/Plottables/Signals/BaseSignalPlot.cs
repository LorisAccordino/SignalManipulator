using ScottPlot;
using ScottPlot.DataSources;
using ScottPlot.Plottables;
using SignalManipulator.Logic.Utils;

namespace SignalManipulator.UI.Plottables.Signals
{
    public abstract class BaseSignalPlot : IPlottable
    {
        protected CircularBuffer<double> buffer = new CircularBuffer<double>(1);
        protected readonly object lockObject = new();
        protected double[] data = [];
        protected int sampleRate;

        protected Signal Signal { get; } = new Signal(new SignalSourceDouble([], 1));

        public BaseSignalPlot(int sampleRate, string label = "")
        {
            this.sampleRate = sampleRate;
            ResizeBuffer(this.sampleRate);
            Signal.LegendText = label;
        }

        public abstract void UpdatePeriod(int param); // param = either windowSeconds or fftSize, etc...

        public virtual void ResizeBuffer(int newCapacity)
        {
            lock (lockObject)
            {
                buffer = new CircularBuffer<double>(newCapacity);
                data = new double[newCapacity];
                Signal.Data = new SignalSourceDouble(data, 1.0);
            }
        }

        public void Clear()
        {
            lock (lockObject)
            {
                buffer.Clear();
                while (!buffer.IsFull) buffer.Add(0);
                Array.Clear(data);
            }
        }

        // IPlottable methods to implement
        public Color Color { get => Signal.Color; set => Signal.Color = value; }
        public bool IsVisible { get => Signal.IsVisible; set => Signal.IsVisible = value; }
        public IAxes Axes { get => Signal.Axes; set => Signal.Axes = value; }
        public IEnumerable<LegendItem> LegendItems => Signal.LegendItems;
        public AxisLimits GetAxisLimits() => Signal.GetAxisLimits();
        public void Render(RenderPack rp) => Signal.Render(rp);
    }
}