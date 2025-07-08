using SignalManipulator.Logic.AudioMath;

namespace SignalManipulator.Logic.Models
{
    public class WaveformFrame : IChannelProvider
    {
        private readonly float[] stereo;
        private double[]? cachedDoubleStereo;
        private float[]? cachedMono;
        private double[]? cachedDoubleMono;
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



        public double[] Get(ChannelMode mode)
        {
            TryGet(mode, out var data);
            return data;
        }

        public bool TryGet(ChannelMode mode, out double[] data)
        {
            data = mode switch
            {
                ChannelMode.Stereo when DoubleStereo != null => DoubleStereo,
                ChannelMode.Left when DoubleSplitStereo.Left != null => DoubleSplitStereo.Left,
                ChannelMode.Right when DoubleSplitStereo.Right != null => DoubleSplitStereo.Right,
                ChannelMode.Mono when DoubleMono != null => DoubleMono,
                _ => []
            };

            return data.Length > 0;
        }

        public double[] GetOrThrow(ChannelMode mode)
        {
            if (!TryGet(mode, out var data))
                throw new InvalidOperationException($"Channel '{mode}' is not available.");
            return data;
        }

        public IEnumerable<ChannelMode> AvailableChannels
        {
            get
            {
                if (DoubleStereo != null) yield return ChannelMode.Stereo;
                if (DoubleSplitStereo.Left != null) yield return ChannelMode.Left;
                if (DoubleSplitStereo.Right != null) yield return ChannelMode.Right;
                if (DoubleMono != null) yield return ChannelMode.Mono;
            }
        }

        public bool HasChannel(ChannelMode mode)
        {
            return mode switch
            {
                ChannelMode.Stereo => DoubleStereo != null,
                ChannelMode.Left => DoubleSplitStereo.Left != null,
                ChannelMode.Right => DoubleSplitStereo.Right != null,
                ChannelMode.Mono => DoubleMono != null,
                _ => false
            };
        }
    }
}