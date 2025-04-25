using SignalManipulator.Logic.Utils;
using System;

namespace SignalManipulator.Logic.Core
{
    public class AudioFrame
    {
        private readonly float[] floatStereo;
        private double[] cachedDoubleStereo;
        private float[] cachedMono;
        private double[] cachedDoubleMono;

        public AudioFrame(float[] stereoSamples)
        {
            floatStereo = stereoSamples;
        }

        public float[] FloatStereo => floatStereo;

        public double[] DoubleStereo
        {
            get
            {
                if (cachedDoubleStereo == null)
                    cachedDoubleStereo = floatStereo.ToDouble();
                return cachedDoubleStereo;
            }
        }

        public float[] Mono
        {
            get
            {
                if (cachedMono == null)
                    cachedMono = floatStereo.ToMono();
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
    }
}