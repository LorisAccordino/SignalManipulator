using SignalManipulator.Logic.Effects;
using System.Windows.Forms;

namespace SignalManipulator.Logic.Info
{
    public class EffectUIInfo
    {
        public Type UIType { get; }
        public Type EffectType { get; }

        public EffectUIInfo(Type uiType, Type effectType)
        {
            UIType = uiType;
            EffectType = effectType;
        }

        public Form? CreateUIInstance(IAudioEffect effect)
        {
            var ctor = UIType.GetConstructor([effect.GetType()]) ??
                       UIType.GetConstructor([typeof(IAudioEffect)]);

            if (ctor != null)
                return (Form?)ctor.Invoke([effect]);

            return null;
        }
    }
}