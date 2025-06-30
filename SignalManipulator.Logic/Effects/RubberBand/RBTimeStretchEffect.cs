using NAudio.Wave;

namespace SignalManipulator.Logic.Effects.RubberBand
{
    public class RBTimeStretchEffect : RubberBandEffect
    {
        public override string Name => "[RubberBand] Time Strech";

        private VariSpeedEffect variSpeed;
        
        public double Speed
        {
            get => preservePitch ? rubberBandProvider.TimeRatio : variSpeed.Speed;
            set
            {
                rubberBandProvider.TimeRatio = 1.0 / value;
                variSpeed.Speed = value;
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
            variSpeed = new VariSpeedEffect(sourceProvider);
            Speed = 1.0f;
        }

        public override void SetSource(ISampleProvider newSourceProvider)
        {
            base.SetSource(newSourceProvider);
            variSpeed = new VariSpeedEffect(sourceProvider);
        }

        public override int Read(float[] samples, int offset, int count)
        {
            return preservePitch ? 
                rubberBandProvider.Read(samples, offset, count) : 
                variSpeed.Read(samples, offset, count);
        }
    }
}