using NAudio.Wave;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Providers;

namespace SignalManipulator.Logic.Info
{
    public class AudioTechnicalInfo
    {
        private readonly string DEFAULT_MESSAGE = AudioInfo.DEFAULT_MESSAGE;
        private readonly WaveStream? waveStream;

        public static AudioTechnicalInfo Default => new AudioTechnicalInfo(null);

        public AudioTechnicalInfo(WaveStream? waveStream)
        {
            this.waveStream = waveStream;
            if (waveStream != null) 
                SourceProvider = waveStream.ToSampleProvider();
        }

        public TimeSpan CurrentTime => waveStream?.CurrentTime ?? TimeSpan.Zero;
        public TimeSpan TotalTime => waveStream?.TotalTime ?? TimeSpan.Zero;
        public long Length => waveStream?.Length ?? 0;

        public ISampleProvider SourceProvider = new DefaultAudioProvider();
        public WaveStream? WaveStream => waveStream;
        public WaveFormat WaveFormat => waveStream?.WaveFormat ?? AudioEngine.WAVE_FORMAT;
        public int SampleRate => WaveFormat.SampleRate;
        public int Channels => WaveFormat.Channels;
        public int BitsPerSample => WaveFormat.BitsPerSample;
        public int BytesPerSample => BitsPerSample / 8;
        public string WaveFormatDescription => WaveFormat?.ToString() ?? DEFAULT_MESSAGE;
        public WaveFormatEncoding Encoding => WaveFormat.Encoding;

        public int ByteRate => WaveFormat.AverageBytesPerSecond;
        public int BitRate => ByteRate * 8;
        public int KyloBitRate => BitRate / 1000;
        public int BlockAlign => WaveFormat.BlockAlign;
        public int TotalSamples => (int)(Length / BytesPerSample);
        public int TotalFrames => (int)(Length / BlockAlign);
    }
}