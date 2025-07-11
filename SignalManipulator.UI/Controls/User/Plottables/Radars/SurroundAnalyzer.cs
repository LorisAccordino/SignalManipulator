using ScottPlot;
using SignalManipulator.Logic.AudioMath.Scaling;
using SignalManipulator.Logic.AudioMath.Scaling.Curves;
using SignalManipulator.Logic.AudioMath.Smoothing;
using SignalManipulator.Logic.Data;

namespace SignalManipulator.UI.Controls.User.Plottables.Radars
{
    public class SurroundAnalyzer : CardioidRadar
    {
        // Smoothing and scaling
        private Smoother smootherEMA = new SmootherEMA(0.3);
        private Smoother smootherSMA = new SmootherSMA(2);
        private NonLinearScaleMapper scaleMapper = new NonLinearScaleMapper(0.0, 0.95, 10e-6) { Curve = new LogCurve(2.5) };

        protected object lockObject = new object();
        private double[] magnitudes = [0.0, 0.0, 0.0, 0.0, 0.0]; // rL, L, C, R, rR


        public SurroundAnalyzer() : base(["rL", "L", "C", "R", "rR"])
        {
            Radius = 0.65;
            Rotation = Angle.FromFraction(2.0 / 5.0) + Angle.FromDegrees(90); // Rotate 2/5 + 90°
        }

        public void AddData(VolumeMetrics volume)
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

                // Apply smoothing (EMA then SMA), and then scale
                double[] smoothed = smootherSMA.Smooth(smootherEMA.Smooth(rawMagnitudes));
                magnitudes = scaleMapper.ToRealValues(smoothed);
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