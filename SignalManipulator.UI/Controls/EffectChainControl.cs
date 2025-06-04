using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Effects;
using SignalManipulator.UI.Helpers;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    public partial class EffectChainControl: UserControl
    {
        private EffectChain effectChain;

        public EffectChainControl()
        {
            InitializeComponent();

            if (!DesignModeHelper.IsDesignMode)
            {
                effectChain = AudioEngine.Instance.EffectChain;
            }
        }

        private void addEffectButton_Click(object sender, System.EventArgs e)
        {

        }

        private void removeEffectButton_Click(object sender, System.EventArgs e)
        {

        }
    }
}