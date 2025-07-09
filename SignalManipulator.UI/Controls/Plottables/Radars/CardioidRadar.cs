using ScottPlot.Plottables;
using ScottPlot;
using SignalManipulator.Logic.AudioMath.Objects;
using System.Numerics;

namespace SignalManipulator.UI.Controls.Plottables.Radars
{
    public class CardioidRadar : Radar
    {
        private const int resolution = 180;

        private Angle rotation = Angle.FromRadians(0);
        private List<double> baseAngles = new(); // Pre-rotation fixed angles
        public Angle Rotation
        {
            get => rotation; 
            set
            {
                rotation = value;
                PolarAxis.Rotation = rotation;
                for (int i = 0; i < Cardioids.Count; i++)
                    Cardioids[i].Angle = baseAngles[i] + rotation.Radians;
            }
        }

        public Color Color
        {
            get => Series[0].FillColor;
            set
            {
                Series[0].FillColor = value.WithOpacity();
                Series[0].LineColor = value.WithOpacity(0.5);
            }
        }

        private double radius = 1.0;
        private string[] labels = [];
        public double Radius
        {
            get => radius;

            set
            {
                radius = value;
                PolarAxis.SetCircles(radius, 4);
                if (labels.Length != 0)
                    PolarAxis.SetSpokes(labels, radius * 1.1);
                else
                    PolarAxis.SetSpokes(Cardioids.Count, radius * 1.1, false);
            }
        }

        public List<Cardioid> Cardioids { get; private set; } = new List<Cardioid>();

        public CardioidRadar(string[] cardioidsLabels) : this(cardioidsLabels.Length)
        {
            labels = cardioidsLabels;
            Radius = 1.0;
        }
        public CardioidRadar(int cardioidCount)
        {
            if (cardioidCount <= 0)
                throw new ArgumentException("The cardiod count must be greater than zero.");

            // Create cardioids with equal angles and magniuted set to 0 as default
            for (int i = 0; i < cardioidCount; i++)
            {
                double baseAngle = -2 * Math.PI * i / cardioidCount;
                baseAngles.Add(baseAngle);
                Cardioids.Add(new Cardioid(0, baseAngle, Cardioid.CardioidMode.Cosine));
            }

            Series.Add(new RadarSeries() { Values = GetMagnitudes() });
            Rotation = Angle.FromDegrees(0);
        }

        public double[] GetMagnitudes()
        {
            double[] magnitudes = new double[Cardioids.Count];
            for (int i = 0; i < Cardioids.Count; i++)
                magnitudes[i] = Cardioids[i].Magnitude;
            return magnitudes;
        }

        public void SetMagnitudes(double[] magnitudes)
        {
            if (magnitudes.Length != Cardioids.Count)
                throw new ArgumentException($"Exactly {Cardioids.Count} values are needed.");

            for (int i = 0; i < magnitudes.Length; i++)
                Cardioids[i].Magnitude = magnitudes[i] * Radius;
        }

        public override void Render(RenderPack rp)
        {
            if (!IsVisible)
                return;

            PolarAxis.Render(rp);

            List<Vector2> points = Cardioid.Merge(Cardioids, resolution);
            Coordinates[] coords = points.Select(p => new Coordinates(p.X, p.Y)).ToArray();
            Pixel[] pixels = coords.Select(Axes.GetPixel).ToArray();
            Drawing.DrawPath(rp.Canvas, rp.Paint, pixels, Series[0].FillStyle);
            Drawing.DrawPath(rp.Canvas, rp.Paint, pixels, Series[0].LineStyle, close: true);
        }
    }
}