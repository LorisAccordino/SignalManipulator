namespace SignalManipulator.UI.Controls.Plottables
{
    public class WaveformPlot : BaseSignalPlot
    {
        public WaveformPlot(int sampleRate) : this(sampleRate, "") { }
        public WaveformPlot(int sampleRate, string channelName = "") : base(sampleRate, channelName) { }

        public override void AddSamples(double[] samples)
        {
            lock (lockObject)
            {
                for (int i = 0; i < samples.Length; i += (int)Data.Period)
                    buffer.Add(samples[i]);

                buffer.CopyTo(data, 0);
            }
        }

        public override void UpdatePeriod(int windowSeconds)
        {
            lock (lockObject) Data.Period = windowSeconds;
        }
    }
}