using ScottPlot;
using ScottPlot.Collections;

namespace SignalManipulator.UI.Controls.Plottables
{
    public abstract class BasePlot : IPlottable
    {
        protected CircularBuffer<double> buffer;
        protected readonly object lockObject = new();

        protected int sampleRate;

        public BasePlot(int sampleRate)
        {
            this.sampleRate = Math.Max(sampleRate, 1);
        }

        public abstract void ResizeBuffer(int newCapacity);
        public abstract void AddSamples(double[] samples);
        public abstract void ClearBuffer();
        public abstract void UpdatePeriod(int param); // param = either windowSeconds or fftSize, etc...


        // IPlottable methods to implement
        public abstract bool IsVisible { get; set; }
        public abstract IAxes Axes { get; set; }
        public abstract IEnumerable<LegendItem> LegendItems { get; }
        public abstract AxisLimits GetAxisLimits();
        public abstract void Render(RenderPack rp);
    }
}