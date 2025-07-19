using NAudio.Wave;

public class WaveConverter
{
    public event EventHandler<float>? ProgressChanged;
    public event EventHandler? ConversionCompleted;
    public event EventHandler<Exception>? ConversionFailed;

    public async Task<string> ConvertToWavAsync(string inputFilePath, CancellationToken cancellationToken = default)
    {
        var tempPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".wav");
        await ConvertToWavAsync(inputFilePath, tempPath, cancellationToken);
        return tempPath;
    }

    public async Task ConvertToWavAsync(string inputFilePath, string outputFilePath, CancellationToken cancellationToken = default)
    {
        try
        {
            if (!File.Exists(inputFilePath))
                throw new FileNotFoundException("Input file not found!", inputFilePath);

            using var reader = new MediaFoundationReader(inputFilePath);
            var sampleProvider = reader.ToSampleProvider();
            var waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(reader.WaveFormat.SampleRate, reader.WaveFormat.Channels);

            using var writer = new WaveFileWriter(outputFilePath, waveFormat);

            var buffer = new float[waveFormat.SampleRate * waveFormat.Channels];
            var totalDuration = reader.TotalTime.TotalSeconds;
            double writtenSeconds = 0;

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                int read = sampleProvider.Read(buffer, 0, buffer.Length);
                if (read <= 0)
                    break;

                writer.WriteSamples(buffer, 0, read);

                writtenSeconds += (double)read / waveFormat.SampleRate / waveFormat.Channels;

                float progress = (float)(writtenSeconds / totalDuration);
                ProgressChanged?.Invoke(this, Math.Clamp(progress, 0, 1));

                await Task.Yield();
            }

            writer.Flush();

            ConversionCompleted?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            ConversionFailed?.Invoke(this, ex);
        }
    }
}