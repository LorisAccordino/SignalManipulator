namespace SignalManipulator.UI.Controls.Plottables.Signals
{
    public class Waveform : BaseSignalPlot
    {
        public Waveform(int sampleRate) : this(sampleRate, "") { }
        public Waveform(int sampleRate, string channelName = "") : base(sampleRate, channelName) { }

        public override void AddSamples(double[] samples)
        {
            lock (lockObject)
            {
                for (int i = 0; i < samples.Length; i += (int)Signal.Data.Period)
                    buffer.Add(samples[i]);

                buffer.CopyTo(data, 0);
            }
        }

        public override void UpdatePeriod(int windowSeconds)
        {
            lock (lockObject) Signal.Data.Period = windowSeconds;
        }
    }
}