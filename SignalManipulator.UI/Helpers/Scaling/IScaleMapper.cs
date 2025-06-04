namespace SignalManipulator.UI.Helpers.Scaling
{
    public interface IScaleMapper
    {
        void Configure(double realMin, double realMax, double precision);
        int ToControlUnits(double realValue);
        double ToRealValue(int controlUnits);
    }
}