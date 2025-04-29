using NAudio.Wave;
using SignalManipulator.Logic.Providers;
using System;

namespace SignalManipulator.Logic.Models
{
    public struct AudioInfo
    {
        public ISampleProvider SourceProvider { get; set; }
        public string FileName { get; set; }
        public TimeSpan CurrentTime { get; set; }
        public TimeSpan TotalTime { get; set; }
        public int SampleRate { get; set; }
        public int Channels { get; set; }
        public int BitsPerSample { get; set; }
        public string WaveFormatDescription { get; set; }

        public static AudioInfo Default
        {
            get
            {
                ISampleProvider source = new DefaultSampleProvider();
                WaveFormat waveFormat = source.WaveFormat;
                return new AudioInfo
                {
                    SourceProvider = source,
                    FileName = string.Empty,
                    CurrentTime = TimeSpan.Zero,
                    TotalTime = TimeSpan.Zero,
                    SampleRate = waveFormat.SampleRate,
                    Channels = waveFormat.Channels,
                    BitsPerSample = waveFormat.BitsPerSample,
                    WaveFormatDescription = "No audio loaded"
                };
            }
        }
    }
}