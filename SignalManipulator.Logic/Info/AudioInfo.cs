using NAudio.Wave;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.Logic.Info
{
    [ExcludeFromCodeCoverage]
    public class AudioInfo
    {
        public static readonly string DEFAULT_MESSAGE = "No info available";

        public AudioTechnicalInfo Technical { get; }
        public AudioMetadataInfo Metadata { get; }

        public static AudioInfo Default => new AudioInfo();

        private AudioInfo()
        {
            Technical = AudioTechnicalInfo.Default;
            Metadata = AudioMetadataInfo.Default;
        }

        public AudioInfo(WaveStream waveStream, string filePath)
        {
            Technical = new AudioTechnicalInfo(waveStream);
            Metadata = new AudioMetadataInfo(filePath);
        }

        // Utility
        public string FilePath => Metadata.FilePath;
        public TimeSpan CurrentTime => Technical.CurrentTime;
        public TimeSpan TotalTime => Technical.TotalTime;
    }
}