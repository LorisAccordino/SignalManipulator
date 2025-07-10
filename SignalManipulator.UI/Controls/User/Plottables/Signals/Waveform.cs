using SignalManipulator.Logic.Models;

namespace SignalManipulator.UI.Controls.User.Plottables.Signals
{
    public class Waveform : BaseSignalPlot
    {
        public AudioChannel ChannelMode { get; set; } = AudioChannel.Stereo;

        public Waveform(int sampleRate) : this(sampleRate, "") { }
        public Waveform(int sampleRate, string channelName = "") : base(sampleRate, channelName) { }

        public void AddData(WaveformFrame waveform)
        {
            lock (lockObject)
            {
                waveform.TryGet(ChannelMode, out var samples);
                buffer.AddRange(samples);
                buffer.CopyTo(data, 0);
            }
        }

        public override void UpdatePeriod(int windowSeconds)
        {
            lock (lockObject) Signal.Data.Period = windowSeconds;
        }
    }
}