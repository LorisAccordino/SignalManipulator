using NAudio.Wave;
using System.Diagnostics;

namespace SignalManipulator.Logic.Effects.RubberBand
{
    public class RBTimeStretchEffect : RubberBandEffect
    {
        public override string Name => "[RubberBand] Time Strech";

        /*
        public double Speed
        {
            get => preservePitch ? Processor.Tempo : Processor.Rate;
            set
            {
                Processor.Rate = preservePitch ? 1.0 : value;
                Processor.Tempo = !preservePitch ? 1.0 : value;
            }
        }

        private bool preservePitch = false;
        public bool PreservePitch
        {
            get => preservePitch;
            set
            {
                if (preservePitch != value)
                {
                    double currentSpeed = Speed; // Get the current Speed value
                    preservePitch = value;       // Set preservePitch value
                    Speed = currentSpeed;        // Reset the Speed value with the new logic
                }
            }
        }
        */

        public double Speed
        {
            get => 1.0 / rubberBandProvider.TimeRatio;
            set => rubberBandProvider.TimeRatio = 1.0 / value;
        }

        public RBTimeStretchEffect(ISampleProvider sourceProvider) : base(sourceProvider)
        {
            Speed = 1.0f;
        }
    }
}