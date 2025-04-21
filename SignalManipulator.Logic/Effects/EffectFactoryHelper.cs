using NAudio.Wave;
using System;

namespace SignalManipulator.Logic.Effects
{
    public static class EffectFactoryHelper
    {
        public static Func<IWaveProvider, IAudioEffect> Create<T>() where T : IAudioEffect
        {
            return previous =>
            {
                var constructor = typeof(T).GetConstructor(new[] { typeof(IWaveProvider) });
                if (constructor == null)
                    throw new InvalidOperationException($"The type {typeof(T).Name} hasn't a constructor with the parameter IWaveProvider.");

                return (IAudioEffect)constructor.Invoke(new object[] { previous });
            };
        }
    }

}
