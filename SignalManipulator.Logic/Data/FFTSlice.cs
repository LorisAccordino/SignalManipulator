using SignalManipulator.Logic.AudioMath;
using SignalManipulator.Logic.AudioMath.Objects;
using SignalManipulator.Logic.Data.Channels;

namespace SignalManipulator.Logic.Data
{
    public class FFTSlice : IChannelDataProvider
    {
        public double[] Frequencies { get; }

        // Magnitudes
        public double[] Stereo { get; }
        public double[] Left { get; }
        public double[] Right { get; }

        public FFTSlice(WaveformSlice frame, int sampleRate)
        {
            (Stereo, Frequencies) = FFT.CalculateMagnitudeSpectrum(frame.DoubleStereo, sampleRate);
            Left = FFT.CalculateMagnitudeSpectrum(frame.DoubleSplitStereo.Left, sampleRate).Magnitudes;
            Right = FFT.CalculateMagnitudeSpectrum(frame.DoubleSplitStereo.Right, sampleRate).Magnitudes;
        }

        public FFTSlice(float[] stereoSamples, int sampleRate) 
            : this(new WaveformSlice(stereoSamples), sampleRate) { }

        public FFTSlice(float[] left, float[] right, int sampleRate) 
            : this(new WaveformSlice((left, right).ToStereo()), sampleRate) { }

        public double[] Get(AudioChannel mode)
        {
            TryGet(mode, out var data);
            return data;
        }

        public bool TryGet(AudioChannel mode, out double[] data)
        {
            data = mode switch
            {
                AudioChannel.Stereo when Stereo != null => Stereo,
                AudioChannel.Left when Left != null => Left,
                AudioChannel.Right when Right != null => Right,
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
                if (Stereo != null) yield return AudioChannel.Stereo;
                if (Left != null) yield return AudioChannel.Left;
                if (Right != null) yield return AudioChannel.Right;
            }
        }

        public bool HasChannel(AudioChannel mode)
        {
            return mode switch
            {
                AudioChannel.Stereo => Stereo != null,
                AudioChannel.Left => Left != null,
                AudioChannel.Right => Right != null,
                _ => false
            };
        }
    }
}