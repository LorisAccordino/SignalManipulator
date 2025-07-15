using NAudio.Wave;

namespace SignalManipulator.Logic.Core.Export
{

    public static class AudioExporter
    {
        public static void ExportToWav(ISampleProvider provider, string outputPath, TimeSpan maxDuration)
        {
            int sampleRate = provider.WaveFormat.SampleRate;
            int channels = provider.WaveFormat.Channels;
            int seconds = (int)maxDuration.TotalSeconds;

            int totalSamples = sampleRate * channels * seconds;

            float[] buffer = new float[1024];
            int writtenSamples = 0;

            using var waveFileWriter = new WaveFileWriter(outputPath, provider.WaveFormat);

            while (writtenSamples < totalSamples)
            {
                int samplesRead = provider.Read(buffer, 0, buffer.Length);
                //if (samplesRead <= 0) break;

                waveFileWriter.WriteSamples(buffer, 0, samplesRead);
                writtenSamples += samplesRead;
            }
        }
    }
}