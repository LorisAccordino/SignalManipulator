namespace SignalManipulator.Logic.Models
{
    public enum ChannelMode
    {
        Stereo,
        Left,
        Right,
        Mid,
        Side,
        Mono // Meant as sum L+R/2
    }

    public interface IChannelProvider
    {
        IEnumerable<ChannelMode> AvailableChannels { get; }
        double[] Get(ChannelMode mode);
        double[] GetOrThrow(ChannelMode mode);
        bool TryGet(ChannelMode mode, out double[] data);
        bool HasChannel(ChannelMode mode);
    }
}