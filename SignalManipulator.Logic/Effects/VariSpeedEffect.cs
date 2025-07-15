using NAudio.Wave;
using SignalManipulator.Logic.Attributes;
using SignalManipulator.Logic.Providers;

namespace SignalManipulator.Logic.Effects
{
    [Effect("VariSpeed", description: 
        "It simultaneously changes the speed and pitch of the signal, just like an analog recorder.\n" +
        "Useful for creative effects or slow motion simulations.")]
    public class VariSpeedEffect : AudioEffect
    {
        private ResampleSpeedProvider resampleProvider;
        private readonly object lockObject = new object();

        public double Speed
        {
            get => resampleProvider.SpeedRatio;
            set => resampleProvider.SpeedRatio = value;
        }

        public VariSpeedEffect(ISampleProvider sourceProvider) : base(sourceProvider)
        {
            SetSource(sourceProvider);
            Speed = 1.0f;
        }

        public override void SetSource(ISampleProvider newSourceProvider)
        {
            base.SetSource(newSourceProvider);
            lock (lockObject) resampleProvider = new ResampleSpeedProvider(sourceProvider);
        }

        public override int Process(float[] samples, int offset, int count)
        {
            lock (lockObject) return resampleProvider.Read(samples, offset, count);
        }

        public override void Reset()
        {
            base.Reset();
            lock (lockObject) resampleProvider.Reset();
        }
    }
}