using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Core.Playback;
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
            audioPlayer.OnUpdate += () => timeLbl.SafeInvoke(() =>
            {
                timeLbl.Text = audioPlayer.CurrentTime.ToString(@"mm\:ss\.fff");
                timeBar.Value = (int)audioPlayer.CurrentTime.TotalSeconds;
            }) ;
            audioPlayer.OnPlaybackStateChanged += (s, e) => { playBtn.Visible = !e; pauseBtn.Visible = e; };
        }


        public void LoadAudio(string path)
        {
            audioPlayer.Load(path);

            // Update UI
            playingAudioLbl.Text = audioPlayer.FileName;
            wvFmtLbl.Text = audioPlayer.WaveFormatDesc;
            timeBar.Maximum = (int)audioPlayer.TotalTime.TotalSeconds;
        }

        private void playBtn_Click(object sender, EventArgs e) => audioPlayer.Play();

        private void pauseBtn_Click(object sender, EventArgs e) => audioPlayer.Pause();

        private void stopBtn_Click(object sender, EventArgs e) => audioPlayer.Stop();

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            audioPlayer.PlaybackSpeed = trackBar1.Value / 100f;
            speedLbl.Text = audioPlayer.PlaybackSpeed + "x";
        }

        private void pitchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            audioPlayer.PreservePitch = pitchCheckBox.Checked;
        }

        private void timeBar_Scroll(object sender, EventArgs e)
        {
            audioPlayer.Seek(TimeSpan.FromSeconds(timeBar.Value));
        }
    }
}
