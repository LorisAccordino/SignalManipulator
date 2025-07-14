using NAudio.Wave;
using SignalManipulator.Logic.Core.Effects;

namespace SignalManipulator.Logic.Effects.SoundTouch
{
    [ExcludeFromEffectLoader]
    [Effect("[SoundTouch] Pitch Shift")]
    public class STPitchShiftEffect : SoundTouchEffect
    {
        public double Pitch { get => Processor.Pitch; set => Processor.Pitch = value; }

        public STPitchShiftEffect(ISampleProvider sourceProvider) : base(sourceProvider)
        {
            Pitch = 1.0;
        }
    }
}