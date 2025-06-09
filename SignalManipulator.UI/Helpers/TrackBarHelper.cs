using SignalManipulator.UI.Scaling;
using System;
using System.Windows.Forms;

namespace SignalManipulator.UI.Helpers
{
    public static class TrackBarHelper
    {
        public class TrackBarSettings
        {
            public int Minimum { get; set; }
            public int Maximum { get; set; }
        }

        public static TrackBarSettings GetTrackBarSettings(BaseScaleMapper mapper)
        {
            int min = mapper.ToControlUnits(mapper.RealMin);
            int max = mapper.ToControlUnits(mapper.RealMax);

            return new TrackBarSettings { Minimum = min, Maximum = max};
        }

        public static void ApplySettings(this TrackBar trackBar, TrackBarSettings settings)
        {
            trackBar.Minimum = settings.Minimum;
            trackBar.Maximum = settings.Maximum;
        }
    }
}