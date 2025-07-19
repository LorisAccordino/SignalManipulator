using NAudio.Wave;
using SignalManipulator.Logic.AudioMath;
using SignalManipulator.Logic.Core.ImportExport;
using SignalManipulator.Logic.Info;
using System.Windows.Forms;

namespace SignalManipulator.Logic.Core.Source
{
    public class FileAudioSource
    {
        //private AudioFileReader? reader;
        private WaveFileReader? reader;
        private string path = "";

        // Audio info
        public AudioInfo Info => new AudioInfo(reader, path);

        public async Task Load(string path)
        //public void Load(string path)
        {
            if (string.IsNullOrEmpty(path)) return;

            reader?.Dispose();
            //reader = new AudioFileReader(path);

            this.path = path;
            var wavPath = await AudioImporter.TryConvertToWavAsync(path);
            if (wavPath == null)
            {
                MessageBox.Show("Error during audio loading!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Error handled
            }

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