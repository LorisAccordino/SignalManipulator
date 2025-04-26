using NAudio.Wave;
using SignalManipulator.Logic.Models;
using System;

namespace SignalManipulator.Logic.Core.Sourcing
{
    public class AudioFileLoader : IAudioSource
    {
        private AudioFileReader reader;

        // Audio info
        public AudioInfo Info => new AudioInfo
        {
            SourceProvider = reader,
            FileName = reader?.FileName,
            CurrentTime = reader?.CurrentTime ?? TimeSpan.Zero,
            TotalTime = reader?.TotalTime ?? TimeSpan.Zero,
            SampleRate = reader.WaveFormat.SampleRate,
            Channels = reader.WaveFormat.Channels,
            BitsPerSample = reader.WaveFormat.BitsPerSample,
            WaveFormatDescription = reader.WaveFormat.ToString()
        };

        public event Action<AudioInfo> LoadCompleted;

        public void Load(string path)
        {
            reader?.Dispose();
            reader = new AudioFileReader(path);
            LoadCompleted?.Invoke(Info);
        }

        public void Seek(TimeSpan position)
        {
            if (reader != null) reader.CurrentTime = position;
        }

        public void Dispose() => reader?.Dispose();
    }
}