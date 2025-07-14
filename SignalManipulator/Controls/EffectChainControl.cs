using SignalManipulator.Logic.Core;
using SignalManipulator.UI.Helpers;

namespace SignalManipulator.Controls
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

        private void addEffectButton_Click(object sender, EventArgs e)
        {

        }

        private void removeEffectButton_Click(object sender, EventArgs e)
        {

        }
    }
}