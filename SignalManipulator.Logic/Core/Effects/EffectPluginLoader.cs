using NAudio.Wave;
using SignalManipulator.Logic.Effects;
using System.Reflection;

namespace SignalManipulator.Logic.Core.Effects
{
    public static class EffectPluginLoader
    {
        private static readonly List<EffectInfo> loadedEffects = new();
        public static IReadOnlyList<EffectInfo> LoadedEffects => loadedEffects;

        // Load plugin from the current assembly (where AudioEffect is defined)
        public static void LoadBuiltinEffects()
        {
            LoadFromAssembly(Assembly.GetExecutingAssembly());
        }

        // Load plugin from extern assembly (DLL)
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

        // Load plugin from a specific DLL path
        public static void LoadFromDll(string dllPath)
        {
            var assembly = Assembly.LoadFrom(dllPath);
            LoadFromAssembly(assembly);
        }

        public static IEnumerable<EffectInfo> GetAvailableEffects() => loadedEffects;

        public static EffectInfo? GetEffectByName(string name)
        => loadedEffects.FirstOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        public static IAudioEffect? CreateInstance(string name, ISampleProvider previous)
        => GetEffectByName(name)?.CreateInstance(previous);

        public static IAudioEffect? CreateInstance(Type type, ISampleProvider previous)
        => loadedEffects.FirstOrDefault(e => e.EffectType == type)?.CreateInstance(previous);
    }
}