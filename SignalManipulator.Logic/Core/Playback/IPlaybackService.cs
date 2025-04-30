using SignalManipulator.Logic.Models;
using System;
using System.Timers;

namespace SignalManipulator.Logic.Core.Playback
{
    public interface IPlaybackService
    {
        // Properties
        AudioInfo Info { get; }

        // Parameters
        double Speed { get; set; }
        bool PreservePitch { get; set; }

        // Events
        event EventHandler<AudioInfo> LoadCompleted;
        event EventHandler OnResume;
        event EventHandler OnPaused;
        event EventHandler OnStopped;
        event EventHandler<bool> OnPlaybackStateChanged;
        event EventHandler OnUpdate;

        // Methods
        void Load(string path);
        void Play();
        void Pause();
        void Stop();
        void Seek(TimeSpan pos);
    }
}
