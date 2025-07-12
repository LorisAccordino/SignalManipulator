namespace SignalManipulator.Logic.Data.Channels
{
    public interface IChannelDataProvider<T>
    {
        IEnumerable<AudioChannel> AvailableChannels { get; }
        T Get(AudioChannel channel);
        T GetOrThrow(AudioChannel channel);
        bool TryGet(AudioChannel channel, out T data);
        bool HasChannel(AudioChannel channel);
    }
}