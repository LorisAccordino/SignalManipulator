using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Core.Playback;
using SignalManipulator.Logic.Events;
using SignalManipulator.UI.Controls.User.Dialogs;
using SignalManipulator.UI.Helpers;
using SignalManipulator.UI.Misc;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls.User
{
    public partial class AudioPlayer : UserControl
    {
        private IPlaybackController playback;
        private IAudioEventDispatcher audioEventDispatcher;
        private AudioInfoDialog audioInfoDialog = new AudioInfoDialog();

        public AudioPlayer()
        {
            InitializeComponent();

            if (!DesignModeHelper.IsDesignMode)
            {
                playback = AudioEngine.Instance.PlaybackController;
                audioEventDispatcher = AudioEngine.Instance.AudioEventDispatcher;
                InitializePlaybackEvents();

                // Initialize stuff
                LoadAudio(null);
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
            timeSlider.OnSeek += playback.Seek;
            volumeSlider.ValueChanged += (s, volume) => playback.Volume = volume;
            pitchCheckBox.CheckedChanged += (s, e) => playback.PreservePitch = pitchCheckBox.Checked;
        }

        public void OnStarted(object? sender, EventArgs e)
        {
            settingsPanel.Enabled = true;
        }

        public void OnStopped(object? sender, EventArgs e)
        {
            volumeSlider.Value = 1.0;
            playbackSpeedSlider.Value = 1.0;
            pitchCheckBox.CheckState = CheckState.Unchecked;

            settingsPanel.Enabled = false;
        }

        public void OnPlaybackStateChanged(object? sender, bool playing)
        {
            playBtn.Visible = !playing;
            pauseBtn.Visible = playing;
        }

        private void OnUpdate()
        {
            timeSlider.SyncWith(playback.Info.CurrentTime);
        }


        public void LoadAudio(string? path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                playback.Load(path);
                audioInfoDialog.SetInfo(playback.Info);
                moreInfoLbl.Visible = true;
            }

            // Update UI
            playingAudioLbl.Value = playback.Info.FileName;
            waveFmtLbl.Text = playback.Info.WaveFormatDescription;
            timeSlider.TotalTime = playback.Info.TotalTime;
        }

        private void OnPlay(object sender, EventArgs e)
        {
            UIUpdateService.Instance.Start();
            playback.Play();
        }

        private void OnPause(object sender, EventArgs e) => playback.Pause();

        private void OnStop(object sender, EventArgs e)
        {
            playback.Stop();
            UIUpdateService.Instance.Stop();
        }

        private void OnShowMoreInfo(object sender, LinkLabelLinkClickedEventArgs e)
        {
            audioInfoDialog.ShowDialog();
        }
    }
}