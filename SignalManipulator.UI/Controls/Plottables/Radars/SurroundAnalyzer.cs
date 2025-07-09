using ScottPlot;
using SignalManipulator.Logic.AudioMath;
using SignalManipulator.Logic.AudioMath.Objects.Smoothing;
using SignalManipulator.Logic.AudioMath.Smoothing;
using SignalManipulator.Logic.Models;

namespace SignalManipulator.UI.Controls.Plottables.Radars
{
    public class SurroundAnalyzer : CardioidRadar
    {
        private Smoother smootherEMA = new SmootherEMA(0.3);
        private Smoother smootherSMA = new SmootherSMA(2);

        protected object lockObject = new object();
        private double[] magnitudes = [0.0, 0.0, 0.0, 0.0, 0.0]; // rL, L, C, R, rR


        public SurroundAnalyzer() : base(["rL", "L", "C", "R", "rR"])
        {
            Radius = 0.65;
            Rotation = Angle.FromFraction(2.0 / 5.0) + Angle.FromDegrees(90); // Rotate 2/5 + 90°
        }

        public void AddData(VolumeFrame volume)
        {
            lock (lockObject)
            {
                // Compute RMS for each channel
                double leftRMS = volume.LeftRMS;
                double rightRMS = volume.RightRMS;

                // Center (mid) and difference (side)
                double mid = volume.Mid;
                double side = volume.Side;

                // Rear channels attenuated
                double rearL = side * leftRMS;
                double rearR = side * rightRMS;

                // Recostruction of values in this order: rL, L, C, R, rR
                double[] rawMagnitudes =
                [
                    rearL,
                    leftRMS,
                    mid,
                    rightRMS,
                    rearR
                ];

                // Apply smoothing (EMA then SMA), and then exaggerate
                double[] smoothed = smootherSMA.Smooth(smootherEMA.Smooth(rawMagnitudes));
                smoothed.Exaggerate(scale: 0.9);
                magnitudes = smoothed;
                SetMagnitudes(magnitudes);
            }
        }

        public void Clear()
        {
            lock (lockObject)
            {
                Array.Clear(magnitudes);
                SetMagnitudes(magnitudes);
            }
        }
    }
}