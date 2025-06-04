using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace SignalManipulator.UI.Components.Precision
{
    public partial class ValueLabel : Label
    {
        private double value;
        private string suffix = "";
        private int precision = 2;

        [DefaultValue(0.0)]
        public double Value
        {
            get => value;
            set
            {
                this.value = value;
                UpdateText();
            }
        }

        [DefaultValue("")]
        public string Suffix
        {
            get => suffix;
            set
            {
                suffix = value;
                UpdateText();
            }
        }

        [DefaultValue(2)]
        public int Precision
        {
            get => precision;
            set
            {
                precision = value;
                UpdateText();
            }
        }

        public new string Text { get; private set; }

        public void UpdateValue(double newValue)
        {
            Value = newValue;
        }

        private void UpdateText()
        {
            Text = Value.ToString($"F{precision}", CultureInfo.InvariantCulture) + suffix;
        }

        public ValueLabel()
        {
            InitializeComponent();
        }

        public ValueLabel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
