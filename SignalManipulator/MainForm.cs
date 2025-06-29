using System.Windows.Forms;

namespace SignalManipulatora
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void openAudioToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (audioOFD.ShowDialog() == DialogResult.OK)
            {
                audioPlayer.LoadAudio(audioOFD.FileName);
            }
        }
    }
}
