using MathNet.Numerics.Statistics;

namespace SignalManipulator.Logic.Models
{
    public class VolumeFrame
    {
        public WaveformFrame Waveform => waveform;
        private readonly WaveformFrame waveform;

        private double? cachedRMSLeft;
        private double? cachedRMSRight;
        private double? cachedRMSStereo;
        private double? cachedRMSMono;
        private double? cachedPeak;
        private double? cachedLoudness;
        private double? cachedMid;
        private double? cachedSide;

        public VolumeFrame(float[] samples)
        {
            waveform = new WaveformFrame(samples);
        }

        public VolumeFrame(WaveformFrame waveform)
        {
            this.waveform = waveform;
        }

        // === RMS ===
        public double StereoRMS => cachedRMSStereo ??= waveform.Stereo.RootMeanSquare();
        public double LeftRMS => cachedRMSLeft ??= waveform.DoubleSplitStereo.Left.RootMeanSquare();
        public double RightRMS => cachedRMSRight ??= waveform.DoubleSplitStereo.Right.RootMeanSquare();
        public double MonoRMS => cachedRMSMono ??= waveform.DoubleMono.RootMeanSquare();

        // === PEAK ===
        public double Peak => cachedPeak ??= waveform.Stereo.Max(Math.Abs);

        // === LOUDNESS ===
        public double Loudness => cachedLoudness ??= 20 * Math.Log10(StereoRMS + 1e-9); // avoid log(0)



        // === MID ===
        public double Mid
        {
            get
            {
                if (cachedMid.HasValue)
                    return cachedMid.Value;

                double[] L = waveform.DoubleSplitStereo.Left;
                double[] R = waveform.DoubleSplitStereo.Right;

                int len = Math.Min(L.Length, R.Length);
                double[] mid = new double[len];

                for (int i = 0; i < len; i++)
                    mid[i] = (L[i] + R[i]) / 2.0;

                cachedMid = mid.RootMeanSquare();
                return cachedMid.Value;
            }
        }

        // === SIDE ===
        public double Side
        {
            get
            {
                if (cachedSide.HasValue)
                    return cachedSide.Value;

                double[] L = waveform.DoubleSplitStereo.Left;
                double[] R = waveform.DoubleSplitStereo.Right;

                int len = Math.Min(L.Length, R.Length);
                double[] side = new double[len];

                for (int i = 0; i < len; i++)
                    side[i] = (L[i] - R[i]) / 2.0;

                cachedSide = side.RootMeanSquare();
                return cachedSide.Value;
            }
        }
    }
}