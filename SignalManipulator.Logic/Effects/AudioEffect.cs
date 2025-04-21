using NAudio.Wave;

namespace SignalManipulator.Logic.Effects
{
    public abstract class AudioEffect : IAudioEffect
    {
        public abstract string Name { get; }
        public WaveFormat WaveFormat => sourceProvider.WaveFormat;
        private IWaveProvider sourceProvider;

        public AudioEffect(IWaveProvider sourceProvider)
        {
            this.sourceProvider = sourceProvider;
        }
        
        public virtual int Read(byte[] buffer, int offset, int count)
        {
            return sourceProvider.Read(buffer, offset, count);
        }
    }
}
