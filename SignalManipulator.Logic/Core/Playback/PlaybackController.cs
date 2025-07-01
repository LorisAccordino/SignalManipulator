using NAudio.Wave;
using SignalManipulator.Logic.Core.Routing;
using SignalManipulator.Logic.Models;
using System;

namespace SignalManipulator.Logic.Core.Playback
{
    public class PlaybackController : IPlaybackController
    {
        private readonly IPlaybackService playback;
        private readonly IAudioRouter router;

        public PlaybackController(IPlaybackService playback, IAudioRouter router)
        {
            this.playback = playback;
            this.router = router;
        }

        // --- Commands ---
        public void Load(string path) => playback.Load(path);
        public void Play() { if (!IsPlaying) playback.Play(); }
        public void Pause() { if (IsPlaying) playback.Pause(); }
        public void Stop() { if (IsPlaying) playback.Stop(); }
        public void Seek(double time) => Seek(TimeSpan.FromSeconds(time));
        public void Seek(TimeSpan pos) => playback.Seek(pos);

        // --- State ---
        public bool IsPlaying => router.CurrentDevice.PlaybackState == PlaybackState.Playing;
        public bool IsPaused => router.CurrentDevice.PlaybackState == PlaybackState.Paused;
        public bool IsStopped => router.CurrentDevice.PlaybackState == PlaybackState.Stopped;

        // --- Audio info ---
        public AudioInfo Info => playback.Info;

        // --- Parameters ---
        public double PlaybackSpeed { get => playback.Speed; set => playback.Speed = value; }
        public bool PreservePitch { get => playback.PreservePitch; set => playback.PreservePitch = value; }
        public double Volume { get => playback.Volume; set => playback.Volume = value; }
    }
}