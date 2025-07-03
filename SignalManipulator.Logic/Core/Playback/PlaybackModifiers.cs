using NAudio.Wave;
using SignalManipulator.Logic.Effects;
using SignalManipulator.Logic.Effects.RubberBand;
using SignalManipulator.Logic.Providers;

namespace SignalManipulator.Logic.Core.Playback
{
    public class PlaybackModifiers
    {
        public VolumeEffect VolumeEffect { get; }
        public RBTimeStretchEffect TimeStretchEffect { get; }

        public ISampleProvider Output => TimeStretchEffect;

        public PlaybackModifiers() : this(DefaultSampleProvider.Empty) { }
        public PlaybackModifiers(ISampleProvider sourceProvider)
        {
            VolumeEffect = new VolumeEffect(sourceProvider);
            TimeStretchEffect = new RBTimeStretchEffect(VolumeEffect);
        }

        public void SetSource(ISampleProvider source)
        {
            VolumeEffect.SetSource(source); // FIRST
        }

        public void Reset()
        {
            TimeStretchEffect?.Reset(); // LAST
        }

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
    }
}