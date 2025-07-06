using ScottPlot;
using ScottPlot.Collections;
using ScottPlot.DataSources;
using ScottPlot.Plottables;
using SignalManipulator.Logic.AudioMath;

namespace SignalManipulator.UI.Controls.Plottables.Scatters
{
    public class Lissajous : IPlottable
    {
        protected Scatter Scatter { get; }
        protected readonly object lockObject = new();

        protected CircularBuffer<double> buffer;
        protected readonly double[] left;
        protected readonly double[] right;

        public Lissajous(int scatterSamples, string label = "")
        {
            left = new double[scatterSamples];
            right = new double[scatterSamples];
            buffer = new CircularBuffer<double>(scatterSamples * 2);

            Scatter = new Scatter(new ScatterSourceDoubleArray(left, right))
            {
                MarkerSize = 0,
                LineWidth = 1,
                LegendText = label
            };
        }

        public void AddSamples(double[] stereoSamples)
        {
            lock (lockObject)
            {
                for (int i = 0; i < stereoSamples.Length; i++)
                    buffer.Add(stereoSamples[i]);

                if (buffer.Count < left.Length * 2)
                    return;

                buffer.ToArray().SplitStereo(left, right);
            }
        }

        public void Clear()
        {
            lock (lockObject)
            {
                buffer.Clear();
                while (!buffer.IsFull) buffer.Add(0);
                Array.Clear(left);
                Array.Clear(right);
            }
        }


        // IPlottable methods to implement
        public Color Color { get => Scatter.Color; set => Scatter.Color = value; }
        public bool IsVisible { get => Scatter.IsVisible; set => Scatter.IsVisible = value; }
        public IAxes Axes { get => Scatter.Axes; set => Scatter.Axes = value; }
        public IEnumerable<LegendItem> LegendItems => Scatter.LegendItems;
        public AxisLimits GetAxisLimits() => Scatter.GetAxisLimits();
        public void Render(RenderPack rp) => Scatter.Render(rp);
    }
}