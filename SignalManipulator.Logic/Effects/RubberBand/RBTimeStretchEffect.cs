﻿using NAudio.Wave;

namespace SignalManipulator.Logic.Effects.RubberBand
{
    public class RBTimeStretchEffect : RubberBandEffect
    {
        public override string Name => "[RubberBand] Time Strech";

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

        public override int Read(float[] samples, int offset, int count)
        {
            return rubberBandProvider.Read(samples, offset, count);
        }
    }
}