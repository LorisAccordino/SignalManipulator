using NAudio.Wave;

namespace SignalManipulator.Logic.Effects.SoundTouch
{
    public class STPitchShiftEffect : SoundTouchEffect
    {
        public override string Name => "[SoundTouch] Pitch Shift";

        public double Pitch { get => Processor.Pitch; set => Processor.Pitch = value; }

        public STPitchShiftEffect(ISampleProvider sourceProvider) : base(sourceProvider)
        {
            Pitch = 1.0;
        }
    }
}