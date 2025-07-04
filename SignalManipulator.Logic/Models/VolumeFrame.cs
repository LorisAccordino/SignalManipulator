using MathNet.Numerics.Statistics;

namespace SignalManipulator.Logic.Models
{
    public class VolumeFrame
    {
        private readonly WaveformFrame waveFrame;

        private double? cachedPeak;
        private double? cachedRMS;
        private double? cachedLoudness;

        public VolumeFrame(float[] stereoSamples)
        {
            waveFrame = new WaveformFrame(stereoSamples);
        }

        public double Peak
        {
            get
            {
                if (!cachedPeak.HasValue)
                    cachedPeak = waveFrame.Stereo.Max(Math.Abs);
                return cachedPeak.Value;
            }
        }

        public double RMS
        {
            get
            {
                if (!cachedRMS.HasValue)
                    cachedRMS = waveFrame.Stereo.RootMeanSquare();
                return cachedRMS.Value;
            }
        }

        public double Loudness
        {
            get
            {
                if (!cachedLoudness.HasValue)
                    cachedLoudness = 20 * Math.Log10(RMS + 1e-9f); // Avoid log(0)
                return cachedLoudness.Value;
            }
        }
    }
}