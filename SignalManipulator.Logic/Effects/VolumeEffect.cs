using NAudio.Wave.SampleProviders;
using NAudio.Wave;

namespace SignalManipulator.Logic.Effects
{
    public class VolumeEffect : AudioEffect
    {
        public override string Name => "Volume";

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