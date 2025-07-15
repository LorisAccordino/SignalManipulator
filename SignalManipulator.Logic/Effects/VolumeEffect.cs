using NAudio.Wave.SampleProviders;
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
        private VolumeSampleProvider volumeAdjustedProvider;

        public VolumeEffect(ISampleProvider sourceProvider) : base(sourceProvider)
        {
            RebuildInternalPipeline();
        }

        public override void SetSource(ISampleProvider newSourceProvider)
        {
            base.SetSource(newSourceProvider);
            RebuildInternalPipeline();
        }

        private void RebuildInternalPipeline()
        {
            volumeAdjustedProvider = new VolumeSampleProvider(sourceProvider);
        }

        public override int Process(float[] samples, int offset, int count)
        {
            return volumeAdjustedProvider.Read(samples, offset, count);
        }

        public double Volume
        {
            get => volume;
            set
            {
                volume = Math.Max(0.0, value); // Prevent negative values
                volumeAdjustedProvider.Volume = (float)volume;
            }
        }

        public override void Reset()
        {
            base.Reset();
            RebuildInternalPipeline();
        }
    }
}