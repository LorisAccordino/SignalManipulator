using NAudio.Wave;
using SignalManipulator.Logic.Attributes;

namespace SignalManipulator.Logic.Effects.RubberBand
{
    [Effect("[RubberBand] Time Strech", "Time & Pitch", 
        "Change the duration of the signal without altering the pitch.\n" +
        "Ideal for adapting loops or vocals to a different tempo.")]
    public class RBTimeStretchEffect : RubberBandEffect
    {
        private VariSpeedEffect variSpeed;
        
        public double Speed
        {
            get => preservePitch ? 1.0 / rubberBandProvider.TimeRatio : variSpeed.Speed;
            set
            {
                rubberBandProvider.TimeRatio = preservePitch ? 1.0 / value : 1.0;
                variSpeed.Speed = !preservePitch ? value : 1.0;
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

        public RBTimeStretchEffect(ISampleProvider sourceProvider) : base(sourceProvider)
        {
            SetSource(sourceProvider);
            Speed = 1.0f;
        }

        public override void SetSource(ISampleProvider newSourceProvider)
        {
            base.SetSource(variSpeed = new VariSpeedEffect(newSourceProvider));
        }

        public override int Process(float[] samples, int offset, int count)
        {
            return rubberBandProvider.Read(samples, offset, count);
        }
    }
}