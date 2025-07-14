using SignalManipulator.Logic.Core;
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
                var samples = waveform.DoubleSamples[Channel];
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