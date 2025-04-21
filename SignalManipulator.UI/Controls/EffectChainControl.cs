using SignalManipulator.Logic.Core;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    public partial class EffectChainControl: UserControl
    {
        private EffectChain effectChain = AudioEngine.Instance.EffectChain;

        public EffectChainControl()
        {
            InitializeComponent();
        }

        private void addEffectButton_Click(object sender, System.EventArgs e)
        {

        }

        private void removeEffectButton_Click(object sender, System.EventArgs e)
        {

        }
    }
}
