using NAudio.Wave;
using SignalManipulator.Logic.Helpers;
using SignalManipulator.Logic.Providers;
using System.Collections.Generic;
using System.Linq;

namespace SignalManipulator.Logic.Effects
{
    public class EffectChain : ISampleProvider
    {
        //public WaveFormat WaveFormat => SourceProvider.InnerProvider?.WaveFormat;
        public WaveFormat WaveFormat => SourceProvider?.WaveFormat;
        //public IWaveProvider Output => effectList.Count == 0 ? SourceProvider.InnerProvider : EffectList.Last();
        public IReadOnlyList<IAudioEffect> EffectList => effectList;
        private List<IAudioEffect> effectList = new List<IAudioEffect>();

        // Audio modules references
        //private AudioEngine audioEngine;
        //public DynamicWaveProvider SourceProvider { get; private set; } = new DynamicWaveProvider();
        public ISampleProvider SourceProvider { get; private set; } = new DefaultSampleProvider();

        //public EffectChain(AudioEngine audioEngine)
        public EffectChain()
        {
            //this.audioEngine = audioEngine;
        }

        public void AddEffect<T>() where T : IAudioEffect
        {
            //ISampleProvider input = effectList.Count == 0 ? (ISampleProvider)SourceProvider : effectList.Last();
            //ISampleProvider input = effectList.Count == 0 ? SourceProvider.ToSampleProvider() : effectList.Last();
            ISampleProvider input = effectList.Count == 0 ? SourceProvider : effectList.Last();
            var factory = EffectFactory.Create<T>();
            effectList.Add(factory(input));
        }

        public IAudioEffect GetEffect(int index)
        {
            return GetEffect<IAudioEffect>(index);
        }

        public T GetEffect<T>(int index) where T : IAudioEffect
        {
            return (T)effectList[index];
        }

        public void RemoveEffect(IAudioEffect effect)
        {
            effectList.Remove(effect);
        }

        public void RebuildChain()
        {
            ISampleProvider current = SourceProvider ?? new DefaultSampleProvider();

            foreach (var effect in effectList)
            {
                if (effect is AudioEffect audioEffect)
                {
                    audioEffect.SetSource(current);
                    current = audioEffect;
                }
            }
        }

        public void SetSource(ISampleProvider newSource)
        {
            SourceProvider = newSource;
            if (effectList.Count > 0) effectList[0].SetSource(newSource);
        }


        public int Read(float[] samples,  int offset, int count)
        {
            //return effectList.Count == 0 ? SourceProvider.InnerProvider.ToSampleProvider().Read(samples, offset, count) :
            return effectList.Count == 0 ? SourceProvider.Read(samples, offset, count) :
                effectList.Last().Read(samples, offset, count);
        }

        public void ResetAll()
        {
            foreach (var effect in effectList)
                effect.Reset();
        }
    }
}
