using NAudio.Wave;

namespace SignalManipulator.Logic.Effects
{
    public abstract class AudioEffect : IAudioEffect
    {
        public abstract string Name { get; }
        public WaveFormat WaveFormat => sourceProvider.WaveFormat;
        protected ISampleProvider sourceProvider;

        public AudioEffect(ISampleProvider sourceProvider)
        {
            this.sourceProvider = sourceProvider;
        }

        public virtual void SetSource(ISampleProvider newSourceProvider)
        {
            sourceProvider = newSourceProvider;
        }
        public abstract int Read(float[] samples, int offset, int count);
    }
}
