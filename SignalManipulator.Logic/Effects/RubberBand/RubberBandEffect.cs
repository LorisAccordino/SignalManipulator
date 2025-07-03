using NAudio.Wave;
using SignalManipulator.Logic.Providers;

namespace SignalManipulator.Logic.Effects.RubberBand
{
    public abstract class RubberBandEffect : AudioEffect
    {
        protected RubberBandProvider rubberBandProvider;
        private readonly object lockObject = new object();

        public RubberBandEffect(ISampleProvider sourceProvider) : base(sourceProvider)
        {
            SetSource(sourceProvider);
        }

        public override void SetSource(ISampleProvider newSourceProvider)
        {
            base.SetSource(newSourceProvider);
            lock (lockObject) rubberBandProvider = new RubberBandProvider(sourceProvider);
        }

        public override int Read(float[] samples, int offset, int count)
        {
            lock (lockObject) return rubberBandProvider.Read(samples, offset, count);
        }

        public override void Reset()
        {
            base.Reset();
            lock (lockObject) rubberBandProvider.Reset();
        }
    }
}