using System.ComponentModel;
using System;
using System.Windows.Forms;
using SignalManipulator.UI.Helpers.Scaling;

namespace SignalManipulator.UI.Components.Precision
{
    public class PrecisionControl : Control
    {
        // Events
        public event EventHandler<double> ValueChanged;

        // Precision properties
        private double precision = 0.01;
        private double minimum = 0;
        private double value = 2.5;
        private double maximum = 10;
        private PrecisionScale precisionScale = PrecisionScale.Linear;


        [DefaultValue(0.01f)]
        public double Precision
        {
            get => precision;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(Precision));
                precision = value;
                OnPrecisionChanged();
            }
        }

        [DefaultValue(0)]
        public double Minimum
        {
            get => minimum;
            set
            {
                minimum = value;
                OnRangeChanged();
            }
        }

        [DefaultValue(2.5)]
        public virtual double Value
        {
            get => value;
            set
            {
                double clampled = Math.Min(Math.Max(value, Minimum), Maximum);
                if (this.value != clampled)
                {
                    this.value = clampled;
                    OnValueChanged(this, clampled);
                    UpdateUIFromValue(clampled);
                }
            }
        }

        [DefaultValue(10)]
        public double Maximum
        {
            get => maximum;
            set
            {
                maximum = value;
                OnRangeChanged();
            }
        }

        [DefaultValue(PrecisionScale.Linear)]
        public PrecisionScale PrecisionScale
        {
            get => precisionScale;
            set
            {
                precisionScale = value;
                OnScaleChanged();
            }
        }

        protected IScaleMapper ScaleMapper
        {
            get
            {
                switch (PrecisionScale)
                {
                    case PrecisionScale.Logarithmic:
                        return new LogScaleMapper(Minimum, Maximum, Precision);
                    case PrecisionScale.Exponential:
                        return new ExpScaleMapper(Minimum, Maximum, Precision);
                    default:
                        return new LinearScaleMapper(Minimum, Maximum, Precision);
                }
            }
        }


        protected virtual void OnValueChanged(object sender, double e)
        {
            ValueChanged?.Invoke(this, e);
        }

        protected virtual void OnRangeChanged() => UpdateUIFromValue(Value);
        protected virtual void OnPrecisionChanged() => UpdateUIFromValue(Value);
        protected virtual void OnScaleChanged() => UpdateUIFromValue(Value);


        protected virtual void UpdateUIFromValue(double value) { }
        protected virtual void UpdateValueFromUI(double value) { }

    }
}