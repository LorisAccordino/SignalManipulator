namespace SignalManipulator.Logic.AudioMath.Smoothing
{
    public abstract class Smoother
    {
        public abstract double[] Smooth(double[] input);
        public abstract void Set(double alpha);
    }
}