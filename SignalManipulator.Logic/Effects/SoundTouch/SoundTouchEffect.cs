using NAudio.Wave;
using SoundTouch.Net.NAudioSupport;
using SoundTouch;

namespace SignalManipulator.Logic.Effects.SoundTouch
{
    public abstract class SoundTouchEffect : AudioEffect
    {
        protected SoundTouchProcessor Processor => processor;
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
    }
}