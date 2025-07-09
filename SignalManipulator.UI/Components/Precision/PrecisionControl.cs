using System.ComponentModel;
using System.Windows.Forms;
using SignalManipulator.Logic.AudioMath.Scaling.Curves;
using System.Diagnostics.CodeAnalysis;
using SignalManipulator.Logic.AudioMath.Scaling;

namespace SignalManipulator.UI.Components.Precision
{
    public enum PrecisionScale
    {
        Linear,
        Logarithmic,
        Exponential
    }

    public enum ValueUpdateMode
    {
        None,              // It doesn't fire events
        UserOnly,          // Only when the user changes the value
        ProgrammaticOnly,  // Only when the code changes the value
        Both               // It always fire events
    }

    [ExcludeFromCodeCoverage]
    public abstract class PrecisionControl : Control
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

                    // Refresh value
                    SetValue(Value, false);

                    // Fire event
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

                    // Refresh value
                    SetValue(Value, false);

                    // Fire event
                    RangeChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        [DefaultValue(ValueUpdateMode.Both)]
        public virtual ValueUpdateMode UpdateMode { get; set; } = ValueUpdateMode.Both;

        [DefaultValue(0.0)]
        public virtual double Value
        {
            get => value;
            set => SetValue(value, fromUser: false); // Normal behaviour
        }

        protected virtual void SetValue(double newValue, bool fromUser)
        {
            double clamped = double.Clamp(newValue, Minimum, Maximum);

            if (value == clamped)
                return;

            value = clamped;

            UpdateUIFromValue(value);

            bool shouldFire = false;

            if (UpdateMode == ValueUpdateMode.Both)
                shouldFire = true;
            else if (UpdateMode == ValueUpdateMode.UserOnly && fromUser)
                shouldFire = true;
            else if (UpdateMode == ValueUpdateMode.ProgrammaticOnly && !fromUser)
                shouldFire = true;
            // ValueUpdateMode.None → shouldFire remains false

            if (shouldFire)
                ValueChanged?.Invoke(this, clamped);
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
                if (curvature != value)
                {
                    new ExpCurve(value); // Test the curve
                    curvature = value;
                    PrecisionSettingsChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        [DefaultValue(PrecisionScale.Linear)]
        public PrecisionScale PrecisionScale
        {
            get => precisionScale;
            set
            {
                if (precisionScale != value)
                {
                    precisionScale = value;
                    ScaleChanged?.Invoke(this, EventArgs.Empty);
                }
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
                        scaleMapper.Curve = new LogCurve(curvature);
                        break;
                    case PrecisionScale.Exponential:
                        scaleMapper.Curve = new ExpCurve(curvature);
                        break;
                    default:
                        scaleMapper.Curve = new LinearCurve();
                        break;
                }

                return scaleMapper;
            }
        }

        public PrecisionControl()
        {
            RangeChanged += (s, e) => PrecisionSettingsChanged(s, e);
            PrecisionChanged += (s, e) => PrecisionSettingsChanged(s, e);
            ScaleChanged += (s, e) => PrecisionSettingsChanged(s, e);

            PrecisionSettingsChanged += (s, e) => UpdateUIFromValue(Value);
        }

        protected abstract void UpdateUIFromValue(double value);
        protected abstract void UpdateValueFromUI();
    }
}