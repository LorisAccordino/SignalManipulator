using ScottPlot;
using ScottPlot.Plottables;
using SkiaSharp;

namespace SignalManipulator.UI.Controls.Plottables.Radars
{
    public class SmoothRadar : Radar
    {
        public float Smoothness { get; set; } = 0.5f;

        public override void Render(RenderPack rp)
        {
            if (!IsVisible || Series.Count == 0)
                return;

            using SKPaint paint = new SKPaint();
            PolarAxis.Render(rp);

            foreach (var series in Series)
            {
                Pixel[] rawPoints = PolarAxis.GetCoordinates(series.Values, clockwise: true)
                                             .Select(Axes.GetPixel)
                                             .ToArray();

                // Chiudi la curva aggiungendo il primo punto alla fine
                var points = rawPoints.Concat([rawPoints[0]]).ToArray();

                // Disegna il fill come curva
                using var fillPath = CreateSmoothPath(points, Smoothness);
                Drawing.FillPath(rp.Canvas, paint, fillPath, series.FillStyle);

                // Disegna il bordo come curva
                using var strokePath = CreateSmoothPath(points, Smoothness);
                //Drawing.StrokePath(rp.Canvas, strokePath, series.LineStyle);
                Drawing.DrawPath(rp.Canvas, paint, strokePath, series.LineStyle);
            }
        }


        private SKPath CreateSmoothPath(Pixel[] pts, float tension = 0.5f)
        {
            var path = new SKPath();
            if (pts.Length < 3)
                return path;

            // Converte in SKPoint
            var points = pts.Select(p => new SKPoint(p.X, p.Y)).ToList();

            // Aggiunge punti artificiali per wrapping (copia l'ultimo e il secondo)
            points.Insert(0, points[^2]);       // punto prima dell'inizio
            points.Add(points[1]);              // punto dopo la fine

            path.MoveTo(points[1]); // parte dal vero primo punto

            for (int i = 1; i < points.Count - 2; i++)
            {
                var p0 = points[i - 1];
                var p1 = points[i];
                var p2 = points[i + 1];
                var p3 = points[i + 2];

                var cp1 = new SKPoint(
                    p1.X + (p2.X - p0.X) * tension / 6,
                    p1.Y + (p2.Y - p0.Y) * tension / 6);

                var cp2 = new SKPoint(
                    p2.X - (p3.X - p1.X) * tension / 6,
                    p2.Y - (p3.Y - p1.Y) * tension / 6);

                path.CubicTo(cp1, cp2, p2);
            }

            path.Close();
            return path;
        }
    }
}