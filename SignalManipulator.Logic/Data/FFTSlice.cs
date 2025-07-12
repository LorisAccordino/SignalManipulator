using SignalManipulator.Logic.AudioMath;
using SignalManipulator.Logic.AudioMath.Objects;
using SignalManipulator.Logic.Data.Channels;

namespace SignalManipulator.Logic.Data
{
    public class FFTSlice
    {
        public WaveformSlice Waveform { get; }

        public double[] Frequencies { get; }
        public IChannelDataProvider<double[]> Magnitudes { get; }

        public FFTSlice(WaveformSlice waveform, int sampleRate)
        {
            Waveform = waveform;

            // Computer Frequencies one time: through lazy Stereo
            var stereoSpectrum = FFT.CalculateMagnitudeSpectrum(waveform.DoubleSamples[AudioChannel.Stereo], sampleRate);
            Frequencies = stereoSpectrum.Frequencies;

            // Lazy registration for magnitudes of the channels
            var magnitudesCache = new ChannelCache<double[]>();
            magnitudesCache.Register(AudioChannel.Stereo, () => stereoSpectrum.Magnitudes);
            magnitudesCache.Register(AudioChannel.Mono, () => FFT.CalculateMagnitudeSpectrum(waveform.DoubleSamples[AudioChannel.Mono], sampleRate).Magnitudes);
            magnitudesCache.Register(AudioChannel.Left, () => FFT.CalculateMagnitudeSpectrum(waveform.DoubleSamples[AudioChannel.Left], sampleRate).Magnitudes);
            magnitudesCache.Register(AudioChannel.Right, () => FFT.CalculateMagnitudeSpectrum(waveform.DoubleSamples[AudioChannel.Right], sampleRate).Magnitudes);
            magnitudesCache.Register(AudioChannel.Mid, () => magnitudesCache[AudioChannel.Mono]);
            magnitudesCache.Register(AudioChannel.Side, () => FFT.CalculateMagnitudeSpectrum(waveform.DoubleSamples[AudioChannel.Side], sampleRate).Magnitudes);
            Magnitudes = magnitudesCache;
        }

        public FFTSlice(float[] stereoSamples, int sampleRate)
            : this(new WaveformSlice(stereoSamples), sampleRate) { }

        public FFTSlice(float[] left, float[] right, int sampleRate)
            : this(new WaveformSlice((left, right).ToStereo()), sampleRate) { }
    }
}