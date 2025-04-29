using NAudio.Wave;
using SignalManipulator.Logic.Models;
using System;

namespace SignalManipulator.Logic.Core.Sourcing
{
    public class AudioFileLoader : IAudioSource
    {
        private AudioFileReader reader;

        // Audio info
        public AudioInfo Info
        {
            get
            {
                if (reader == null) return AudioInfo.Default;

                WaveFormat waveFormat = reader.WaveFormat;
                return new AudioInfo
                {
                    SourceProvider = reader,
                    FileName = reader?.FileName,
                    CurrentTime = reader?.CurrentTime ?? TimeSpan.Zero,
                    TotalTime = reader?.TotalTime ?? TimeSpan.Zero,
                    SampleRate = waveFormat.SampleRate,
                    Channels = waveFormat.Channels,
                    BitsPerSample = waveFormat.BitsPerSample,
                    WaveFormatDescription = waveFormat.ToString()
                };
            }
        }

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