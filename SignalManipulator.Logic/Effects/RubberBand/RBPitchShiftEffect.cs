using NAudio.Wave;

namespace SignalManipulator.Logic.Effects.RubberBand
{
    public class RBPitchShiftEffect : RubberBandEffect
    {
        public override string Name => "[RubberBand] Pitch Shift";
        public double Pitch { get => rubberBandProvider.PitchRatio; set => rubberBandProvider.PitchRatio = value; }

        public RBPitchShiftEffect(ISampleProvider sourceProvider) : base(sourceProvider)
        {
            Pitch = 1.0;
        }
    }
}