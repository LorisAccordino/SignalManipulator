using SignalManipulator.Logic.AudioMath;

namespace SignalManipulator.UI.Controls.Plottables.Scatters
{
    public class LissajousPlot : BaseScatterPlot
    {
        public LissajousPlot(int sampleRate, int scatterSamples, string label = "") : base(sampleRate, scatterSamples, label)
        {
            MarkerSize = 0;
            LineWidth = 1;
            LegendText = label;
        }

        public override void AddSamples(double[] stereoSamples)
        {
            lock (lockObject)
            {
                for (int i = 0; i < stereoSamples.Length; i++)
                    buffer.Add(stereoSamples[i]);

                if (buffer.Count < left.Length * 2)
                    return;

                buffer.ToArray().SplitStereo(left, right);
            }
        }
    }
}