using SignalManipulator.Forms;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Core.Playback;
using SignalManipulator.Logic.Events;
using SignalManipulator.UI.Helpers;
using SignalManipulator.UI.Misc;

namespace SignalManipulator.Controls
{
    public partial class AudioPlayerControl : UserControl
    {
        private AudioPlayer audioPlayer;
        private AudioEventDispatcher audioEventDispatcher;
        private AudioInfoDialog audioInfoDialog = new AudioInfoDialog();

        public AudioPlayerControl()
        {
            InitializeComponent();

            if (!DesignModeHelper.IsDesignMode)
            {
                audioPlayer = AudioEngine.Instance.AudioPlayer;
                audioEventDispatcher = AudioEngine.Instance.AudioEventDispatcher;
                InitializePlaybackEvents();

                // Ensure to disable UI initially
                Enabled = false;

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
            playbackSpeedSlider.ValueChanged += (s, speed) => audioPlayer.PlaybackSpeed = speed;
            timeSlider.OnSeek += audioPlayer.Seek;
            volumeSlider.ValueChanged += (s, volume) => audioPlayer.Volume = volume;
            pitchCheckBox.CheckedChanged += (s, e) => audioPlayer.PreservePitch = pitchCheckBox.Checked;
        }

        private void OnStarted(object? sender, EventArgs e)
        {
            settingsPanel.Enabled = true;
        }

        private void OnStopped(object? sender, EventArgs e)
        {
            volumeSlider.Value = 1.0;
            playbackSpeedSlider.Value = 1.0;
            pitchCheckBox.CheckState = CheckState.Unchecked;

            settingsPanel.Enabled = false;
        }

        private void OnPlaybackStateChanged(object? sender, bool playing)
        {
            playBtn.Visible = !playing;
            pauseBtn.Visible = playing;
        }

        private void OnUpdate()
        {
            timeSlider.SyncWith(audioPlayer.Info.CurrentTime);
        }


        public void LoadAudio(string? path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                audioPlayer.Load(path);
                audioInfoDialog.SetInfo(audioPlayer.Info);
                moreInfoLbl.Visible = true;
                Enabled = true; // Ensure to enable UI after loading audio
            }

            // Update UI
            playingAudioLbl.Value = audioPlayer.Info.FileName;
            waveFmtLbl.Text = audioPlayer.Info.WaveFormatDescription;
            timeSlider.TotalTime = audioPlayer.Info.TotalTime;
        }

        private void OnPlay(object sender, EventArgs e)
        {
            UIUpdateService.Instance.Start();
            audioPlayer.Play();
        }

        private void OnPause(object sender, EventArgs e) => audioPlayer.Pause();

        private void OnStop(object sender, EventArgs e)
        {
            audioPlayer.Stop();
            UIUpdateService.Instance.Stop();
        }

        private void OnShowMoreInfo(object sender, LinkLabelLinkClickedEventArgs e)
        {
            audioInfoDialog.ShowDialog();
        }
    }
}