using NAudio.Wave;
using SignalManipulator.Logic.Attributes;
using SignalManipulator.Logic.Providers;

namespace SignalManipulator.Logic.Effects
{
    [ExcludeFromEffectLoader]
    [Effect("Empty")]
    public class EmptyEffect : AudioEffect
    {
        public EmptyEffect(ISampleProvider dummyProvider) : base(DefaultAudioProvider.Empty) { }
        public override int Process(float[] samples, int offset, int count)
        {
            return sourceProvider.Read(samples, offset, count);
        }
    }
}