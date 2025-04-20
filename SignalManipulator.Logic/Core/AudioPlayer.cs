using NAudio.Wave;

namespace SignalManipulator.Logic.Core
{
    public class AudioPlayer
    {
        // Properties
        public double PlaybackSpeed { get; set; } = 1.0;

        public bool IsPlaying => audioRouter.CurrentDevice.PlaybackState == PlaybackState.Playing;
        public bool IsPaused => audioRouter.CurrentDevice.PlaybackState == PlaybackState.Paused;
        public bool IsStopped => audioRouter.CurrentDevice.PlaybackState == PlaybackState.Stopped;


        // Audio modules references
        private AudioEngine audioEngine;
        private AudioRouter audioRouter => audioEngine.AudioRouter;

        public AudioPlayer(AudioEngine audioEngine)
        {
            this.audioEngine = audioEngine;
        }

        public void Play()
        {

        }

        public void Pause()
        {

        }

        public void Stop()
        {

        }
    }
}
