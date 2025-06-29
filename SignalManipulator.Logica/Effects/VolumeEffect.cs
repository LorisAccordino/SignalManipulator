using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using System;

namespace SignalManipulator.Logic.Effects
{
    public class VolumeEffect : AudioEffect
    {
        public override string Name => "Volume";

        private double volume = 1.0; // Default: full volume
        private ISampleProvider volumeAdjustedProvider;

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

        public override int Read(float[] samples, int offset, int count)
        {
            return volumeAdjustedProvider.Read(samples, offset, count);
        }

        /// <summary>
        /// Gets or sets the volume multiplier (1.0 = 100%)
        /// </summary>
        public double Volume
        {
            get => volume;
            set
            {
                volume = Math.Max(0.0, value); // Prevent negative values
                if (volumeAdjustedProvider is VolumeSampleProvider vsp)
                    vsp.Volume = (float)volume;
            }
        }
    }
}
