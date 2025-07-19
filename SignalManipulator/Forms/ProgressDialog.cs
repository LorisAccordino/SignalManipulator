namespace SignalManipulator.Forms
{
    public partial class ProgressDialog : Form
    {
        public ProgressDialog(string title)
        {
            InitializeComponent();
            Text = title;
        }

        public void UpdateProgress(float progress)
        {
            progress = Math.Clamp(progress, 0f, 1f);
            int percent = (int)(progress * 100);

            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateProgress(progress)));
            }
            else
            {
                progressBar.Value = percent;
                percentLbl.Text = $"{percent}%";
            }
        }

        private void abortBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
