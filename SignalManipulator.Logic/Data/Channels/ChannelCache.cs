namespace SignalManipulator.Logic.Data.Channels
{
    public class ChannelCache<T> : IChannelDataProvider<T>
    {
        private readonly Dictionary<AudioChannel, Lazy<T>> cache = [];
        public IEnumerable<AudioChannel> AvailableChannels => cache.Keys;

        public void Register(AudioChannel channel, Func<T> factory)
        {
            if (!cache.ContainsKey(channel))
                cache[channel] = new Lazy<T>(factory);
        }

        public T this[AudioChannel channel] => GetOrThrow(channel);

        public T Get(AudioChannel channel)
        {
            TryGet(channel, out var data);
            return data;
        }

        public T GetOrThrow(AudioChannel channel)
        {
            if (!TryGet(channel, out var data))
                throw new InvalidOperationException($"Channel '{channel}' is not registered.");
            return data;
        }

        public bool TryGet(AudioChannel channel, out T value)
        {
            if (cache.TryGetValue(channel, out var lazy))
            {
                value = lazy.Value;
                return true;
            }
            value = default!;
            return false;
        }

        public bool HasChannel(AudioChannel channel) => cache.ContainsKey(channel);
    }
}