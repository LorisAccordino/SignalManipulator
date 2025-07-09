namespace SignalManipulator.Logic.Models
{
    public enum AudioChannel
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
        IEnumerable<AudioChannel> AvailableChannels { get; }
        double[] Get(AudioChannel mode);
        double[] GetOrThrow(AudioChannel mode);
        bool TryGet(AudioChannel mode, out double[] data);
        bool HasChannel(AudioChannel mode);
    }
}