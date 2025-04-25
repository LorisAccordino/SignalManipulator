using NAudio.Wave;
using SignalManipulator.Logic.Effects;
using System;

namespace SignalManipulator.Logic.Utils
{
    public static class EffectFactoryHelper
    {
        public static Func<ISampleProvider, IAudioEffect> Create<T>() where T : IAudioEffect
        {
            return previous =>
            {
                var constructor = typeof(T).GetConstructor(new[] { typeof(ISampleProvider) });
                if (constructor == null)
                    throw new InvalidOperationException($"The type {typeof(T).Name} hasn't a constructor with the parameter ISampleProvider.");

                return (IAudioEffect)constructor.Invoke(new object[] { previous });
            };
        }
    }
}