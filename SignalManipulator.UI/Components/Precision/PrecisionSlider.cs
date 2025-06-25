using SignalManipulator.UI.Helpers;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;

namespace SignalManipulator.UI.Components.Precision
{
    [DesignerCategory("Code")]
    [DefaultProperty("Value")]
    [DefaultEvent("ValueChanged")]
    [ExcludeFromCodeCoverage]
    public class PrecisionSlider : PrecisionControl
    {
        private const int CONSTRAINT_SIZE = 30;
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
                }
            }
        }

        public override Font Font
        {
            get => base.Font; set
            {
                base.Font = value;
                descriptionLabel.Font = value;
                valueLabel.Font = value;
                AdjustUI();
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

        public override ValueUpdateMode UpdateMode
        {
            get => base.UpdateMode;
            set
            {
                if (base.UpdateMode != value)
                {
                    base.UpdateMode = value;
                    trackBar.Enabled = UpdateMode != ValueUpdateMode.ProgrammaticOnly;
                }
            }
        }

        protected override void SetValue(double newValue, bool fromUser)
        {
            base.SetValue(newValue, fromUser);
            valueLabel.Value = Value;
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
                }
            }
        }

        public TickStyle TickStyle { get => trackBar.TickStyle; set => trackBar.TickStyle = value; }

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
            trackBar.ValueChanged += (s, e) => UpdateValueFromUI();
            PrecisionSettingsChanged += (s, e) => UpdateTrackbarSettings();

            // Initialize some default properties
            Description = "Value:";
            //Width = DEFAULT_SIZE;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            UpdateTrackbarSettings();
            ApplyOrientationConstraints();
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
            int longSide = Math.Max(Width, Height);
            Size preferred = GetPreferredSize(Size);

            if (Orientation == Orientation.Horizontal)
            {
                MinimumSize = new Size(preferred.Width, CONSTRAINT_SIZE);
                MaximumSize = new Size(preferred.Width * 4, CONSTRAINT_SIZE);
                Size = new Size(longSide, CONSTRAINT_SIZE);
            }
            else // Vertical
            {
                MinimumSize = new Size(CONSTRAINT_SIZE, preferred.Height);
                MaximumSize = new Size(CONSTRAINT_SIZE, preferred.Height * 4);
                Size = new Size(CONSTRAINT_SIZE, longSide);
            }

            // Adjust UI after applying constraints
            AdjustUI();
        }


        private void AdjustUI()
        {
            int valueWidth = GetValueLabelWidth();
            int valueHeight = GetValueLabelHeight();
            int descWidth = GetDescriptionLabelWidth();
            int labelsWidth = descWidth + valueWidth;

            bool isHorizontal = Orientation == Orientation.Horizontal;

            descriptionLabel.Visible = showDescription && isHorizontal;
            valueLabel.Visible = showValue;

            // Trackbar
            Point trackLocation = isHorizontal ? new Point(descWidth, 0) : new Point(0, 0);
            Size trackSize = isHorizontal
                ? new Size(Width - labelsWidth, CONSTRAINT_SIZE)
                : new Size(CONSTRAINT_SIZE, Height - valueHeight);
            AnchorStyles trackAnchor = isHorizontal
                ? AnchorStyles.Left | AnchorStyles.Right
                : AnchorStyles.Top | AnchorStyles.Bottom;

            trackBar.Location = trackLocation;
            trackBar.Size = trackSize;
            trackBar.Anchor = trackAnchor;

            // Description label (only horizontal)
            if (showDescription && isHorizontal)
            {
                descriptionLabel.Location = new Point(0, Y_LBL_OFFSET);
                descriptionLabel.Anchor = AnchorStyles.Left;
            }

            // Value label
            if (showValue)
            {
                Point valueLocation = isHorizontal
                    ? new Point(descWidth + trackSize.Width, Y_LBL_OFFSET)
                    : new Point(0, trackSize.Height + Y_LBL_OFFSET);

                AnchorStyles valueAnchor = isHorizontal
                    ? AnchorStyles.Right
                    : AnchorStyles.Bottom;

                valueLabel.Location = valueLocation;
                valueLabel.Anchor = valueAnchor;
            }
        }

        protected override void UpdateValueFromUI()
        {
            if (isUpdatingValue)
                return;

            isUpdatingValue = true;
            try
            {
                SetValue(ScaleMapper.ToRealValue(trackBar.Value), true);
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