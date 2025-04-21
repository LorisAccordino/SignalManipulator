
using NAudio.Wave;
using SoundTouch.Net.NAudioSupport;
using SoundTouch;

namespace SignalManipulator.Logic.Effects
{
    public abstract class SoundTouchEffect : AudioEffect
    {
        protected SoundTouchWaveProvider soundTouchWaveProvider;
        protected SoundTouchProcessor soundTouchProcessor;

        public SoundTouchEffect(IWaveProvider sourceProvider) : base(sourceProvider)
        {
            soundTouchProcessor = new SoundTouchProcessor();
            soundTouchWaveProvider = new SoundTouchWaveProvider(sourceProvider, soundTouchProcessor);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return soundTouchWaveProvider.Read(buffer, offset, count);
        }
    }
}