
using NAudio.Wave;
using SoundTouch.Net.NAudioSupport;
using SoundTouch;

namespace SignalManipulator.Logic.Effects
{
    public abstract class SoundTouchEffect : AudioEffect
    {
        private readonly SoundTouchProcessor processor;
        private SoundTouchWaveProvider soundTouchWaveProvider;
        private ISampleProvider processedProvider;

        public SoundTouchEffect(ISampleProvider sourceProvider) : base(sourceProvider)
        {
            processor = new SoundTouchProcessor();
            RebuildInternalPipeline();
        }

        public override void SetSource(ISampleProvider newSourceProvider)
        {
            base.SetSource(newSourceProvider);
            RebuildInternalPipeline();
        }

        private void RebuildInternalPipeline()
        {
            var waveProvider = sourceProvider.ToWaveProvider();
            soundTouchWaveProvider = new SoundTouchWaveProvider(waveProvider, processor);
            processedProvider = soundTouchWaveProvider.ToSampleProvider();
        }

        public override int Read(float[] samples, int offset, int count)
        {
            return processedProvider.Read(samples, offset, count);
        }

        public override void Reset()
        {
            soundTouchWaveProvider.Clear();
        }

        protected SoundTouchProcessor Processor => processor;
    }
}