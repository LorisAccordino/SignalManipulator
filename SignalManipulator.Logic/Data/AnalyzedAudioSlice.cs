namespace SignalManipulator.Logic.Data
{
    public class AnalyzedAudioSlice
    {
        public WaveformSlice Waveform { get; }
        public FFTSlice FFT { get; }
        public VolumeMetrics Volume { get; }

        public AnalyzedAudioSlice(float[] stereoSamples, int sampleRate) 
            : this(new WaveformSlice(stereoSamples), sampleRate) { }

        public AnalyzedAudioSlice(WaveformSlice waveform, int sampleRate)
            : this(waveform, new FFTSlice(waveform, sampleRate), new VolumeMetrics(waveform)) { }

        public AnalyzedAudioSlice(WaveformSlice waveform, FFTSlice fft, VolumeMetrics volume)
        {
            Waveform = waveform;
            FFT = fft;
            Volume = volume;
        }
    }
}