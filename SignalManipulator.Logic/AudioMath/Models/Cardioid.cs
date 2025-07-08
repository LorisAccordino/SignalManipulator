using System.Numerics;

namespace SignalManipulator.Logic.AudioMath.Models
{
    public class Cardioid
    {
        public double Magnitude { get; set; } = 1.0;
        public double Angle { get; set; } = 0.0;
        public enum CardioidMode { Generic, Cosine, Sine, NegativeCosine }
        public CardioidMode Mode { get; set; } = CardioidMode.Cosine;

        public Cardioid(double magnitude, double angleRadians, CardioidMode mode = CardioidMode.Cosine)
        {
            Magnitude = magnitude;
            Angle = angleRadians;
            Mode = mode;
        }

        public double GetRadius(double theta)
        {
            double shiftedTheta = theta - Angle;
            return Mode switch
            {
                CardioidMode.Cosine => Magnitude / 2 + (Magnitude / 2) * Math.Cos(shiftedTheta),
                CardioidMode.NegativeCosine => Magnitude / 2 + (Magnitude / 2) * -Math.Cos(shiftedTheta),
                CardioidMode.Sine => Magnitude / 2 + (Magnitude / 2) * Math.Sin(shiftedTheta),
                _ => Magnitude / 2 + (Magnitude / 2) * Math.Cos(shiftedTheta),
            };
        }

        public Vector2 GetPoint(double theta)
        {
            double r = GetRadius(theta);
            return new Vector2((float)(r * Math.Cos(theta)), (float)(r * Math.Sin(theta)));
        }

        public List<Vector2> GetPoints(int resolution = 180)
        {
            List<Vector2> points = new();
            for (int i = 0; i <= resolution; i++)
            {
                double theta = 2 * Math.PI * i / resolution;
                points.Add(GetPoint(theta));
            }
            return points;
        }

        public static List<Vector2> Merge(Cardioid a, Cardioid b, int resolution = 180)
        {
            List<Vector2> points = new();
            for (int i = 0; i <= resolution; i++)
            {
                double theta = 2 * Math.PI * i / resolution;
                double ra = a.GetRadius(theta);
                double rb = b.GetRadius(theta);
                double r = new[] { ra, rb }.SoftMax();
                points.Add(new Vector2((float)(r * Math.Cos(theta)), (float)(r * Math.Sin(theta))));
            }
            return points;
        }

        public static List<Vector2> Merge(IEnumerable<Cardioid> cardioids, int resolution = 180)
        {
            var cardioidList = cardioids.ToList();
            List<Vector2> points = new List<Vector2>();

            for (int i = 0; i <= resolution; i++)
            {
                // Proper scale theta based on the resolution
                double theta = 2 * Math.PI * i / resolution;

                // Compute the radii on this theta and get the soft max
                double[] radii = cardioidList.Select(c => c.GetRadius(theta)).ToArray();
                double r = radii.SoftMax();
                points.Add(new Vector2((float)(r * Math.Cos(theta)), (float)(r * Math.Sin(theta))));
            }
            return points;
        }

    }
}