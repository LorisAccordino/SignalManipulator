using SignalManipulator.Logic.Attributes;
using SignalManipulator.Logic.Effects;
namespace SignalManipulator.EffectUI
{
    [EffectUIFor(typeof(VolumeEffect))]
    public partial class VolumeEffectUI : EffectUIForm
    {
        private VolumeEffect? VolumeEffect => Effect as VolumeEffect;

        public VolumeEffectUI()
        {
            InitializeComponent();
        }

        public VolumeEffectUI(VolumeEffect effect) : base(effect)
        {
            InitializeComponent();
            LoadValuesFromEffect();
            InitializeEvents();
        }

        protected void LoadValuesFromEffect()
        {
            if (VolumeEffect == null) return;
            volumeSlider.Value = VolumeEffect.Volume;
        }

        protected void InitializeEvents()
        {
            if (VolumeEffect == null) return;
            volumeSlider.ValueChanged += (s, e) => VolumeEffect.Volume = volumeSlider.Value;
        }
    }
}
