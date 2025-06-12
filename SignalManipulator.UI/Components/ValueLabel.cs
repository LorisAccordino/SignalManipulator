using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace SignalManipulator.UI.Components
{
    [ToolboxItem(false)]
    [ExcludeFromCodeCoverage]
    public partial class ValueLabel : Label
    {
        private double value;
        private string suffix = "";
        private int decimalPlaces = 2;

        [DefaultValue(0.0)]
        public double Value
        {
            get => value;
            set
            {
                if (Math.Abs(this.value - value) > double.Epsilon)
                {
                    this.value = value;
                    UpdateText();
                }
            }
        }

        [DefaultValue("")]
        public string Suffix
        {
            get => suffix;
            set
            {
                if (suffix != value)
                {
                    suffix = value;
                    UpdateText();
                }
            }
        }

        [DefaultValue(2)]
        public int DecimalPlaces
        {
            get => decimalPlaces;
            set
            {
                if (decimalPlaces != value)
                {
                    decimalPlaces = value;
                    UpdateText();
                }
            }
        }

        public string FormattedText => base.Text;

        private void UpdateText()
        {
            base.Text = Value.ToString($"F{decimalPlaces}", CultureInfo.InvariantCulture) + suffix;
        }

        public int EstimateRequiredWidth(int maxDigits = 3, int extraPixels = 6, bool apply = false)
        {
            // Create a dummy string that simulates the max value visualizable
            string fakeValue = new string('9', maxDigits) + "." + new string('9', decimalPlaces);
            string fullText = fakeValue + suffix;

            // Measure the size in pixel with the current font
            Size size = TextRenderer.MeasureText(fullText, Font);
            int totalWidth = size.Width + extraPixels;

            if (apply) Width = totalWidth;

            return totalWidth;
        }

        public ValueLabel()
        {
            InitializeComponent();
            AutoSize = false; // Mandatory to allow the assign of Width
            UpdateText();
        }

        public ValueLabel(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            AutoSize = false;
        }
    }
}
