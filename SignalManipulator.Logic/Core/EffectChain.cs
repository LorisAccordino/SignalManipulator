using NAudio.Wave;
using SignalManipulator.Logic.Effects;
using SignalManipulator.Logic.Providers;
using SignalManipulator.Logic.Utils;

namespace SignalManipulator.Logic.Core
{
    // TODO: Exclude from effect plugin loader
    public class EffectChain : AudioEffect
    {
        public override string Name => "Effect Chain";
        public IReadOnlyList<IAudioEffect> EffectList => effectList;
        private List<IAudioEffect> effectList = new List<IAudioEffect>();

        public EffectChain() : this(DefaultAudioProvider.Empty) { }
        public EffectChain(ISampleProvider sourceProvider) : base(sourceProvider) { }

        public void AddEffect<T>() where T : IAudioEffect
        {
            ISampleProvider input = effectList.Count == 0 ? sourceProvider : effectList.Last();
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
            if (effectList.Count > 0) effectList[0].SetSource(newSource);
        }

        public override int Process(float[] samples, int offset, int count)
        {
            return effectList.Count == 0 ? sourceProvider.Read(samples, offset, count) :
                effectList.Last().Read(samples, offset, count);
        }

        public override void Reset()
        {
            base.Reset();
            foreach (var effect in effectList) effect.Reset();
        }
    }
}