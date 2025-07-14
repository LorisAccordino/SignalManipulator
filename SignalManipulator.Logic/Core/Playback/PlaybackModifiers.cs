using NAudio.Wave;
using SignalManipulator.Logic.Core.Effects;
using SignalManipulator.Logic.Effects;
using SignalManipulator.Logic.Effects.RubberBand;
using SignalManipulator.Logic.Providers;

namespace SignalManipulator.Logic.Core.Playback
{
    [ExcludeFromEffectLoader]
    [Effect("Playback Modifiers")]
    public class PlaybackModifiers : AudioEffect
    {
        public VolumeEffect VolumeEffect { get; }
        public RBTimeStretchEffect TimeStretchEffect { get; }

        public double Speed
        {
            get => TimeStretchEffect.Speed;
            set => TimeStretchEffect.Speed = value;
        }

        public bool PreservePitch
        {
            get => TimeStretchEffect.PreservePitch;
            set => TimeStretchEffect.PreservePitch = value;
        }

        public double Volume
        {
            get => VolumeEffect.Volume;
            set => VolumeEffect.Volume = value;
        }


        public PlaybackModifiers() : this(DefaultAudioProvider.Empty) { }
        public PlaybackModifiers(ISampleProvider sourceProvider) : base(sourceProvider)
        {
            VolumeEffect = new VolumeEffect(sourceProvider);
            TimeStretchEffect = new RBTimeStretchEffect(VolumeEffect);
        }

        public override void SetSource(ISampleProvider source)
        {
            base.SetSource(source);
            VolumeEffect.SetSource(source); // FIRST
        }

        public override int Process(float[] buffer, int offset, int count)
        {
            return TimeStretchEffect.Read(buffer, offset, count);
        }

        public override void Reset()
        {
            base.Reset();
            TimeStretchEffect?.Reset(); // LAST
        }
    }
}