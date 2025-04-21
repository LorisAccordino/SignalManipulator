using NAudio.Wave;

namespace SignalManipulator.Logic.Effects
{
    public abstract class AudioEffect : IAudioEffect
    {
        public abstract string Name { get; }
        public WaveFormat WaveFormat => sourceProvider.WaveFormat;
        protected IWaveProvider sourceProvider;

        public AudioEffect(IWaveProvider sourceProvider)
        {
            this.sourceProvider = sourceProvider;
        }

        public abstract int Read(byte[] buffer, int offset, int count);
    }
}
