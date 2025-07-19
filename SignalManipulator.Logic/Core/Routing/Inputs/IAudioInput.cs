using NAudio.Wave;

namespace SignalManipulator.Logic.Core.Routing.Inputs
{
    public interface IAudioInput : IWaveProvider, ISampleProvider, IDisposable
    {
        event EventHandler? Ready;
        event EventHandler<byte[]>? OnBytes;
        bool IsReady { get; }
        bool IsRealTime { get; }
        void Start();
        void Stop();
    }
}