using NAudio.Wave;
using SignalManipulator.Logic.AudioMath;
using SignalManipulator.Logic.Core.ImportExport;
using SignalManipulator.Logic.Info;

namespace SignalManipulator.Logic.Core.Source
{
    public class FileAudioSource
    {
        //private AudioFileReader? reader;
        private WaveFileReader? reader;
        private string path = "";

        // Audio info
        public AudioInfo Info => new AudioInfo(reader, path);

        public void Load(string path)
        {
            reader?.Dispose();
            //reader = new AudioFileReader(path);

            this.path = path;
            var wavPath = AudioImporter.TryConvertToWav(path);
            if (wavPath == null)
                return; // Errore già gestito

            reader = new WaveFileReader(wavPath);
        }

        public void Seek(TimeSpan position)
        {
            if (reader != null && reader.CanSeek)
            {
                var safePosition = position.Clamp(TimeSpan.Zero, reader.TotalTime);
                reader.CurrentTime = safePosition;
            }
        }

        public void Dispose() => reader?.Dispose();
    }
}