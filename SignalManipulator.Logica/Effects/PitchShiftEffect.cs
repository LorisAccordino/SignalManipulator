using NAudio.Wave;

namespace SignalManipulator.Logic.Effects
{
    public class PitchShiftEffect : SoundTouchEffect
    {
        public override string Name => "Pitch Shift";

        public double Pitch { get => Processor.Pitch; set => Processor.Pitch = value; }

        public PitchShiftEffect(ISampleProvider sourceProvider) : base(sourceProvider)
        {
            Pitch = 1.0;
        }
    }
}