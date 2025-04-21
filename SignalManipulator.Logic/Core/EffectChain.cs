using NAudio.Wave;
using SignalManipulator.Logic.Effects;
using SignalManipulator.Logic.Providers;
using System.Collections.Generic;
using System.Linq;

namespace SignalManipulator.Logic.Core
{
    public class EffectChain
    {
        private List<IAudioEffect> effectList = new List<IAudioEffect>();
        public IReadOnlyList<IAudioEffect> EffectList => effectList;

        // Audio modules references
        private AudioEngine audioEngine;
        public DynamicWaveProvider SourceProvider { get; private set; } = new DynamicWaveProvider();

        public EffectChain(AudioEngine audioEngine)
        {
            this.audioEngine = audioEngine;
        }

        public void AddEffect<T>() where T : IAudioEffect
        {
            IWaveProvider input = effectList.Count == 0 ? (IWaveProvider)SourceProvider : effectList.Last();
            var factory = EffectFactoryHelper.Create<T>();
            effectList.Add(factory(input));
        }

        public IAudioEffect GetEffect(int index)
        {
            return effectList[index];
        }

        public void RemoveEffect(IAudioEffect effect)
        {
            effectList.Remove(effect);
        }

        public void ProcessEffects(byte[] buffer)
        {
            if (effectList.Count == 0) 
                SourceProvider.Read(buffer, 0, buffer.Length);
            else
                effectList.Last().Read(buffer, 0, buffer.Length);
        }
    }
}
