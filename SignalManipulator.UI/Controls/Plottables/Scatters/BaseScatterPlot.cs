using ScottPlot;
using ScottPlot.Collections;
using ScottPlot.DataSources;
using ScottPlot.Plottables;

namespace SignalManipulator.UI.Controls.Plottables.Scatters
{
    public abstract class BaseScatterPlot : BasePlot, IHasLine, IHasMarker, IHasLegendText, IGetNearest
    {
        protected Scatter Scatter { get; }
        protected readonly double[] left;
        protected readonly double[] right;

        public BaseScatterPlot(int sampleRate, int scatterSamples, string label = "") : base(sampleRate)
        {
            left = new double[scatterSamples];
            right = new double[scatterSamples];
            buffer = new CircularBuffer<double>(scatterSamples * 2);
            Scatter = new Scatter(new ScatterSourceDoubleArray(left, right));
            LegendText = label;
        }

        public override void ClearBuffer()
        {
            lock (lockObject)
            {
                buffer.Clear();
                while (!buffer.IsFull) buffer.Add(0);
                Array.Clear(left);
                Array.Clear(right);
            }
        }

        // No implementations
        public override sealed void ResizeBuffer(int newCapacity) { }
        public override sealed void UpdatePeriod(int param) { }


        // IPlottable methods to implement
        public string LegendText { get => Scatter.LegendText; set => Scatter.LegendText = value; }
        public Color Color { get => Scatter.Color; set => Scatter.Color = value; }

        public override bool IsVisible { get => Scatter.IsVisible; set => Scatter.IsVisible = value; }
        public override IAxes Axes { get => Scatter.Axes; set => Scatter.Axes = value; }
        public override IEnumerable<LegendItem> LegendItems => Scatter.LegendItems;

        public MarkerStyle MarkerStyle { get => Scatter.MarkerStyle; set => Scatter.MarkerStyle = value; }
        public MarkerShape MarkerShape { get => Scatter.MarkerShape; set => Scatter.MarkerShape = value; }
        public float MarkerSize { get => Scatter.MarkerSize; set => Scatter.MarkerSize = value; }
        public Color MarkerFillColor { get => Scatter.MarkerFillColor; set => Scatter.MarkerFillColor = value; }
        public Color MarkerLineColor { get => Scatter.MarkerLineColor; set => Scatter.MarkerLineColor = value; }
        public float MarkerLineWidth { get => Scatter.MarkerLineWidth; set => Scatter.MarkerLineWidth = value; }
        public Color MarkerColor { get => Scatter.MarkerColor; set => Scatter.MarkerColor = value; }
        public LineStyle LineStyle { get => Scatter.LineStyle; set => Scatter.LineStyle = value; }
        public float LineWidth { get => Scatter.LineWidth; set => Scatter.LineWidth = value; }
        public LinePattern LinePattern { get => Scatter.LinePattern; set => Scatter.LinePattern = value; }
        public Color LineColor { get => Scatter.LineColor; set => Scatter.LineColor = value; }
        public override AxisLimits GetAxisLimits() => Scatter.GetAxisLimits();
        public override void Render(RenderPack rp) => Scatter.Render(rp);

        public DataPoint GetNearest(Coordinates mouseLocation, RenderDetails renderInfo, float maxDistance)
            => Scatter.GetNearest(mouseLocation, renderInfo, maxDistance);

        public DataPoint GetNearestX(Coordinates mouseLocation, RenderDetails renderInfo, float maxDistance)
            => Scatter.GetNearestX(mouseLocation, renderInfo, maxDistance);
    }
}
