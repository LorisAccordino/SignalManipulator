using SignalManipulator.Logic.Attributes;
using SignalManipulator.Logic.Effects;

namespace SignalManipulator.EffectUI
{
    [EffectUIFor(typeof(EchoEffect))]
    public partial class EchoEffectUI : EffectUIForm
    {
        private EchoEffect? EchoEffect => Effect as EchoEffect;
        public EchoEffectUI()
        {
            InitializeComponent();
        }

        public EchoEffectUI(EchoEffect effect) : base(effect)
        {
            InitializeComponent();
            LoadValuesFromEffect();
            InitializeEvents();
        }

        protected void LoadValuesFromEffect()
        {
            if (EchoEffect == null) return;

            delaySlider.Value = EchoEffect.DelayMs;
            feedbackSlider.Value = EchoEffect.Feedback;
            wetMix.Value = EchoEffect.WetMix;
            dryMix.Value = EchoEffect.DryMix;
        }

        protected void InitializeEvents()
        {
            if (EchoEffect == null) return;

            // Value changed events
            delaySlider.ValueChanged += (s, e) => EchoEffect.DelayMs = (int)delaySlider.Value;
            feedbackSlider.ValueChanged += (s, e) => EchoEffect.Feedback = (float)feedbackSlider.Value;
            wetMix.ValueChanged += (s, e) => EchoEffect.WetMix = (float)wetMix.Value;
            dryMix.ValueChanged += (s, e) => EchoEffect.DryMix = (float)dryMix.Value;
        }
    }
}