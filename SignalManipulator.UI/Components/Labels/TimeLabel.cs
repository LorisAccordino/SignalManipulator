using ScottPlot.Plottables;
using System.ComponentModel;
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
    }
}