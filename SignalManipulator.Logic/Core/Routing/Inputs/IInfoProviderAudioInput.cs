using SignalManipulator.Logic.Info;

namespace SignalManipulator.Logic.Core.Routing.Inputs
{
    public interface IInfoProviderAudioInput : IAudioInput
    {
        AudioInfo Info { get; }
    }
}