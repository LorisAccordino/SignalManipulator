using NAudio.Wave;

namespace SignalManipulator.Logic.Effects
{
    public abstract class AudioEffect : IAudioEffect
    {
        protected ISampleProvider sourceProvider;

        public abstract string Name { get; }
        public WaveFormat WaveFormat => sourceProvider.WaveFormat;
        public bool Bypass { get; set; } = false;

        public AudioEffect(ISampleProvider sourceProvider)
        {
            this.sourceProvider = sourceProvider;
        }

        public virtual void SetSource(ISampleProvider newSourceProvider)
        {
            sourceProvider = newSourceProvider;
        }
        public abstract int Read(float[] samples, int offset, int count);

        public virtual void Reset()
        {
            if (sourceProvider is AudioEffect audioEffect) 
                audioEffect.Reset();
        }
    }
}