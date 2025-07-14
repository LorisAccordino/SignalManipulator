using NAudio.Wave;
using SignalManipulator.Logic.Effects;
using System.Reflection;

namespace SignalManipulator.Logic.Utils
{
    public static class EffectPluginLoader
    {
        // Dictionary: effect name -> factory (Func<ISampleProvider, IAudioEffect>)
        private static readonly Dictionary<string, Func<ISampleProvider, IAudioEffect>> _factories = new();

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
                .Where(t => !t.IsAbstract && effectType.IsAssignableFrom(t));

            foreach (var type in types)
            {
                var ctor = type.GetConstructor([typeof(ISampleProvider)]);
                if (ctor == null) continue; // Ignore classes without correct constructor

                Func<ISampleProvider, IAudioEffect> factory = (prev) =>
                    (IAudioEffect)ctor.Invoke([prev]);

                _factories[type.Name] = factory;
            }
        }

        // Load plugin from a specific DLL path
        public static void LoadFromDll(string dllPath)
        {
            var assembly = Assembly.LoadFrom(dllPath);
            LoadFromAssembly(assembly);
        }

        // Retrieve the list of the names of the loaded effects
        public static IEnumerable<string> GetAvailableEffectNames() => _factories.Keys;

        // Create an instance of the effect plugin through name (or null if it does not exist)
        public static IAudioEffect? CreateInstance(string effectName, ISampleProvider previous)
        {
            if (_factories.TryGetValue(effectName, out var factory))
            {
                return factory(previous);
            }
            return null;
        }

        // Create an instance of the effect directly from the Type
        public static IAudioEffect? CreateInstance(Type effectType, ISampleProvider previous)
        {
            if (!typeof(IAudioEffect).IsAssignableFrom(effectType) || effectType.IsAbstract)
                return null;

            var ctor = effectType.GetConstructor([typeof(ISampleProvider)]);
            if (ctor == null)
                return null;

            return (IAudioEffect?)ctor.Invoke([previous]);
        }

    }
}