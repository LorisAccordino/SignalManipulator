namespace SignalManipulator.Logic.Data.Channels
{
    public interface IChannelDataProvider
    {
        IEnumerable<AudioChannel> AvailableChannels { get; }
        double[] Get(AudioChannel mode);
        double[] GetOrThrow(AudioChannel mode);
        bool TryGet(AudioChannel mode, out double[] data);
        bool HasChannel(AudioChannel mode);
    }
}