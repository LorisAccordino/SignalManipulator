using NAudio.Wave;
using SignalManipulator.Logic.Attributes;
using SignalManipulator.Logic.Core.Effects.Loaders;
using SignalManipulator.Logic.Effects;
using SignalManipulator.Logic.Info;
using SignalManipulator.Logic.Providers;

namespace SignalManipulator.Logic.Core.Effects
{
    [ExcludeFromEffectLoader]
    [Effect("Effect Chain")]
    public class EffectChain : AudioEffect
    {
        public IReadOnlyList<IAudioEffect> EffectList => effectList.AsReadOnly();
        private List<IAudioEffect> effectList = new List<IAudioEffect>();

        public EffectChain() : this(DefaultAudioProvider.Empty) { }
        public EffectChain(ISampleProvider sourceProvider) : base(sourceProvider) { }
        
        public void AddEffect(EffectInfo effectInfo)
        {
            var input = effectList.Count == 0 ? sourceProvider : GetLastEffect();
            var effect = effectInfo.CreateInstance(input);
            if (effect != null) effectList.Add(effect);
        }

        public void AddEffect<T>() where T : IAudioEffect
        {
            var effectInfo = EffectLoader.GetEffectInfoByType<T>();
            if (effectInfo != null) AddEffect(effectInfo);
        }

        public void RemoveEffect(IAudioEffect effect)
        {
            if (effectList.Remove(effect))
                RebuildChain();
        }

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < effectList.Count)
            {
                effectList.RemoveAt(index);
                RebuildChain();
            }
        }

        public IAudioEffect GetEffect(int index) => effectList[index];
        public IAudioEffect GetFirstEffect() => effectList.First();
        public IAudioEffect GetLastEffect() => effectList.Last();

        public T GetEffect<T>(int index) where T : IAudioEffect => (T)effectList[index];

        public void Clear()
        {
            effectList.Clear();
        }

        public void RebuildChain()
        {
            ISampleProvider current = sourceProvider ?? new DefaultAudioProvider();

            foreach (var effect in effectList)
            {
                if (effect is AudioEffect audioEffect)
                {
                    audioEffect.SetSource(current);
                    current = audioEffect;
                }
            }
        }

        public override void SetSource(ISampleProvider newSource)
        {
            base.SetSource(newSource);
            RebuildChain();
        }

        public override int Process(float[] samples, int offset, int count)
        {
            return effectList.Count == 0 ? sourceProvider.Read(samples, offset, count) :
                GetLastEffect().Read(samples, offset, count);
        }

        public override void Reset()
        {
            base.Reset();
            foreach (var effect in effectList) effect.Reset();
        }
    }
}