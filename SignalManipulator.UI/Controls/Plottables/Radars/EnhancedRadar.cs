using ScottPlot;
using ScottPlot.PathStrategies;
using ScottPlot.Plottables;
using SkiaSharp;

namespace SignalManipulator.UI.Controls.Plottables.Radars
{
    public class EnhancedRadar : Radar
    {
        public Color Color { get; set; }
        //public bool RenderPolarAxis { get; set; } = true;
        public new PolarAxis PolarAxis { get; set; }
        public bool Smooth { set => PathStrategy = value ? new ScottPlot.PathStrategies.CubicSpline() : new Straight(); }
        
        public double SmoothTension
        {
            get => PathStrategy is CubicSpline cs ? cs.Tension : 0;
            set => PathStrategy = new CubicSpline() { Tension = value };
        }

        private IPathStrategy PathStrategy { get; set; } = new Straight();

        public EnhancedRadar()
        {
            base.PolarAxis = new PolarAxis
            {
                Rotation = Angle.FromDegrees(90),
                ManageAxisLimits = true,
            };
            PolarAxis = base.PolarAxis;
        }

        public override void Render(RenderPack rp)
        {
            if (!IsVisible)
                return;

            if (Series.Count == 0)
                return;

            /*rp.Paint.Color = Color.ToSKColor();
            Series[0].FillColor = Color;
            Series[0].LineColor = Color;*/

            //if (RenderPolarAxis)
                PolarAxis.Render(rp);


            for (int i = 0; i < Series.Count; i++)
            {
                Coordinates[] cs1 = base.PolarAxis.GetCoordinates(Series[i].Values, clockwise: true);
                Pixel[] pixels = cs1.Select(Axes.GetPixel).ToArray();
                SKPath path = PathStrategy.GetPath(pixels.Append(pixels[0])); // Close curve
                Drawing.DrawPath(rp.Canvas, rp.Paint, path, Series[i].FillStyle, rp.DataRect);
                Drawing.DrawPath(rp.Canvas, rp.Paint, path, Series[i].LineStyle);
            }
        }
    }
}