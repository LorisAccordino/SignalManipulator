namespace SignalManipulator.Logic.AudioMath.Scaling.Curves
{
    public interface INonLinearCurve
    {
        double Forward(double x);  // Transform x ∈ [0,1] in curve ∈ [0,1]
        double Inverse(double y);  // Invert the curve
    }
}