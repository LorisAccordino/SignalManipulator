using NAudio.Wave;
using System.Windows.Forms;

namespace SignalManipulator.Logic.Core.ImportExport
{
    public static class AudioImporter
    {
        public static string? TryConvertToWav(string inputPath)
        {
            try
            {
                var tempPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".wav");

                using var reader = new MediaFoundationReader(inputPath);
                WaveFileWriter.CreateWaveFile16(tempPath, reader.ToSampleProvider());

                return tempPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot open the audio file:\n{ex.Message}", "Loading error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}