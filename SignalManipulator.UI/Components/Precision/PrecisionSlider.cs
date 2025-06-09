using SignalManipulator.UI.Helpers;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SignalManipulator.UI.Components.Precision
{
    [DesignerCategory("Code")]
    [DefaultProperty("Value")]
    [DefaultEvent("ValueChanged")]
    public class PrecisionSlider : PrecisionControl
    {
        private const int CONSTRAINT_SIZE = 30;
        private const int DEFAULT_SIZE = 200;
        private const int MIN_TRACKBAR_SIZE = 100;
        private const int Y_LBL_OFFSET = 4;

        private TrackBar trackBar;
        private Label descriptionLabel;
        private ValueLabel valueLabel;

        private bool showDescription = true;
        private bool showValue = true;

        private bool isUpdatingValue = false;


        /*** HIDDEN PROPERTIES ***/

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text { get => ""; set => base.Text = ""; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Size MinimumSize { get => base.MinimumSize; set => base.MinimumSize = value; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Size MaximumSize { get => base.MaximumSize; set => base.MaximumSize = value; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool AutoSize { get => base.AutoSize; set => base.AutoSize = value; }

        /*************************/

        [DefaultValue(true)]
        public bool ShowDescription
        {
            get => showDescription;
            set
            {
                if (showDescription != value)
                {
                    showDescription = value;
                    ApplyOrientationConstraints();
                    AdjustUI();
                }
            }
        }

        [DefaultValue(true)]
        public bool ShowValue
        {
            get => showValue;
            set
            {
                if (showValue != value)
                {
                    showValue = value;
                    ApplyOrientationConstraints();
                    AdjustUI();
                }
            }
        }

        [DefaultValue("Value:")]
        public string Description
        {
            get => descriptionLabel.Text;
            set
            {
                if (descriptionLabel.Text != value)
                {
                    descriptionLabel.Text = value;
                    AdjustUI();
                }
            }
        }

        [DefaultValue("")]
        public string Suffix
        {
            get => valueLabel.Suffix;
            set
            {
                if (valueLabel.Suffix != value)
                {
                    valueLabel.Suffix = value;
                    AdjustUI();
                }
            }
        }

        public override double Maximum
        {
            get => base.Maximum;
            set
            {
                if (base.Maximum != value)
                {
                    base.Maximum = value;
                    AdjustUI();
                }
            }
        }

        public override double Value 
        { 
            get => base.Value;
            set
            {
                base.Value = value;
                valueLabel.Value = base.Value;
            }
        }

        public override double Precision
        {
            get => base.Precision;
            set
            {
                base.Precision = value;
                valueLabel.DecimalPlaces = MathHelper.GetDecimalPlaces(base.Precision);
            }
        }


        [DefaultValue(Orientation.Horizontal)]
        public Orientation Orientation
        {
            get => trackBar.Orientation;
            set
            {
                if (trackBar.Orientation != value)
                {
                    trackBar.Orientation = value;
                    ApplyOrientationConstraints();
                    AdjustUI();
                }
            }
        }

        public int TickFrequency { get => trackBar.TickFrequency; set => trackBar.TickFrequency = value; }


        public PrecisionSlider()
        {
            // Create controls
            trackBar = new TrackBar();
            descriptionLabel = new Label { AutoSize = true };
            valueLabel = new ValueLabel();

            Controls.Add(trackBar);
            Controls.Add(descriptionLabel);
            Controls.Add(valueLabel);

            // Register events
            trackBar.ValueChanged += (s, e) => UpdateValueFromUI(Value);
            PrecisionSettingsChanged += (s, e) => UpdateTrackbarSettings();

            // Initialize some default properties
            Description = "Value:";
            Width = DEFAULT_SIZE;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            UpdateTrackbarSettings();
            ApplyOrientationConstraints();
            AdjustUI();
        }

        private int GetValueLabelWidth() =>
            showValue ? valueLabel.EstimateRequiredWidth(MathHelper.GetIntegerDigits(Maximum), apply: true) : 0;

        private int GetValueLabelHeight() =>
            showValue ? valueLabel.Height : 0;

        private int GetDescriptionLabelWidth() =>
            showDescription ? descriptionLabel.Width : 0;


        public override Size GetPreferredSize(Size proposedSize)
        {
            if (Orientation == Orientation.Horizontal)
            {
                int w = GetDescriptionLabelWidth() + GetValueLabelWidth() + MIN_TRACKBAR_SIZE;
                return new Size(w, CONSTRAINT_SIZE);
            }
            else
            {
                int h = MIN_TRACKBAR_SIZE + GetValueLabelHeight();
                return new Size(CONSTRAINT_SIZE, h);
            }
        }


        private void ApplyOrientationConstraints()
        {
            Size preferred = GetPreferredSize(Size);

            if (Orientation == Orientation.Horizontal)
            {
                Size = new Size(Math.Max(Width, preferred.Width), CONSTRAINT_SIZE);
                MinimumSize = new Size(preferred.Width, CONSTRAINT_SIZE);
                MaximumSize = new Size(int.MaxValue, CONSTRAINT_SIZE);
            }
            else
            {
                Size = new Size(CONSTRAINT_SIZE, Math.Max(Height, preferred.Height));
                MinimumSize = new Size(CONSTRAINT_SIZE, preferred.Height);
                MaximumSize = new Size(CONSTRAINT_SIZE, int.MaxValue);
            }
        }


        private void AdjustUI()
        {
            int valueWidth = GetValueLabelWidth();
            int valueHeight = GetValueLabelHeight();
            int descWidth = GetDescriptionLabelWidth();
            int labelsWidth = descWidth + valueWidth;

            descriptionLabel.Visible = showDescription && !(Orientation == Orientation.Vertical);
            valueLabel.Visible = showValue;

            if (Orientation == Orientation.Horizontal)
            {
                trackBar.Location = new Point(descWidth, 0);
                trackBar.Size = new Size(Width - labelsWidth, CONSTRAINT_SIZE);
                trackBar.Anchor = AnchorStyles.Left | AnchorStyles.Right;

                if (showDescription)
                {
                    descriptionLabel.Location = new Point(0, Y_LBL_OFFSET);
                    descriptionLabel.Anchor = AnchorStyles.Left;
                }

                if (showValue)
                {
                    valueLabel.Location = new Point(descWidth + trackBar.Width, Y_LBL_OFFSET);
                    valueLabel.Anchor = AnchorStyles.Right;
                }
            }
            else // Vertical
            {
                trackBar.Location = new Point(0, 0);
                trackBar.Size = new Size(CONSTRAINT_SIZE, Height - valueHeight);
                trackBar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;

                if (showValue)
                {
                    valueLabel.Location = new Point(0, trackBar.Bottom + Y_LBL_OFFSET);
                    valueLabel.Anchor = AnchorStyles.Bottom;
                }
            }
        }

        protected override void UpdateValueFromUI(double value)
        {
            if (isUpdatingValue)
                return;

            isUpdatingValue = true;
            try
            {
                Value = ScaleMapper.ToRealValue(trackBar.Value);
                base.UpdateValueFromUI(Value);
            }
            finally
            {
                isUpdatingValue = false;
            }
        }

        protected override void UpdateUIFromValue(double value)
        {
            if (isUpdatingValue)
                return;

            isUpdatingValue = true;
            try
            {
                trackBar.Value = ScaleMapper.ToControlUnits(value);
            }
            finally
            {
                isUpdatingValue = false;
            }
        }

        private void UpdateTrackbarSettings()
        {
            trackBar.ApplySettings(TrackBarHelper.GetTrackBarSettings(ScaleMapper));
        }
    }
}