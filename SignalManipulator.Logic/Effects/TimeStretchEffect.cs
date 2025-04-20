using NAudio.Wave;
using SoundTouch;
using SoundTouch.Net.NAudioSupport;

namespace SignalManipulator.Logic.Effects
{
    public class TimeStretchEffect : AudioEffect
    {
        private SoundTouchWaveProvider soundTouchWaveProvider;
        private SoundTouchProcessor soundTouchProcessor = new SoundTouchProcessor();

        public double Speed
        {
            get => preservePitch ? soundTouchProcessor.Tempo : soundTouchProcessor.Rate;
            set
            {
                soundTouchProcessor.Rate = preservePitch ? 1.0 : value;
                soundTouchProcessor.Tempo = !preservePitch ? 1.0 : value;
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

        public TimeStretchEffect(IWaveProvider sourceProvider)
        {
            soundTouchWaveProvider = new SoundTouchWaveProvider(sourceProvider, soundTouchProcessor);
            Speed = 1.0f;
        }

        public override byte[] Process(byte[] data)
        {
            soundTouchWaveProvider.Read(data, 0, data.Length);
            return data;
        }
    }
}
