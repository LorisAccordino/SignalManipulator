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
        public IReadOnlyList<IAudioEffect> EffectList => effectList.AsReadOnly();
        private List<IAudioEffect> effectList = new List<IAudioEffect>();

        public EffectChain() : this(DefaultAudioProvider.Empty) { }
        public EffectChain(ISampleProvider sourceProvider) : base(sourceProvider) { }

        public void AddEffect<T>() where T : IAudioEffect
        {
            var input = effectList.Count == 0 ? sourceProvider : effectList.Last();
            var effect = EffectPluginLoader.CreateInstance(typeof(T), input);
            if (effect != null) effectList.Add(effect);
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