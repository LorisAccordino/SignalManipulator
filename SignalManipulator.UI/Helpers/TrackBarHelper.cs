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
            public int TickFrequency { get; set; }
            public int SmallChange { get; set; }
            public int LargeChange { get; set; }
        }

        public static TrackBarSettings GetTrackBarSettings(BaseScaleMapper mapper)
        {
            int min = mapper.ToControlUnits(mapper.RealMin);
            int max = mapper.ToControlUnits(mapper.RealMax);

            int range = max - min;
            int tickFrequency = Math.Max(1, range / 10);
            //int smallChange = Math.Max(1, range / 100);
            int smallChange = 1;
            int largeChange = Math.Max(1, range / 5);

            return new TrackBarSettings
            {
                Minimum = min,
                Maximum = max,
                TickFrequency = tickFrequency,
                SmallChange = smallChange,
                LargeChange = largeChange
            };
        }

        public static void ApplySettings(this TrackBar trackBar, TrackBarSettings settings)
        {
            trackBar.Minimum = settings.Minimum;
            trackBar.Maximum = settings.Maximum;
            trackBar.TickFrequency = settings.TickFrequency;
            trackBar.SmallChange = settings.SmallChange;
            trackBar.LargeChange = settings.LargeChange;
        }
    }
}