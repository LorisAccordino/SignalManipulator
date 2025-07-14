namespace SignalManipulator.Logic.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EffectUIForAttribute : Attribute
    {
        public Type EffectType { get; }
        public EffectUIForAttribute(Type effectType) => EffectType = effectType;
    }
}