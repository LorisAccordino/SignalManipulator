using NAudio.Wave;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Effects;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    public partial class AudioPlayerControl : UserControl
    {
        //public static AudioPlayer Instance { get; private set; }

        // Sample providers
        private AudioFileReader audioFile;
        //private BufferedWaveProvider bufferedWaveProvider = new BufferedWaveProvider(AudioEngine.WAVE_FORMAT);
        //private SoundTouchWaveProvider soundTouchWaveProvider;
        //private SoundTouchProcessor soundTouchProcessor = new SoundTouchProcessor();
        private TimeStretchEffect timeStrechEffect;

        // Threading
        private Thread playbackThread;

        // Playback generic
        private System.Windows.Forms.Timer playbackTimer;
        private WaveFormat waveFormat;
        private float playbackSpeed = 1.0f;

        /*
        // Playback state
        public bool IsPlaying => outputDevices[currentDevice]?.PlaybackState == PlaybackState.Playing;
        public bool IsPaused => outputDevices[currentDevice]?.PlaybackState == PlaybackState.Paused;
        public bool IsStopped => outputDevices[currentDevice]?.PlaybackState == PlaybackState.Stopped;
        */

        public AudioPlayerControl()
        {
            //Instance = this;

            InitializeComponent();
            InitializePlayback();
            //InitOutputs();
        }

        private void InitializePlayback()
        {
            Disposed += (sender, e) => StopAudio();

            playbackTimer = new System.Windows.Forms.Timer();
            playbackTimer.Interval = 50;
            playbackTimer.Tick += (s, e) =>
            {
                if (audioFile != null)
                    timeLbl.Text = audioFile.CurrentTime.ToString(@"mm\:ss\.fff");
            };
        }


        public void LoadAudio(string path)
        {
            // Stop previous if any
            StopAudio();

            // Audio file init
            audioFile = new AudioFileReader(path);
            waveFormat = audioFile.WaveFormat;

            // Processing providers
            bufferedWaveProvider = new BufferedWaveProvider(waveFormat);
            //soundTouchWaveProvider = new SoundTouchWaveProvider(bufferedWaveProvider, soundTouchProcessor);
            timeStrechEffect = new TimeStretchEffect(audioFile);

            // Output
            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
                outputDevices[i] = new WaveOutEvent() { DesiredLatency = 150, NumberOfBuffers = 3, DeviceNumber = i };
                //outputDevices[i].Init(soundTouchWaveProvider);
                outputDevices[i].Init(bufferedWaveProvider);
            }

            // Update UI
            playingAudioLbl.Text = Path.GetFileName(path);
            playPauseBtn.Enabled = true;
            stopBtn.Enabled = true;

            // Recreate the thread
            playbackThread = new Thread(ThreadRoutine);
        }

        private void playPauseBtn_Click(object sender, EventArgs e)
        {
            if (outputDevices[currentDevice] == null || audioFile == null)
                return;

            if (IsPlaying)
            {
                outputDevices[currentDevice].Pause();
                playbackTimer.Stop();
                playPauseBtn.Text = "Play";
            }
            else
            {
                bool isStopped = IsStopped;
                outputDevices[currentDevice].Play();
                playbackTimer.Start();
                playPauseBtn.Text = "Pause";

                // Start thread
                if (isStopped) playbackThread.Start();
            }
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            StopAudio();
            playbackTimer.Stop();
            timeLbl.Text = "00:00";
        }

        private void StopAudio()
        {
            if (outputDevices[currentDevice] != null)
            {
                outputDevices[currentDevice].Stop();
                outputDevices[currentDevice].Dispose();
                outputDevices[currentDevice] = null;
            }

            if (audioFile != null)
            {
                audioFile.Dispose();
                audioFile = null;
            }

            playPauseBtn.Text = "Play";
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            playbackSpeed = ((float)trackBar1.Value + 25) / 100;
            speedLbl.Text = playbackSpeed + "x";

            timeStrechEffect.Speed = playbackSpeed;
            /*
            if (pitchCheckBox.Checked)
                //soundTouchProcessor.Tempo = playbackSpeed;
            else
                //soundTouchProcessor.Rate = playbackSpeed;
            */
        }

        private void pitchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            timeStrechEffect.PreservePitch = pitchCheckBox.Checked;
            /*if (pitchCheckBox.Checked)
            {
                soundTouchProcessor.Tempo = soundTouchProcessor.Rate;
                soundTouchProcessor.Rate = 1.0f;
            }
            else
            {
                soundTouchProcessor.Rate = soundTouchProcessor.Tempo;
                soundTouchProcessor.Tempo = 1.0f;
            }*/
        }

        private void ThreadRoutine()
        {
            byte[] buffer = new byte[AudioEngine.CHUNK_SIZE];
            //while (audioFile.Read(buffer, 0, buffer.Length) > 0 && !IsStopped)
            while (!IsStopped)
            {
                // Effects
                timeStrechEffect.Process(buffer);

                // Add samples
                bufferedWaveProvider.AddSamples(buffer, 0, buffer.Length);

                // Wait for the buffer to empty enough
                while (IsBufferFull() && IsAlive()) Thread.Sleep(10);
            }
        }
        public bool IsBufferFull(int samples = 44100)
        {
            return bufferedWaveProvider.BufferedBytes > samples * playbackSpeed;
        }

        public bool IsAlive()
        {
            return outputDevices[currentDevice].PlaybackState != PlaybackState.Stopped;
        }
    }
}
