namespace SignalManipulator.UI.Scaling.Curves
{
    public class LogCurve : INonLinearCurve
    {
        private readonly ExpCurve exp;

        public LogCurve(double curvature = 1) => exp = new ExpCurve(curvature);

        public double Forward(double x) => exp.Inverse(x);
        public double Inverse(double y) => exp.Forward(y);
    }

}