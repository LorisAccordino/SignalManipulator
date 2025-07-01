using SignalManipulator.UI.Components.Labels;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SignalManipulator.UI.Components
{
    public class TimeSlider : UserControl
    {
        private const int CONSTRAINT_SIZE = 30;
        private Size ConstraintMinSize => new Size(base.MinimumSize.Width, CONSTRAINT_SIZE);
        private Size ConstraintMaxSize => new Size(base.MaximumSize.Width, CONSTRAINT_SIZE);

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Size MinimumSize { get => ConstraintMinSize; set => base.MinimumSize = ConstraintMinSize; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Size MaximumSize { get => ConstraintMaxSize; set => base.MaximumSize = ConstraintMaxSize; }

        private readonly TrackBar trackBar;
        private readonly TimeLabel timeLabel;
        private TimeSpan totalTime = TimeSpan.FromMinutes(1); // Default
        private bool isSeeking = false;
        private bool autoUpdate = true;

        public event Action<TimeSpan>? OnSeek;
        public event Action<TimeSpan>? OnPositionChanged;

        [Browsable(false)]
        public TimeSpan CurrentTime
        {
            get => TimeSpan.FromSeconds(trackBar.Value);
            set
            {
                if (!isSeeking && value <= totalTime)
                    trackBar.Value = (int)value.TotalSeconds;
            }
        }

        [Browsable(false)]
        public TimeSpan TotalTime
        {
            get => totalTime;
            set
            {
                totalTime = value;
                trackBar.Maximum = Math.Max(1, (int)Math.Ceiling(value.TotalSeconds));
                timeLabel.Time = CurrentTime;
            }
        }

        [Category("Behavior")]
        public bool AutoUpdate
        {
            get => autoUpdate;
            set => autoUpdate = value;
        }

        [Category("Appearance")]
        public string TimeFormat { get => timeLabel.Format; set => timeLabel.Format = value; }
        [Category("Appearance")]
        public override Font Font { get => timeLabel.Font; set => timeLabel.Font = value; }

        public TimeSlider()
        {
            // Set constraints
            MinimumSize = ConstraintMinSize;
            MaximumSize = ConstraintMaxSize;

            trackBar = new TrackBar
            {
                Dock = DockStyle.Fill,
                Minimum = 0,
                Maximum = 60,
                TickStyle = TickStyle.None
            };

            timeLabel = new TimeLabel
            {
                Dock = DockStyle.Right,
                Width = 65,
                Height = 30,
                Padding = new Padding(0, 2, 0, 0),
                TextAlign = ContentAlignment.TopLeft,
                Format = @"mm\:ss\.fff"
            };

            Height = trackBar.Height;
            Controls.Add(trackBar);
            Controls.Add(timeLabel);

            trackBar.ValueChanged += (s, e) =>
            {
                if (!isSeeking)
                    timeLabel.Time = CurrentTime;

                OnPositionChanged?.Invoke(CurrentTime);
            };

            trackBar.MouseDown += (s, e) => isSeeking = true;
            trackBar.MouseUp += (s, e) =>
            {
                isSeeking = false;
                OnSeek?.Invoke(CurrentTime);
            };
        }

        public void SyncWith(TimeSpan current)
        {
            if (autoUpdate && !isSeeking)
            {
                trackBar.Value = (int)Math.Min(current.TotalSeconds, trackBar.Maximum);
                timeLabel.Time = current;
            }
        }
    }
}
