using SignalManipulator.Logic.Attributes;
using SignalManipulator.Logic.Effects;
using SignalManipulator.Logic.Info;
using System.Reflection;
using System.Windows.Forms;

namespace SignalManipulator.Logic.Core.Effects.Loaders
{
    public static class EffectUILoader
    {
        public static IReadOnlyList<EffectUIInfo> LoadedUI => loadedUI;
        private static readonly List<EffectUIInfo> loadedUI = [];

        public static void LoadFromAssembly(Assembly assembly)
        {
            var uiTypes = assembly.GetTypes()
                .Where(t => t.GetCustomAttribute<EffectUIForAttribute>() != null);

            foreach (var uiType in uiTypes)
            {
                var attr = uiType.GetCustomAttribute<EffectUIForAttribute>()!;
                loadedUI.Add(new EffectUIInfo(uiType, attr.EffectType));
            }
        }

        public static Form? CreateUIForEffect(IAudioEffect effect)
        {
            var uiInfo = loadedUI.FirstOrDefault(u => u.EffectType == effect.GetType());
            if (uiInfo == null) return null;
            return uiInfo.CreateUIInstance(effect);
        }
    }
}