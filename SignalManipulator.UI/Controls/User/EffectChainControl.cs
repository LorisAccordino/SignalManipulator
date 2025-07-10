using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Effects;
using SignalManipulator.UI.Helpers;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls.User
{
    [ExcludeFromCodeCoverage]
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