namespace SignalManipulator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void openAudioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (audioOFD.ShowDialog() == DialogResult.OK)
            {
                audioPlayer.LoadAudio(audioOFD.FileName);
            }
        }

        private void ShowHideEffects(object sender, EventArgs e)
        {
            mainSplitContainer.Panel1Collapsed = !effectsToolStripMenuItem.Checked;
        }

        private void ShowHidePlots(object sender, EventArgs e)
        {
            mainSplitContainer.Panel2Collapsed = !plotsToolStripMenuItem.Checked;
        }

        private void ShowHidePlayback(object sender, EventArgs e)
        {
            bottomSplitContainer.Panel1Collapsed = !playbackToolStripMenuItem.Checked;
        }

        private void ShowHideRouting(object sender, EventArgs e)
        {
            bottomSplitContainer.Panel2Collapsed = !routingToolStripMenuItem.Checked;
        }
    }
}
