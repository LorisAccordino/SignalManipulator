using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SignalManipulator.UI.Components.Labels
{
    public partial class TimeLabel : Label
    {
        private TimeSpan time = TimeSpan.Zero;
        private string format = @"mm\:ss\.fff";

        // Hidden text
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string Text { get => base.Text; set => base.Text = value; }

        [Category("Data")]
        [Description("The time to show")]
        [DefaultValue("00:00:000")]
        public TimeSpan Time
        {
            get => time;
            set
            {
                time = value;
                FormatText();
            }
        }

        [Category("Appearance")]
        [Description("The format which the time is formatted (es. mm\\:ss\\.fff).")]
        [DefaultValue(@"mm\:ss\.fff")]
        public string Format
        {
            get => format;
            set
            {
                format = value ?? @"mm\:ss\.fff";
                FormatText();
            }
        }

        private void FormatText()
        {
            base.Text = time.ToString(format);
        }

        protected override void OnCreateControl()
        {
            FormatText();
        }

        public TimeLabel()
        {
            InitializeComponent();
            FormatText();
        }

        public TimeLabel(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            FormatText();
        }

        public int EstimateRequiredWidth(int extraPixels = 6, bool apply = false)
        {
            // Simulate a TimeSpan with max values for the current format
            // For example: mm:ss.fff => 59:59.999
            TimeSpan testSpan = new TimeSpan(1, 9, 59, 59, 999); // 1d 9h 59m 59s 999ms
            string formatted = testSpan.ToString(format);

            // Measure the resulting text with the current font
            Size size = TextRenderer.MeasureText(formatted, Font);
            int totalWidth = size.Width + extraPixels;

            if (apply) Width = totalWidth;

            return totalWidth;
        }
    }
}