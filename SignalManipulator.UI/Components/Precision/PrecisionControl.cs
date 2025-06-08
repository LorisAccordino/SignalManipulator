using System.ComponentModel;
using System;
using System.Windows.Forms;
using SignalManipulator.UI.Scaling;
using SignalManipulator.UI.Scaling.Curves;
using SignalManipulator.UI.Helpers;

namespace SignalManipulator.UI.Components.Precision
{
    public enum PrecisionScale
    {
        Linear,
        Logarithmic,
        Exponential
    }

    public class PrecisionControl : Control
    {
        // Events
        public event EventHandler<double> ValueChanged;
        public event EventHandler RangeChanged;
        public event EventHandler PrecisionChanged;
        public event EventHandler ScaleChanged;
        public event EventHandler PrecisionSettingsChanged;

        // Precision properties
        private double precision = 0.01;
        private double minimum = 0.0;
        private double value = 0.0;
        private double maximum = 10.0;
        private double curvature = 1.0;
        private PrecisionScale precisionScale = PrecisionScale.Linear;
        private NonLinearScaleMapper scaleMapper;


        [DefaultValue(0.0)]
        public virtual double Minimum
        {
            get => minimum;
            set
            {
                if (minimum != value)
                {
                    minimum = value;

                    // Ensure range is valid
                    if (minimum > maximum)
                        maximum = minimum;

                    Value = value;
                    RangeChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        [DefaultValue(10.0)]
        public virtual double Maximum
        {
            get => maximum;
            set
            {
                if (maximum != value)
                {
                    maximum = value;

                    // Ensure range is valid
                    if (maximum < minimum)
                        minimum = maximum;

                    Value = value;
                    RangeChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        [DefaultValue(0.0)]
        public virtual double Value
        {
            get => value;
            set
            {
                double clampled = MathHelper.Clamp(value, Minimum, Maximum);
                if (this.value != clampled)
                {
                    this.value = clampled;
                    ValueChanged?.Invoke(this, clampled);
                }
            }
        }

        [DefaultValue(0.01)]
        public virtual double Precision
        {
            get => precision;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(Precision));
                precision = value;
                PrecisionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        [DefaultValue(1.0)]
        public double Curvature
        {
            get => curvature;
            set
            {
                new ExpCurve(value); // Test the curve
                curvature = value;
                PrecisionSettingsChanged?.Invoke(this, EventArgs.Empty);
                UpdateUIFromValue(Value);
            }
        }

        [DefaultValue(PrecisionScale.Linear)]
        public PrecisionScale PrecisionScale
        {
            get => precisionScale;
            set
            {
                precisionScale = value;
                ScaleChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        protected NonLinearScaleMapper ScaleMapper
        {
            get
            {
                scaleMapper = new NonLinearScaleMapper(Minimum, Maximum, Precision);

                switch (PrecisionScale)
                {
                    case PrecisionScale.Logarithmic: 
                        scaleMapper.SetCurve(new LogCurve(curvature));
                        break;
                    case PrecisionScale.Exponential:
                        scaleMapper.SetCurve(new ExpCurve(curvature));
                        break;
                    default:
                        scaleMapper.SetCurve(new LinearCurve());
                        break;
                }

                return scaleMapper;
            }
        }

        public PrecisionControl()
        {
            ValueChanged += (s, e) => UpdateUIFromValue(Value);

            RangeChanged += (s, e) => PrecisionSettingsChanged(s, e);
            PrecisionChanged += (s, e) => PrecisionSettingsChanged(s, e);
            ScaleChanged += (s, e) => PrecisionSettingsChanged(s, e);

            PrecisionSettingsChanged += (s, e) => UpdateUIFromValue(Value);
        }

        protected virtual void UpdateUIFromValue(double value) { }
        protected virtual void UpdateValueFromUI() { }
    }
}