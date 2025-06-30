using NAudio.Wave;
using SignalManipulator.Logic.Providers;

namespace SignalManipulator.Logic.Effects
{
    public class VariSpeedEffect : AudioEffect
    {
        public override string Name => "VariSpeed";

        private ResampleSpeedProvider resampleProvider;

        public double Speed
        {
            get => resampleProvider.SpeedRatio;
            set => resampleProvider.SpeedRatio = value;
        }

        public VariSpeedEffect(ISampleProvider sourceProvider) : base(sourceProvider)
        {
            resampleProvider = new ResampleSpeedProvider(sourceProvider);
            Speed = 1.0f;
        }

        public override void SetSource(ISampleProvider newSourceProvider)
        {
            base.SetSource(newSourceProvider);
            resampleProvider = new ResampleSpeedProvider(sourceProvider);
        }

        public override int Read(float[] samples, int offset, int count)
        {
            return resampleProvider.Read(samples, offset, count);
        }
    }
}