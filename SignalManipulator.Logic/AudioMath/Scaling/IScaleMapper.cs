namespace SignalManipulator.Logic.AudioMath.Scaling
{
    public interface IScaleMapper
    {
        int ToControlUnits(double realValue);
        int[] ToControlUnits(double[] realValues);
        double ToRealValue(double realValue);
        double[] ToRealValues(double[] realValues);
        double ToRealValue(int controlUnits);
    }
}