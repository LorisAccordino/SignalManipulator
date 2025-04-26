using NAudio.Wave;
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
    }
}