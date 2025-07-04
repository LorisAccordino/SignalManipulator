using SignalManipulator.UI.Components.Labels;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;

namespace SignalManipulator.UI.Components
{
    public enum SideAlignment
    {
        Left,
        Right
    }

    [DesignerCategory("Code")]
    [DefaultProperty("CurrentTime")]
    [ExcludeFromCodeCoverage]
    public class TimeSlider : UserControl
    {
        private const int CONSTRAINT_SIZE = 30;
        private const int MIN_TRACKBAR_SIZE = 150;
        private const int Y_LBL_OFFSET = 4;

        private Size ConstraintMinSize => new Size(base.MinimumSize.Width, CONSTRAINT_SIZE);
        private Size ConstraintMaxSize => new Size(base.MaximumSize.Width, CONSTRAINT_SIZE);


        /*** HIDDEN PROPERTIES ***/

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Size MinimumSize { get => ConstraintMinSize; set => base.MinimumSize = ConstraintMinSize; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Size MaximumSize { get => ConstraintMaxSize; set => base.MaximumSize = ConstraintMaxSize; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool AutoSize { get => base.AutoSize; set => base.AutoSize = value; }

        /*************************/


        private TrackBar trackBar;
        private Label descriptionLabel;
        private TimeLabel timeLabel;

        private bool showDescription = true;
        private SideAlignment timeLabelAlignment = SideAlignment.Right;

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


        [DefaultValue(true)]
        public bool ShowDescription
        {
            get => showDescription;
            set
            {
                if (showDescription != value)
                {
                    showDescription = value;
                    ResizeUI();
                }
            }
        }

        [DefaultValue(SideAlignment.Right)]
        public SideAlignment TimeLabelAlignment
        {
            get => timeLabelAlignment;
            set { timeLabelAlignment = value; ResizeUI(); }
        }

        [Category("Appearance")]
        public string TimeFormat { get => timeLabel.Format; set => timeLabel.Format = value; }

        [Category("Appearance")]
        public override Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value; 
                descriptionLabel.Font = value; 
                timeLabel.Font = value;
                ResizeUI();
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
                    ResizeUI();
                }
            }
        }

        public TimeSlider()
        {
            trackBar = new TrackBar
            {
                TickStyle = TickStyle.None,
                Anchor = AnchorStyles.Left | AnchorStyles.Right
            };

            descriptionLabel = new Label
            {
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Font = Font
            };

            timeLabel = new TimeLabel
            {
                AutoSize = false,
                Format = @"mm\:ss\.fff",
                Font = Font
            };

            Controls.Add(trackBar);
            Controls.Add(descriptionLabel);
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

            // Initialize some default properties
            Description = "Value:";
        }

        protected override void OnCreateControl()
        {
            //Size = GetPreferredSize(Size);
            ResizeUI();
        }

        private int GetTimeLabelWidth() => timeLabel.EstimateRequiredWidth(apply: true);

        private int GetDescriptionLabelWidth() =>
            showDescription ? descriptionLabel.Width : 0;

        public override Size GetPreferredSize(Size proposedSize)
        {
            int width = GetDescriptionLabelWidth() + GetTimeLabelWidth() + MIN_TRACKBAR_SIZE;
            return new Size(width, CONSTRAINT_SIZE);
        }

        private void ResizeUI()
        {
            // Ensure size is consistent
            if (Size.Width <= 0 || Size.Height <= 0) 
                Size = GetPreferredSize(Size);

            int timeWidth = GetTimeLabelWidth();
            int descWidth = GetDescriptionLabelWidth();
            int trackBarWidth = Width - (descWidth + timeWidth);
            bool timeOnRight = timeLabelAlignment == SideAlignment.Right;

            // Description
            descriptionLabel.Visible = showDescription;
            descriptionLabel.Location = new Point(0, Y_LBL_OFFSET);

            // Trackbar
            int trackBarX = timeOnRight ? descWidth : descWidth + timeWidth;
            trackBar.Size = new Size(trackBarWidth, CONSTRAINT_SIZE);
            trackBar.Location = new Point(trackBarX, 0);

            // TimeLabel
            int timeX = timeOnRight ? descWidth + trackBarWidth : descWidth;
            timeLabel.Location = new Point(timeX, Y_LBL_OFFSET);
            timeLabel.Anchor = timeOnRight ? AnchorStyles.Right : AnchorStyles.Left;
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