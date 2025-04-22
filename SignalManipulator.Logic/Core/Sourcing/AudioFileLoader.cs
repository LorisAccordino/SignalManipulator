using NAudio.Wave;
using System;

namespace SignalManipulator.Logic.Core.Sourcing
{
    public class AudioFileLoader : IAudioSource
    {
        private AudioFileReader reader;
        public IWaveProvider SourceProvider => reader;
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