using ScottPlot;
using ScottPlot.Collections;
using ScottPlot.DataSources;
using ScottPlot.Plottables;

namespace SignalManipulator.UI.Controls.Plottables.Signals
{
    public abstract class BaseSignalPlot : BasePlot, IHasLine, IHasMarker, IHasLegendText, IGetNearest
    {
        protected Signal Signal { get; } = new Signal(new SignalSourceDouble([], 1));
        protected double[] data;
        protected ISignalSource Data { get => Signal.Data; set => Signal.Data = value; }

        public BaseSignalPlot(int sampleRate, string label = "") : base(sampleRate)
        {
            ResizeBuffer(this.sampleRate);
            LegendText = label;
        }

        public override void ResizeBuffer(int newCapacity)
        {
            lock (lockObject)
            {
                buffer = new CircularBuffer<double>(newCapacity);
                data = new double[newCapacity];
                Data = new SignalSourceDouble(data, 1.0);
            }
        }

        public override void ClearBuffer()
        {
            lock (lockObject)
            {
                buffer.Clear();
                while (!buffer.IsFull) buffer.Add(0);
                Array.Clear(data);
            }
        }

        // IPlottable methods to implement
        public string LegendText { get => Signal.LegendText; set => Signal.LegendText = value; }
        public Color Color { get => Signal.Color; set => Signal.Color = value; }

        public override bool IsVisible { get => Signal.IsVisible; set => Signal.IsVisible = value; }
        public override IAxes Axes { get => Signal.Axes; set => Signal.Axes = value; }
        public override IEnumerable<LegendItem> LegendItems => Signal.LegendItems;

        public MarkerStyle MarkerStyle { get => Signal.MarkerStyle; set => Signal.MarkerStyle = value; }
        public MarkerShape MarkerShape { get => Signal.MarkerShape; set => Signal.MarkerShape = value; }
        public float MarkerSize { get => Signal.MarkerSize; set => Signal.MarkerSize = value; }
        public Color MarkerFillColor { get => Signal.MarkerFillColor; set => Signal.MarkerFillColor = value; }
        public Color MarkerLineColor { get => Signal.MarkerLineColor; set => Signal.MarkerLineColor = value; }
        public float MarkerLineWidth { get => Signal.MarkerLineWidth; set => Signal.MarkerLineWidth = value; }
        public Color MarkerColor { get => Signal.MarkerColor; set => Signal.MarkerColor = value; }
        public LineStyle LineStyle { get => Signal.LineStyle; set => Signal.LineStyle = value; }
        public float LineWidth { get => Signal.LineWidth; set => Signal.LineWidth = value; }
        public LinePattern LinePattern { get => Signal.LinePattern; set => Signal.LinePattern = value; }
        public Color LineColor { get => Signal.LineColor; set => Signal.LineColor = value; }
        public override AxisLimits GetAxisLimits() => Signal.GetAxisLimits();
        public override void Render(RenderPack rp) => Signal.Render(rp);

        public DataPoint GetNearest(Coordinates mouseLocation, RenderDetails renderInfo, float maxDistance)
            => Signal.GetNearest(mouseLocation, renderInfo, maxDistance);

        public DataPoint GetNearestX(Coordinates mouseLocation, RenderDetails renderInfo, float maxDistance)
            => Signal.GetNearestX(mouseLocation, renderInfo, maxDistance);
    }
}