using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SignalManipulator.UI.Components.Precision
{
    public class PrecisionSlider : PrecisionControl
    {
        private TrackBar trackBar;
        private Label descriptionLabel;
        private ValueLabel valueLabel;

        //private bool isUpdatingValue = false;


        [DefaultValue("Value: ")]
        public override string Text
        {
            get { return descriptionLabel.Text; }
            set
            {
                descriptionLabel.Text = value;
                AdjustControls();
            }
        }

        public override double Value
        {
            get => base.Value;
            set
            {
                base.Value = value;
                valueLabel.Value = value;
            }
        }

        [DefaultValue(Orientation.Horizontal)]
        public Orientation Orientation
        {
            get => trackBar.Orientation;
            set
            {
                trackBar.Orientation = value;
                AdjustSizeForOrientation(value);
                AdjustControls();
            }
        }

        public int TickFrequency
        {
            get => trackBar.TickFrequency;
            set => trackBar.TickFrequency = value;
        }


        public PrecisionSlider()
        {
            // Initialize controls
            trackBar = new TrackBar();
            descriptionLabel = new Label();
            //valueLabel = new Label();
            valueLabel = new ValueLabel();

            descriptionLabel.AutoSize = true;
            valueLabel.AutoSize = true;

            // Initializes the component properties and styles
            Orientation = Orientation.Horizontal;

            // Event handling
            //trackBar.ValueChanged += TrackBarValueChanged;
            Resize += (sender, e) => AdjustControls();

            // Initialize default properties
            Precision = 0.01f;
            Minimum = 0.0f;
            Maximum = 10f;
            Value = 2.5f;
            Text = "Value: ";
            //valueSuffix = "";

            // Add controls to the PrecisionSlider component
            Controls.Add(trackBar);
            Controls.Add(descriptionLabel);
            Controls.Add(valueLabel);

            // Adjust controls
            AdjustControls();

            OnValueChanged(this, Value);
        }

        /*
        private void TrackBarValueChanged(object sender, EventArgs e)
        {
            if (!isUpdatingValue)
            {
                isUpdatingValue = true;
                double newValue = ScaleMapper.ToRealValue(trackBar.Value, trackBar.Minimum, trackBar.Maximum, PrecisionScale);
                internalValue = Math.Clamp(newValue, Minimum, Maximum);
                UpdateValueLabel();
                OnValueChanged(new ValueEventArgs<double>(internalValue));
                isUpdatingValue = false;
            }
        }
        */

        private void UpdateTrackBarRange()
        {
            trackBar.Minimum = (int)(Minimum / Precision);
            trackBar.Maximum = (int)(Maximum / Precision);
        }

        private void AdjustSizeForOrientation(Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
            {
                Height = 56;
                Width = 300;
            }
            else
            {
                Width = 56;
                Height = 300;
            }
        }

        private void AdjustControls()
        {
            if (Orientation == Orientation.Horizontal)
            {
                trackBar.Left = descriptionLabel.Width;
                //trackBar.Width = Width - (descriptionLabel.Width + MaxValueLabelWidth());
                descriptionLabel.Location = new Point(0, 0);
                valueLabel.Location = new Point(descriptionLabel.Width + trackBar.Width, 0);
            }
            else
            {
                descriptionLabel.Visible = false;
                valueLabel.Location = new Point(0, Height - valueLabel.Height);
                trackBar.Location = new Point(0, 0);
                trackBar.Height = Height - valueLabel.Height;
            }
        }

        /*private int MaxValueLabelWidth()
        {
            valueLabel.Text = $"{Maximum:F2}{ValueSuffix}";
            int max = valueLabel.Width;
            valueLabel.Text = $"{Minimum:F2}{ValueSuffix}";
            int min = valueLabel.Width;
            valueLabel.Text = $"{Value:F2}{ValueSuffix}";
            return Math.Max(min, max);
        }*/
    }
}