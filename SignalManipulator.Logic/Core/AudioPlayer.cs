using NAudio.Wave;
using SignalManipulator.Logic.Effects;
using System.IO.Ports;
using System.IO;
using System.Threading;

namespace SignalManipulator.Logic.Core
{
    public class AudioPlayer
    {
        // Properties
        public double PlaybackSpeed { get; set; } = 1.0;
        public bool IsPlaying => audioRouter.CurrentDevice.PlaybackState == PlaybackState.Playing;
        public bool IsPaused => audioRouter.CurrentDevice.PlaybackState == PlaybackState.Paused;
        public bool IsStopped => audioRouter.CurrentDevice.PlaybackState == PlaybackState.Stopped;

        public string GetCurrentTime(string format = @"mm\:ss\.fff") => audioFile.CurrentTime.ToString(format);



        // Audio providers & logic components
        public BufferedWaveProvider BufferedWaveProvider { get; private set; } = new BufferedWaveProvider(AudioEngine.WAVE_FORMAT);
        private AudioFileReader audioFile;
        private Thread playbackThread;


        // Audio modules references
        private AudioEngine audioEngine;
        private AudioRouter audioRouter => audioEngine.AudioRouter;

        public AudioPlayer(AudioEngine audioEngine)
        {
            this.audioEngine = audioEngine;
        }

        public void LoadAudio(string path)
        {
            LoadAudio(new AudioFileReader(path));
        }

        public void LoadAudio(AudioFileReader audioFileReader)
        {
            // Stop previous if any
            Stop();

            // Audio file init
            audioFile = audioFileReader;

            // Processing providers
            BufferedWaveProvider = new BufferedWaveProvider(audioFile.WaveFormat);
            //soundTouchWaveProvider = new SoundTouchWaveProvider(bufferedWaveProvider, soundTouchProcessor);
            //timeStrechEffect = new TimeStretchEffect(audioFile);

            // Init outputs
            audioRouter.InitOutputs(BufferedWaveProvider);

            /*
            // Output
            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
                outputDevices[i] = new WaveOutEvent() { DesiredLatency = 150, NumberOfBuffers = 3, DeviceNumber = i };
                //outputDevices[i].Init(soundTouchWaveProvider);
                outputDevices[i].Init(bufferedWaveProvider);
            }
            */

            // Update UI
            playingAudioLbl.Text = Path.GetFileName(path);
            playPauseBtn.Enabled = true;
            stopBtn.Enabled = true;

            // Recreate the thread
            playbackThread = new Thread(PlaybackThreadRoutine);
        }

        private void PlaybackThreadRoutine()
        {
            byte[] buffer = new byte[AudioEngine.CHUNK_SIZE];
            //while (audioFile.Read(buffer, 0, buffer.Length) > 0 && !IsStopped)
            while (!IsStopped)
            {
                // Effects
                //timeStrechEffect.Process(buffer);

                // Add samples
                BufferedWaveProvider.AddSamples(buffer, 0, buffer.Length);

                // Wait for the buffer to empty enough
                while (IsBufferFull() && !IsStopped) Thread.Sleep(10);
            }
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


        // State methods
        public bool IsBufferFull(int samples = 44100)
        {
            return BufferedWaveProvider.BufferedBytes > samples * PlaybackSpeed;
        }
    }
}
