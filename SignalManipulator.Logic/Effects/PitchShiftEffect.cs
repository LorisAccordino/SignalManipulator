using NAudio.Wave;

namespace SignalManipulator.Logic.Effects
{
    public class PitchShiftEffect : SoundTouchEffect
    {
        public override string Name => "Pitch Shift";

        public double Pitch { get => soundTouchProcessor.Pitch; set => soundTouchProcessor.Pitch = value; }

        public PitchShiftEffect(IWaveProvider sourceProvider) : base(sourceProvider)
        {
            Pitch = 1.0;
        }
    }
}