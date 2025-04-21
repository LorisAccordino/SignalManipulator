using SignalManipulator.Logic.Core;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    public partial class AudioPlayerControl : UserControl
    {
        private AudioPlayer audioPlayer;

        public AudioPlayerControl()
        {
            InitializeComponent();

            if (!LicenseManager.UsageMode.Equals(LicenseUsageMode.Designtime))
            {
                audioPlayer = AudioEngine.Instance.AudioPlayer;
                InitializePlaybackEvents();
            }
        }

        private void InitializePlaybackEvents()
        {
            Disposed += (s, e) => audioPlayer.Stop();
            audioPlayer.OnUpdate += (s, e) => timeLbl.SafeInvoke(() =>
            {
                timeLbl.Text = audioPlayer.GetCurrentTime();
                timeBar.Value = audioPlayer.CurrentTime;
            }) ;
            audioPlayer.OnPlaybackStateChanged += (s, e) => { playBtn.Visible = !e; pauseBtn.Visible = e; };
        }


        public void LoadAudio(string path)
        {
            audioPlayer.LoadAudio(path);

            // Update UI
            playingAudioLbl.Text = Path.GetFileName(path);
            wvFmtLbl.Text = audioPlayer.WaveFormatDesc;
            timeBar.Maximum = audioPlayer.Duration;
        }

        private void playBtn_Click(object sender, EventArgs e) => audioPlayer.Play();

        private void pauseBtn_Click(object sender, EventArgs e) => audioPlayer.Pause();

        private void stopBtn_Click(object sender, EventArgs e) => audioPlayer.Stop();

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            audioPlayer.PlaybackSpeed = ((float)trackBar1.Value + 25) / 100; // TODO: to fix!!!
            speedLbl.Text = audioPlayer.PlaybackSpeed + "x";
        }

        private void pitchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            audioPlayer.PreservePitch = pitchCheckBox.Checked;
        }

        private void timeBar_Scroll(object sender, EventArgs e)
        {
            audioPlayer.SetTimeTo(timeBar.Value);
        }
    }
}
