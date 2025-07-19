using NAudio.Wave;

namespace SignalManipulator.Logic.Core.Routing.Import
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
            catch
            {
                return null;
            }
        }

        public static async Task<string?> TryConvertToWavAsync(string inputPath)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var tempPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".wav");
                    using var reader = new MediaFoundationReader(inputPath);
                    WaveFileWriter.CreateWaveFile16(tempPath, reader.ToSampleProvider());
                    return tempPath;
                }
                catch
                {
                    return null;
                }
            });
        }
    }
}