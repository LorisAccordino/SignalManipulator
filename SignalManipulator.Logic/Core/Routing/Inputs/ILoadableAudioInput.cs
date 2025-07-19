namespace SignalManipulator.Logic.Core.Routing.Inputs
{
    public interface ILoadableAudioInput : IAudioInput
    {
        void Load(string path);
    }
}