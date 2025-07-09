using SignalManipulator.Logic.AudioMath.Models;

namespace SignalManipulator.Logic.Models
{
    public class FFTFrame : IChannelProvider
    {
        public double[] Frequencies { get; }

        // Magnitudes
        public double[]? Stereo { get; }
        public double[]? Left { get; }
        public double[]? Right { get; }

        public FFTFrame(WaveformFrame frame, int sampleRate)
        {
            (Stereo, Frequencies) = FFT.CalculateMagnitudeSpectrum(frame.DoubleStereo, sampleRate);
            Left = FFT.CalculateMagnitudeSpectrum(frame.DoubleSplitStereo.Left, sampleRate).Magnitudes;
            Right = FFT.CalculateMagnitudeSpectrum(frame.DoubleSplitStereo.Right, sampleRate).Magnitudes;
        }

        private FFTFrame(double[] frequencies, double[]? stereo = null, double[]? left = null, double[]? right = null)
        {
            Frequencies = frequencies;
            Stereo = stereo;
            Left = left;
            Right = right;
        }

        public static FFTFrame FromStereo(double[] stereoSamples, int sampleRate)
        {
            var (magnitudes, freqs) = FFT.CalculateMagnitudeSpectrum(stereoSamples, sampleRate);
            return new FFTFrame(freqs, stereo: magnitudes);
        }

        public static FFTFrame FromSplit(double[] left, double[] right, int sampleRate)
        {
            var (magL, freqsL) = FFT.CalculateMagnitudeSpectrum(left, sampleRate);
            var magR = FFT.CalculateMagnitudeSpectrum(right, sampleRate).Magnitudes;
            return new FFTFrame(freqsL, left: magL, right: magR);
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