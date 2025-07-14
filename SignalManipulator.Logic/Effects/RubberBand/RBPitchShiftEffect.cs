using NAudio.Wave;
using SignalManipulator.Logic.Attributes;

namespace SignalManipulator.Logic.Effects.RubberBand
{
    [Effect("[RubberBand] Pitch Shift")]
    public class RBPitchShiftEffect : RubberBandEffect
    {
        public double Pitch { get => rubberBandProvider.PitchRatio; set => rubberBandProvider.PitchRatio = value; }

        public RBPitchShiftEffect(ISampleProvider sourceProvider) : base(sourceProvider)
        {
            Pitch = 1.0;
        }
    }
}