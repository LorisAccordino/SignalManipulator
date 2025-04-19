using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using SoundTouch;
using SoundTouch.Net.NAudioSupport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SignalManipulator.UI.Modules
{
    public partial class AudioPlayer : UserControl
    {
        public static AudioPlayer Instance { get; private set; }

        // Consts
        public const int CHUNK_SIZE = 512;
        public const int SAMPLE_RATE = 44100; // 44.1 kHz
        public const int CHANNELS = 2; // Stereo Audio
        public const int TARGET_FPS = 30;
        public static readonly WaveFormat WAVE_FORMAT = new WaveFormat(SAMPLE_RATE, CHANNELS);

        // Outputs
        private Dictionary<int, WaveOutEvent> outputDevices = new Dictionary<int, WaveOutEvent>();
        //private Dictionary<int, BufferedWaveProvider> providers = new Dictionary<int, BufferedWaveProvider>();
        private int currentDevice = -1;


        // Sample providers
        private AudioFileReader audioFile;
        private BufferedWaveProvider bufferedWaveProvider = new BufferedWaveProvider(WAVE_FORMAT);
        private SoundTouchWaveProvider soundTouchWaveProvider;
        private SoundTouchProcessor soundTouchProcessor = new SoundTouchProcessor();

        // Threading
        private Thread playbackThread;

        // Playback generic
        private System.Windows.Forms.Timer playbackTimer;
        private WaveFormat waveFormat;
        private float playbackSpeed = 1.0f;


        // Playback state
        public bool IsPlaying => outputDevices[currentDevice]?.PlaybackState == PlaybackState.Playing;
        public bool IsPaused => outputDevices[currentDevice]?.PlaybackState == PlaybackState.Paused;
        public bool IsStopped => outputDevices[currentDevice]?.PlaybackState == PlaybackState.Stopped;

        public AudioPlayer()
        {
            Instance = this;

            InitializeComponent();
            InitializePlayback();
            InitOutputs();
            ChangeDevice(0);
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

        public void InitOutputs()
        {
            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
                var wo = new WaveOutEvent { DeviceNumber = i };
                wo.Init(bufferedWaveProvider);

                outputDevices[i] = wo;
                //providers[i] = bufferedWaveProvider;
            }
        }

        public void ChangeDevice(int newDevice)
        {
            if (currentDevice >= 0)
                outputDevices[currentDevice].Pause();

            outputDevices[newDevice].Play();
            currentDevice = newDevice;
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
            soundTouchWaveProvider = new SoundTouchWaveProvider(bufferedWaveProvider, soundTouchProcessor);

            // Output
            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
                outputDevices[i] = new WaveOutEvent() { DesiredLatency = 150, NumberOfBuffers = 3, DeviceNumber = i };
                outputDevices[i].Init(soundTouchWaveProvider);
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

            if (pitchCheckBox.Checked)
                soundTouchProcessor.Tempo = playbackSpeed;
            else
                soundTouchProcessor.Rate = playbackSpeed;
        }

        private void pitchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (pitchCheckBox.Checked)
            {
                soundTouchProcessor.Tempo = soundTouchProcessor.Rate;
                soundTouchProcessor.Rate = 1.0f;
            }
            else
            {
                soundTouchProcessor.Rate = soundTouchProcessor.Tempo;
                soundTouchProcessor.Tempo = 1.0f;
            }
        }

        private void ThreadRoutine()
        {
            byte[] buffer = new byte[CHUNK_SIZE];
            while (audioFile.Read(buffer, 0, buffer.Length) > 0 && !IsStopped)
            {
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
