using NAudio.Wave;
using SignalManipulator.Logic.Info;

namespace SignalManipulator.Logic.Core.Source
{
    public class FileAudioSource
    {
        private AudioFileReader? reader;

        // Audio info
        public AudioInfo Info => new AudioInfo(reader);

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