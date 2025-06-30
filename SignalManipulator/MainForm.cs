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
    }
}
