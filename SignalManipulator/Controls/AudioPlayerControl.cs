using SignalManipulator.Forms;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Core.Playback;
using SignalManipulator.UI.Helpers;
using SignalManipulator.UI.Misc;

namespace SignalManipulator.Controls
{
    public partial class AudioPlayerControl : UserControl
    {
        private AudioPlayer AudioPlayer;
        private UIUpdateService UIUpdate;
        private AudioInfoDialog audioInfoDialog = new AudioInfoDialog();

        public AudioPlayerControl()
        {
            InitializeComponent();

            if (!DesignModeHelper.IsDesignMode)
            {
                AudioPlayer = AudioEngine.Instance.AudioPlayer;
                UIUpdate = UIUpdateService.Instance;
                InitializeEvents();

                // Ensure to disable UI initially
                Enabled = false;

                // Initialize stuff
                LoadAudio(null);
            }
        }

        private void InitializeEvents()
        {
            // Main events
            AudioPlayer.OnStarted += OnStarted;
            AudioPlayer.OnStopped += OnStopped;
            AudioPlayer.OnPlaybackStateChanged += OnPlaybackStateChanged;

            // Update event
            UIUpdate.Register(OnUpdate);

            // Force stop event to init purpose
            OnStopped(this, EventArgs.Empty);

            // Other events
            playbackSpeedSlider.ValueChanged += (s, speed) => AudioPlayer.PlaybackSpeed = speed;
            timeSlider.OnSeek += AudioPlayer.Seek;
            volumeSlider.ValueChanged += (s, volume) => AudioPlayer.Volume = volume;
            pitchCheckBox.CheckedChanged += (s, e) => AudioPlayer.PreservePitch = pitchCheckBox.Checked;
        }

        private void OnStarted(object? sender, EventArgs e)
        {
            this.SafeInvoke(() =>
            {
                settingsPanel.Enabled = true;
            });
        }

        private void OnStopped(object? sender, EventArgs e)
        {
            this.SafeInvoke(() =>
            {
                volumeSlider.Value = 1.0;
                playbackSpeedSlider.Value = 1.0;
                pitchCheckBox.CheckState = CheckState.Unchecked;

                settingsPanel.Enabled = false;
            });
        }

        private void OnPlaybackStateChanged(object? sender, bool playing)
        {
            this.SafeInvoke(() =>
            {
                playBtn.Visible = !playing;
                pauseBtn.Visible = playing;
            });
        }


        private void OnUpdate()
        {
            this.SafeInvoke(() =>
            {
                timeSlider.SyncWith(AudioPlayer.Info.CurrentTime);
            });
        }


        public void LoadAudio(string? path)
        {
            this.SafeInvoke(() =>
            {
                if (!string.IsNullOrEmpty(path))
                {
                    AudioPlayer.Load(path);
                    audioInfoDialog.SetInfo(AudioPlayer.Info);
                    moreInfoLbl.Visible = true;
                    Enabled = true; // Ensure to enable UI after loading audio
                }

                // Update UI
                playingAudioLbl.Value = AudioPlayer.Info.FilePath;
                waveFmtLbl.Text = AudioPlayer.Info.WaveFormatDescription;
                timeSlider.TotalTime = AudioPlayer.Info.TotalTime;
            });
        }

        private void OnPlay(object sender, EventArgs e)
        {
            UIUpdate.Start();
            AudioPlayer.Play();
        }

        private void OnPause(object sender, EventArgs e) => AudioPlayer.Pause();

        private void OnStop(object sender, EventArgs e)
        {
            AudioPlayer.Stop();
            UIUpdate.Stop();
        }

        private void OnShowMoreInfo(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.SafeInvoke(() =>
            {
                audioInfoDialog.ShowDialog();
            });
        }
    }
}