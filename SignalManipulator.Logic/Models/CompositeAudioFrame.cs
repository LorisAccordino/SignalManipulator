namespace SignalManipulator.Logic.Models
{
    public class CompositeAudioFrame
    {
        public WaveformFrame Waveform { get; }
        public FFTFrame FFT { get; }
        public VolumeFrame Volume { get; }

        public CompositeAudioFrame(float[] stereoSamples, int sampleRate) : this(new WaveformFrame(stereoSamples), sampleRate) { }
        public CompositeAudioFrame(WaveformFrame waveform, int sampleRate)
        {
            Waveform = waveform;
            FFT = new FFTFrame(waveform, sampleRate);
            Volume = new VolumeFrame(waveform);
        }
        public CompositeAudioFrame(WaveformFrame waveform, FFTFrame fft, VolumeFrame volume)
        {
            Waveform = waveform;
            FFT = fft;
            Volume = volume;
        }
    }
}