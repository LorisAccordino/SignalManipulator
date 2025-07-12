using MathNet.Numerics.Statistics;
using SignalManipulator.Logic.Data.Channels;

namespace SignalManipulator.Logic.Data
{
    public class VolumeMetrics
    {
        public WaveformSlice Waveform { get; }

        // === RMS ===
        public IChannelDataProvider<double> RMS { get; }

        // === LOUDNESS ===
        private double? cachedLoudness;
        public double Loudness => cachedLoudness ??= 20 * Math.Log10(RMS[AudioChannel.Stereo] + 1e-9);

        // === PEAK ===
        private double? cachedPeak;
        public double Peak => cachedPeak ??= Waveform.FloatSamples[AudioChannel.Stereo].Max(Math.Abs);

        public VolumeMetrics(float[] samples) : this(new WaveformSlice(samples)) { }
        public VolumeMetrics(WaveformSlice waveform)
        {
            Waveform = waveform;

            var volumeCache = new ChannelCache<double>();
            volumeCache.Register(AudioChannel.Stereo, () => Waveform.FloatSamples[AudioChannel.Stereo].RootMeanSquare());
            volumeCache.Register(AudioChannel.Left, () => Waveform.FloatSamples[AudioChannel.Left].RootMeanSquare());
            volumeCache.Register(AudioChannel.Right, () => Waveform.FloatSamples[AudioChannel.Right].RootMeanSquare());
            volumeCache.Register(AudioChannel.Mono, () => Waveform.FloatSamples[AudioChannel.Mono].RootMeanSquare());
            volumeCache.Register(AudioChannel.Mid, () => volumeCache[AudioChannel.Mono]);
            volumeCache.Register(AudioChannel.Side, () => Waveform.FloatSamples[AudioChannel.Side].RootMeanSquare());
            RMS = volumeCache;
        }
    }
}