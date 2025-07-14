using SignalManipulator.Logic.Effects;

namespace SignalManipulator.EffectUI
{
    public partial class EffectUIForm : Form, IEffectUI
    {
        public IAudioEffect Effect { get; set; }

        public EffectUIForm()
        {
            InitializeComponent();
        }

        public EffectUIForm(IAudioEffect effect) : this()
        {
            Effect = effect;
        }
    }
}