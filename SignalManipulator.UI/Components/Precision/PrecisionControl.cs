using System.ComponentModel;
using System;
using System.Windows.Forms;
using SignalManipulator.UI.Helpers.Scaling;

namespace SignalManipulator.UI.Components.Precision
{
    public abstract class PrecisionControl : Control
    {
        // Events
        public event EventHandler<double> ValueChanged;

        // Precision properties
        private float precision = 0.01f;
        private float minimum = 0;
        private float maximum = 10;
        private PrecisionScale precisionScale = PrecisionScale.Linear;


        [DefaultValue(0.01f)]
        public float Precision
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
        public float Minimum
        {
            get => minimum;
            set
            {
                minimum = value;
                OnRangeChanged();
            }
        }

        [DefaultValue(10)]
        public float Maximum
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
                        return new LogarithmicScaleMapper();
                    case PrecisionScale.Exponential:
                        return new ExponentialScaleMapper();
                    default:
                        return new LinearScaleMapper();
                }
            }
        }


        protected virtual void OnValueChanged(object sender, double e)
        {
            ValueChanged?.Invoke(this, e);
        }

        protected virtual void OnRangeChanged() { }
        protected virtual void OnPrecisionChanged() { }
        protected virtual void OnScaleChanged() { }
    }
}