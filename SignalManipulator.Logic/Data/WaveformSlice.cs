using SignalManipulator.Logic.AudioMath;
using SignalManipulator.Logic.Data.Channels;

namespace SignalManipulator.Logic.Data
{
    public class WaveformSlice
    {
        private readonly float[] stereo;
        private (float[] Left, float[] Right) cachedSplitStereo;

        public float[] Stereo => stereo;

        public IChannelDataProvider<float[]> FloatSamples { get; }
        public IChannelDataProvider<double[]> DoubleSamples { get; }

        public WaveformSlice(float[] stereoSamples)
        {
            stereo = stereoSamples;

            // FLOAT CACHE
            var floatCache = new ChannelCache<float[]>();
            floatCache.Register(AudioChannel.Stereo, () => stereo);
            floatCache.Register(AudioChannel.Mono, () => stereo.ToMono());
            floatCache.Register(AudioChannel.Left, () => { EnsureSplitStereo(); return cachedSplitStereo.Left; });
            floatCache.Register(AudioChannel.Right, () => { EnsureSplitStereo(); return cachedSplitStereo.Right; });
            floatCache.Register(AudioChannel.Mid, () => floatCache[AudioChannel.Mono]);
            floatCache.Register(AudioChannel.Side, () => stereo.ToSide());
            FloatSamples = floatCache;

            // DOUBLE CACHE (based on float cache)
            var doubleCache = new ChannelCache<double[]>();
            foreach (var channel in floatCache.AvailableChannels)
                doubleCache.Register(channel, () => floatCache[channel].ToDouble());
            DoubleSamples = doubleCache;
        }

        private void EnsureSplitStereo()
        {
            if (cachedSplitStereo.Left == null || cachedSplitStereo.Right == null)
            {
                int half = stereo.Length / 2;
                cachedSplitStereo.Left = new float[half];
                cachedSplitStereo.Right = new float[half];
                stereo.SplitStereo(cachedSplitStereo.Left, cachedSplitStereo.Right);
            }
        }
    }
}