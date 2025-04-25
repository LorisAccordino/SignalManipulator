using NAudio.Wave;
using System;

namespace SignalManipulator.Logic.Core.Sourcing
{
    public class AudioFileLoader : IAudioSource
    {
        private AudioFileReader reader;
        public ISampleProvider SourceProvider => reader;
        public string FileName => reader?.FileName;
        public TimeSpan CurrentTime => reader?.CurrentTime ?? TimeSpan.Zero;
        public TimeSpan TotalTime => reader?.TotalTime ?? TimeSpan.Zero;

        public void Load(string path)
        {
            reader?.Dispose();
            reader = new AudioFileReader(path);
        }

        public void Seek(TimeSpan position)
        {
            if (reader != null) reader.CurrentTime = position;
        }

        public void Dispose() => reader?.Dispose();
    }
}