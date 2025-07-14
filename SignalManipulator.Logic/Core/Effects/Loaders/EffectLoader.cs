using NAudio.Wave;
using SignalManipulator.Logic.Attributes;
using SignalManipulator.Logic.Effects;
using SignalManipulator.Logic.Info;
using System.Reflection;

namespace SignalManipulator.Logic.Core.Effects.Loaders
{
    public static class EffectLoader
    {
        private static readonly List<EffectInfo> loadedEffects = new();
        public static IReadOnlyList<EffectInfo> LoadedEffects => loadedEffects;

        // Load effects from the current assembly (where AudioEffect is defined)
        public static void LoadBuiltinEffects()
        {
            LoadFromAssembly(Assembly.GetExecutingAssembly());
        }

        // Load effects from extern assembly (DLL)
        public static void LoadFromAssembly(Assembly assembly)
        {
            var effectType = typeof(AudioEffect);

            var types = assembly.GetTypes()
                .Where(t => !t.IsAbstract
                && effectType.IsAssignableFrom(t)
                && t.GetCustomAttribute<ExcludeFromEffectLoaderAttribute>() == null);

            foreach (var type in types)
                loadedEffects.Add(new EffectInfo(type));
        }

        // Load effects from a specific DLL path
        public static void LoadFromDll(string dllPath)
        {
            var assembly = Assembly.LoadFrom(dllPath);
            LoadFromAssembly(assembly);
        }

        public static IEnumerable<EffectInfo> GetAvailableEffects()
            => loadedEffects;

        public static EffectInfo? GetEffectByName(string name)
            => loadedEffects.FirstOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        public static EffectInfo? GetEffectInfoByType(Type type)
            => loadedEffects.FirstOrDefault(e => e.EffectType == type);

        public static EffectInfo? GetEffectInfoByType<T>() where T : IAudioEffect
            => GetEffectInfoByType(typeof(T));

        public static IAudioEffect? CreateInstance(string name, ISampleProvider previous)
            => GetEffectByName(name)?.CreateInstance(previous);

        public static IAudioEffect? CreateInstance(Type type, ISampleProvider previous)
            => loadedEffects.FirstOrDefault(e => e.EffectType == type)?.CreateInstance(previous);
    }
}