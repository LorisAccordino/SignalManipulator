using SignalManipulator.Logic.AudioMath;

namespace SignalManipulator.Logic.Models
{
    public class WaveformFrame
    {
        private readonly float[] stereo;
        private double[] cachedDoubleStereo;
        private float[] cachedMono;
        private double[] cachedDoubleMono;
        private (float[] Left, float[] Right) cachedSplitStereo;
        private (double[] Left, double[] Right) cachedDoubleSplitStereo;

        public WaveformFrame(float[] stereoSamples)
        {
            stereo = stereoSamples;
        }

        public float[] Stereo => stereo;

        public double[] DoubleStereo
        {
            get
            {
                if (cachedDoubleStereo == null)
                    cachedDoubleStereo = stereo.ToDouble();
                return cachedDoubleStereo;
            }
        }

        public float[] Mono
        {
            get
            {
                if (cachedMono == null)
                    cachedMono = stereo.ToMono();
                return cachedMono;
            }
        }

        public double[] DoubleMono
        {
            get
            {
                if (cachedDoubleMono == null)
                    cachedDoubleMono = Mono.ToDouble();
                return cachedDoubleMono;
            }
        }

        public (float[] Left, float[] Right) SplitStereo
        {
            get
            {
                if (cachedSplitStereo.Left == null || cachedSplitStereo.Right == null)
                {
                    // Array allocation
                    int half = Stereo.Length / 2;
                    cachedSplitStereo.Left = new float[half]; cachedSplitStereo.Right = new float[half];

                    // Split operation
                    Stereo.SplitStereo(cachedSplitStereo.Left, cachedSplitStereo.Right);
                }
                return cachedSplitStereo;
            }
        }

        public (double[] Left, double[] Right) DoubleSplitStereo
        {
            get
            {
                if (cachedDoubleSplitStereo.Left == null || cachedDoubleSplitStereo.Right == null)
                {
                    cachedDoubleSplitStereo.Left = SplitStereo.Left.ToDouble();
                    cachedDoubleSplitStereo.Right = SplitStereo.Right.ToDouble();
                }
                return cachedDoubleSplitStereo;
            }
        }
    }
}