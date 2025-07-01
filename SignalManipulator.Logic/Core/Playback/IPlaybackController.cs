using SignalManipulator.Logic.Models;
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
        void Seek(double time);
        void Seek(TimeSpan position);

        // State properties
        bool IsPlaying { get; }
        bool IsPaused { get; }
        bool IsStopped { get; }

        // Audio info
        AudioInfo Info { get; }

        // Parameters
        double PlaybackSpeed { get; set; }
        bool PreservePitch { get; set; }
        double Volume { get; set; }
    }
}