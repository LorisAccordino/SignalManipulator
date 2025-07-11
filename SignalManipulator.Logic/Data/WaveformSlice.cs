using SignalManipulator.Logic.AudioMath;
using SignalManipulator.Logic.Data.Channels;

namespace SignalManipulator.Logic.Data
{
    public class WaveformSlice : IChannelDataProvider
    {
        private readonly float[] stereo;
        private double[]? cachedDoubleStereo;
        private float[]? cachedMono;
        private double[]? cachedDoubleMono;
        private (float[] Left, float[] Right) cachedSplitStereo;
        private (double[] Left, double[] Right) cachedDoubleSplitStereo;

        public WaveformSlice(float[] stereoSamples)
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



        public double[] Get(AudioChannel mode)
        {
            TryGet(mode, out var data);
            return data;
        }

        public bool TryGet(AudioChannel mode, out double[] data)
        {
            data = mode switch
            {
                AudioChannel.Stereo when DoubleStereo != null => DoubleStereo,
                AudioChannel.Left when DoubleSplitStereo.Left != null => DoubleSplitStereo.Left,
                AudioChannel.Right when DoubleSplitStereo.Right != null => DoubleSplitStereo.Right,
                AudioChannel.Mono when DoubleMono != null => DoubleMono,
                _ => []
            };

            return data.Length > 0;
        }

        public double[] GetOrThrow(AudioChannel mode)
        {
            if (!TryGet(mode, out var data))
                throw new InvalidOperationException($"Channel '{mode}' is not available.");
            return data;
        }

        public IEnumerable<AudioChannel> AvailableChannels
        {
            get
            {
                if (DoubleStereo != null) yield return AudioChannel.Stereo;
                if (DoubleSplitStereo.Left != null) yield return AudioChannel.Left;
                if (DoubleSplitStereo.Right != null) yield return AudioChannel.Right;
                if (DoubleMono != null) yield return AudioChannel.Mono;
            }
        }

        public bool HasChannel(AudioChannel mode)
        {
            return mode switch
            {
                AudioChannel.Stereo => DoubleStereo != null,
                AudioChannel.Left => DoubleSplitStereo.Left != null,
                AudioChannel.Right => DoubleSplitStereo.Right != null,
                AudioChannel.Mono => DoubleMono != null,
                _ => false
            };
        }
    }
}