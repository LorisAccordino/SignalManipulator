﻿using NAudio.Wave;

namespace SignalManipulator.Logic.Effects.SoundTouch
{
    public class STTimeStretchEffect : SoundTouchEffect
    {
        public override string Name => "[SoundTouch] Time Strech";

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