using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Core.Playback;
using SignalManipulator.Logic.Events;
using SignalManipulator.UI.Helpers;
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
            Disposed += (s, e) => playback.Stop();
            UIUpdateService.Instance.Register(OnUpdate);

            audioEventDispatcher.OnPlaybackStateChanged += (s, playing) => { playBtn.Visible = !playing; pauseBtn.Visible = playing; };

            playbackSpeedSlider.ValueChanged += (s, value) => playback.PlaybackSpeed = value;
            timeSlider.ValueChanged += (s, value) =>
            {
                playback.Seek(TimeSpan.FromSeconds(timeSlider.Value));
            };
        }

        private void OnUpdate()
        {
            timeLbl.Text = playback.Info.CurrentTime.ToString(@"mm\:ss\.fff");

            // Update without firing event!
            timeSlider.Value = (int)playback.Info.CurrentTime.TotalSeconds;
        }


        public void LoadAudio(string path)
        {
            playback.Load(path);

            // Update UI
            playingAudioLbl.Text = playback.Info.FileName;
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
            playback.Stop();
            UIUpdateService.Instance.Stop();
        }


        private void pitchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            playback.PreservePitch = pitchCheckBox.Checked;
        }
    }
}