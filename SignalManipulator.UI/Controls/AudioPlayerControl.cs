using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Core.Events;
using SignalManipulator.Logic.Core.Playback;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    public partial class AudioPlayerControl : UserControl
    {
        private PlaybackController playback;
        private AudioEventDispatcher audioEventDispatcher;

        public AudioPlayerControl()
        {
            InitializeComponent();

            if (!LicenseManager.UsageMode.Equals(LicenseUsageMode.Designtime))
            {
                //audioPlayer = AudioEngine.Instance.AudioPlayer;
                playback = AudioEngine.Instance.PlaybackController;
                audioEventDispatcher = AudioEngine.Instance.AudioEventDispatcher;
                InitializePlaybackEvents();
            }
        }

        private void InitializePlaybackEvents()
        {
            Disposed += (s, e) => playback.Stop();
            audioEventDispatcher.OnUpdate += () => timeLbl.SafeInvoke(() =>
            {
                timeLbl.Text = playback.CurrentTime.ToString(@"mm\:ss\.fff");
                timeBar.Value = (int)playback.CurrentTime.TotalSeconds;
            }) ;
            audioEventDispatcher.OnPlaybackStateChanged += (playing) => { playBtn.Visible = !playing; pauseBtn.Visible = playing; };
        }


        public void LoadAudio(string path)
        {
            playback.Load(path);

            // Update UI
            playingAudioLbl.Text = playback.FileName;
            wvFmtLbl.Text = playback.WaveFormatDesc;
            timeBar.Maximum = (int)playback.TotalTime.TotalSeconds;
        }

        private void playBtn_Click(object sender, EventArgs e) => playback.Play();

        private void pauseBtn_Click(object sender, EventArgs e) => playback.Pause();

        private void stopBtn_Click(object sender, EventArgs e) => playback.Stop();

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            playback.PlaybackSpeed = trackBar1.Value / 100f;
            speedLbl.Text = playback.PlaybackSpeed + "x";
        }

        private void pitchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            playback.PreservePitch = pitchCheckBox.Checked;
        }

        private void timeBar_Scroll(object sender, EventArgs e)
        {
            playback.Seek(TimeSpan.FromSeconds(timeBar.Value));
        }
    }
}
