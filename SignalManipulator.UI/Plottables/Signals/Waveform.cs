using SignalManipulator.Logic.Data;
using SignalManipulator.Logic.Data.Channels;

namespace SignalManipulator.UI.Plottables.Signals
{
    public class Waveform : BaseSignalPlot
    {
        public AudioChannel Channel { get; set; } = AudioChannel.Stereo;

        public Waveform(int sampleRate) : this(sampleRate, "") { }
        public Waveform(int sampleRate, string channelName = "") : base(sampleRate, channelName) { }

        public void AddData(WaveformSlice waveform)
        {
            lock (lockObject)
            {
                buffer.AddRange(waveform.DoubleSamples[Channel]);
                buffer.CopyTo(data, 0);
            }
        }

        public override void UpdatePeriod(int windowSeconds)
        {
            lock (lockObject) Signal.Data.Period = windowSeconds;
        }
    }
}