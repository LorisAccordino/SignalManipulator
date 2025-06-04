namespace SignalManipulator.UI.Helpers.Scaling
{
    public abstract class BaseScaleMapper : IScaleMapper
    {
        protected double realMin;
        protected double realMax;
        protected double precision;

        public BaseScaleMapper(double realMin, double realMax, double precision) => Configure(realMin, realMax, precision);

        public virtual void Configure(double realMin, double realMax, double precision)
        {
            this.realMin = realMin;
            this.realMax = realMax;
            this.precision = precision;
        }

        public abstract int ToControlUnits(double realValue);
        public abstract double ToRealValue(int controlUnits);
    }
}