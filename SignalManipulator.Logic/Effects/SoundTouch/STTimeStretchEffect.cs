using NAudio.Wave;
using SignalManipulator.Logic.Core.Effects;

namespace SignalManipulator.Logic.Effects.SoundTouch
{
    [ExcludeFromEffectLoader]
    [Effect("[SoundTouch] Time Strech")]
    public class STTimeStretchEffect : SoundTouchEffect
    {
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

        public STTimeStretchEffect(ISampleProvider sourceProvider) : base(sourceProvider)
        {
            Speed = 1.0f;
        }
    }
}