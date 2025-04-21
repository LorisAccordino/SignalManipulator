using SignalManipulator.Logic.Core;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    public partial class EffectChainControl: UserControl
    {
        private EffectChain effectChain;

        public EffectChainControl()
        {
            InitializeComponent();
        }

        private void EffectChainControl_Load(object sender, System.EventArgs e)
        {
            effectChain = AudioEngine.Instance.EffectChain;
        }

        private void addEffectButton_Click(object sender, System.EventArgs e)
        {

        }

        private void removeEffectButton_Click(object sender, System.EventArgs e)
        {

        }
    }
}
