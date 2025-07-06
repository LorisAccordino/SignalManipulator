using ScottPlot;
using ScottPlot.Plottables;
using SignalManipulator.Logic.AudioMath;
using SignalManipulator.Logic.AudioMath.Smoothing;
using SignalManipulator.Logic.Models;
using SignalManipulator.UI.Controls.Plottables.Radars;

namespace SignalManipulator.UI.Controls.Plottables
{
    public class PolarPanningPlot : IPlottable
    {
        protected object lockObject = new object();

        protected Plot Plot { get; }
        protected SmoothRadar Radar { get; } = new SmoothRadar();
        protected double[] pointsPerChannel = [1.0, 0.7, 0.2, 0.2, 0.7]; // C, L, rL, rR, R

        protected double[] left;
        protected double[] right;
        

        public PolarPanningPlot(Plot plot)
        {
            left = new double[3];
            right = new double[3];

            // Aggiungi i dati
            Radar.Series.Add(new RadarSeries
            {
                Values = pointsPerChannel
            });

            // Imposta l'asse se necessario (opzionale)
            Radar.PolarAxis = new PolarAxis
            {
                Rotation = Angle.FromDegrees(90),
                ManageAxisLimits = true
            };
            Radar.PolarAxis.SetSpokes(5, 1.0);
            Radar.Smoothness = 1.0f;

            Plot = plot;
            //Radar = Plot.Add.Radar(pointsPerChannel);
            Plot.Add.Plottable(Radar);
        }

        Smoother smoother = new SmootherEMA(0.35);

        public void AddSamples(double[] samples)
        {
            lock (lockObject)
            {
                if (left.Length != samples.Length / 2) left = new double[samples.Length / 2];
                if (right.Length != samples.Length / 2) right = new double[samples.Length / 2];
                samples.SplitStereo(left, right);

                VolumeFrame leftVolume = new VolumeFrame(left.ToFloat());
                VolumeFrame rightVolume = new VolumeFrame(right.ToFloat());

                double leftRMS = leftVolume.RMS * 1.5;
                double rightRMS = rightVolume.RMS * 1.5;
                double centerRMS = (leftRMS + rightRMS) / 2;

                // Polar coordinates
                var magnitudes = new[] { centerRMS, leftRMS, 0.2, 0.2, rightRMS }; // C, L, rL, rR, R
                Radar.Series[0].Values = smoother.Smooth(magnitudes);
            }
        }

        public void ClearBuffer()
        {
            lock (lockObject)
            {
                Array.Clear(pointsPerChannel);
            }
        }


        // IPlottable methods to implement
        public bool IsVisible { get => Radar.IsVisible; set => Radar.IsVisible = value; }
        public IAxes Axes { get => Radar.Axes; set => Radar.Axes = value; }
        public IEnumerable<LegendItem> LegendItems => Radar.LegendItems;
        public AxisLimits GetAxisLimits() => Radar.GetAxisLimits();
        public void Render(RenderPack rp) => Radar.Render(rp);
    }
}
