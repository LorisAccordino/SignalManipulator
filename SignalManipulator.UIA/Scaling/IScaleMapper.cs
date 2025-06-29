namespace SignalManipulator.UI.Scaling
{
    public interface IScaleMapper
    {
        //void SetCurve(INonLinearCurve curve);
        int ToControlUnits(double realValue);
        double ToRealValue(int controlUnits);
    }
}