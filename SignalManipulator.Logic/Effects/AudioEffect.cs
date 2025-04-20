namespace SignalManipulator.Logic.Effects
{
    public abstract class AudioEffect
    {
        public abstract byte[] Process(byte[] input);
    }
}
