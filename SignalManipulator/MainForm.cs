using SignalManipulator.Logic.Core;

namespace SignalManipulator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeEvents();
        }

        public void InitializeEvents()
        {
            
        }

        private void openAudioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (audioOFD.ShowDialog() == DialogResult.OK)
            {
                audioPlayerControl.LoadAudio(audioOFD.FileName);
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
