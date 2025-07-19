using NAudio.Wave;
using SignalManipulator.Logic.AudioMath;
using SignalManipulator.Logic.Info;

namespace SignalManipulator.Logic.Core.Routing.Inputs
{
    public class ExtendedWaveFileReader : WaveFileReader, ISampleProvider
    {
        public string FilePath { get; }
        public AudioInfo Info { get; }
        public TimeSpan Duration => TotalTime;

        public ExtendedWaveFileReader(string filePath)
            : base(File.OpenRead(filePath))
        {
            FilePath = filePath;
            Info = new AudioInfo(this, filePath);
        }

        public int Read(float[] samples, int offset, int count)
        {
            byte[] buffer = new byte[count * 4];
            int read = Read(buffer, 0, count * 4);
            buffer.CopyToFloats(samples, offset, read / 4);
            return read / 4;
        }

        public void Seek(TimeSpan position)
        {
            if (CanSeek) CurrentTime = position.Clamp(TimeSpan.Zero, TotalTime);
        }
    }
}