namespace SignalManipulator.Logic.Core.Routing.Inputs
{
    public interface ISeekableAudioInput : IAudioInput
    {
        void Seek(TimeSpan position);
    }
}