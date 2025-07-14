namespace SignalManipulator.Logic.Effects
{
    public interface IEffectUI
    {
        IAudioEffect Effect { get; set; }
        void Show();
    }
}