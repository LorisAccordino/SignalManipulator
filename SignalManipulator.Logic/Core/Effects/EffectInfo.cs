using NAudio.Wave;
using SignalManipulator.Logic.Effects;
using System.Reflection;

namespace SignalManipulator.Logic.Core.Effects
{
    public class EffectInfo
    {
        public string Name { get; }
        public string Category { get; }
        public string Description { get; }
        public Type EffectType { get; }

        private readonly ConstructorInfo constructor;

        public EffectInfo(Type effectType)
        {
            EffectType = effectType;

            var attr = effectType.GetCustomAttribute<EffectAttribute>();
            Category = attr?.Category ?? "Misc";
            Description = attr?.Description ?? "";
            Name = attr?.Name ?? "";

            constructor = effectType.GetConstructor([typeof(ISampleProvider)])
                ?? throw new InvalidOperationException($"Missing constructor in {effectType.Name}");
        }

        public IAudioEffect CreateInstance(ISampleProvider source) => (IAudioEffect)constructor.Invoke([source]);
    }
}