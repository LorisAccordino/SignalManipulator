namespace SignalManipulator.UI.Helpers.Scaling
{
    public interface IScaleMapper
    {
        int ToControlUnits(double realValue, float realMin, float realMax, float precision, int controlMin, int controlMax);
        double ToRealValue(int controlUnits, float realMin, float realMax, float precision, int controlMin, int controlMax);
    }
}