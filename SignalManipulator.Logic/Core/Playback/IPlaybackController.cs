using NAudio.Wave;
using System;

namespace SignalManipulator.Logic.Core.Playback
{
    public interface IPlaybackController
    {
        // Commands
        void Load(string path);
        void Play();
        void Pause();
        void Stop();
        void Seek(TimeSpan position);

        // State properties
        bool IsPlaying { get; }
        bool IsPaused { get; }
        bool IsStopped { get; }

        // Audio info
        string FileName { get; }
        TimeSpan CurrentTime { get; }
        TimeSpan TotalTime { get; }
        WaveFormat WaveFormat { get; }
        string WaveFormatDesc { get; }
        int SampleRate { get; }

        // Parameters
        double PlaybackSpeed { get; set; }
        bool PreservePitch { get; set; }
    }
}