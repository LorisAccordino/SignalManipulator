using NAudio.Wave;
using SignalManipulator.Logic.Providers;

namespace SignalManipulator.Logic.Effects.RubberBand
{
    public abstract class RubberBandEffect : AudioEffect
    {
        protected RubberBandProvider rubberBandProvider;

        public RubberBandEffect(ISampleProvider sourceProvider) : base(sourceProvider)
        {
            rubberBandProvider = new RubberBandProvider(sourceProvider);
        }

        public override void SetSource(ISampleProvider newSourceProvider)
        {
            base.SetSource(newSourceProvider);
            rubberBandProvider = new RubberBandProvider(newSourceProvider);
        }

        public override int Read(float[] samples, int offset, int count)
        {
            return rubberBandProvider.Read(samples, offset, count);
        }

        public override void Reset()
        {
            rubberBandProvider.ClearBuffers();
        }
    }
}