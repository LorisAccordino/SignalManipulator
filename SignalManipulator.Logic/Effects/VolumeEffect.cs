using NAudio.Wave;
using SignalManipulator.Logic.Attributes;

namespace SignalManipulator.Logic.Effects
{
    [Effect("Volume", description: 
        "Allows to adjust the volume of the input audio signal.\n" +
        "Useful for balancing the mix or normalizing the effects chain.")]
    public class VolumeEffect : AudioEffect
    {
        private double volume = 1.0; // Default: full volume
        public double Volume
        {
            get => volume;
            set => volume = Math.Max(0.0, value); // Prevent negative values
        }

        public VolumeEffect(ISampleProvider sourceProvider) : base(sourceProvider) { }

        public override int Process(float[] samples, int offset, int count)
        {
            int read = sourceProvider.Read(samples, offset, count);

            float multiplier = (float)volume;
            for (int i = 0; i < read; i++)
            {
                samples[offset + i] *= multiplier;
            }

            return read;
        }
    }
}