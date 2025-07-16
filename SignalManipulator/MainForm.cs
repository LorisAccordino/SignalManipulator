using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Core.Export;

namespace SignalManipulator
{
    public partial class MainForm : Form
    {
        private AudioEngine audioEngine = AudioEngine.Instance;
        public MainForm()
        {
            InitializeComponent();
            InitializeEvents();
        }

        public void InitializeEvents()
        {

        }

        private void OnOpenAudio_Click(object sender, EventArgs e)
        {
            if (audioOFD.ShowDialog() == DialogResult.OK)
            {
                audioPlayerControl.LoadAudio(audioOFD.FileName);
            }
        }

        private void OnSaveAudio_Click(object sender, EventArgs e)
        {
            if (audioSFD.ShowDialog() == DialogResult.OK)
            {
                var audioSource = audioEngine.FileAudioSource;
                AudioExporter.ExportToWav(audioEngine.AudioDataProvider, audioSource.Info.WaveStream, audioSFD.FileName, audioSource.Info.TotalTime);
            }
        }

        private void ShowHideEffects(object sender, EventArgs e)
        {
            mainSplitContainer.Panel1Collapsed = !effectsToolStripMenuItem.Checked;
        }

        private void ShowHidePlaybackAndRouting(object sender, EventArgs e)
        {
            bool showPlayback = playbackToolStripMenuItem.Checked;
            bool showRouting = routingToolStripMenuItem.Checked;

            rightSideSplitContainer.Panel2Collapsed = !(showPlayback || showRouting);
            bottomSplitContainer.Panel1Collapsed = !showPlayback;
            bottomSplitContainer.Panel2Collapsed = !showRouting;
        }
    }
}
