using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Core.Playback;
using SignalManipulator.Logic.Events;
using SignalManipulator.UI.Helpers;
using SignalManipulator.UI.Misc;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    [ExcludeFromCodeCoverage]
    public partial class AudioPlayerControl : UserControl
    {
        private IPlaybackController playback;
        private IAudioEventDispatcher audioEventDispatcher;
        
        public AudioPlayerControl()
        {
            InitializeComponent();

            if (!DesignModeHelper.IsDesignMode)
            {
                playback = AudioEngine.Instance.PlaybackController;
                audioEventDispatcher = AudioEngine.Instance.AudioEventDispatcher;
                InitializePlaybackEvents();
            }
        }

        private void InitializePlaybackEvents()
        {
            // Main events
            audioEventDispatcher.OnStarted += OnStarted;
            audioEventDispatcher.OnStopped += OnStopped;
            audioEventDispatcher.OnPlaybackStateChanged += OnPlaybackStateChanged;

            // Update event
            UIUpdateService.Instance.Register(OnUpdate);

            // Force stop event to init purpose
            OnStopped(this, EventArgs.Empty);

            // Other events
            playbackSpeedSlider.ValueChanged += (s, speed) => playback.PlaybackSpeed = speed;
            timeSlider.ValueChanged += (s, time) => playback.Seek(time);
            volumeSlider.ValueChanged += (s, volume) => playback.Volume = volume;
            pitchCheckBox.CheckedChanged += (s, e) => playback.PreservePitch = pitchCheckBox.Checked;
        }

        public void OnStarted(object? sender, EventArgs e)
        {
            timeSlider.Enabled = true;
            playbackSpeedSlider.Enabled = true;
            volumeSlider.Enabled = true;
            pitchCheckBox.Enabled = true;
        }

        public void OnStopped(object? sender, EventArgs e)
        {
            volumeSlider.Value = 1.0;
            playbackSpeedSlider.Value = 1.0;
            pitchCheckBox.CheckState = CheckState.Unchecked;

            timeSlider.Enabled = false;
            playbackSpeedSlider.Enabled = false;
            volumeSlider.Enabled = false;
            pitchCheckBox.Enabled = false;
        }

        public void OnPlaybackStateChanged(object? sender, bool playing)
        {
            playBtn.Visible = !playing; 
            pauseBtn.Visible = playing;
        }

        private void OnUpdate()
        {
            timeLbl.Time = playback.Info.CurrentTime;
            timeSlider.Value = (int)playback.Info.CurrentTime.TotalSeconds;
        }


        public void LoadAudio(string path)
        {
            playback.Load(path);

            // Update UI
            playingAudioLbl.Value = playback.Info.FileName;
            waveFmtLbl.Text = playback.Info.WaveFormatDescription;
            timeSlider.Maximum = (int)Math.Ceiling(playback.Info.TotalTime.TotalSeconds);
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
            UIUpdateService.Instance.Start();
            playback.Play();
        }

        private void pauseBtn_Click(object sender, EventArgs e) => playback.Pause();

        private void stopBtn_Click(object sender, EventArgs e)
        {
            UIUpdateService.Instance.Stop();
            playback.Stop();
        }
    }
}