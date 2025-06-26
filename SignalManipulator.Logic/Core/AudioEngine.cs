using NAudio.Wave;
using SignalManipulator.Logic.Core.Playback;
using SignalManipulator.Logic.Core.Routing;
using SignalManipulator.Logic.Core.Sourcing;
using SignalManipulator.Logic.Effects;
using SignalManipulator.Logic.Events;
using SignalManipulator.Logic.Providers;

namespace SignalManipulator.Logic.Core
{
    public class AudioEngine
    {
        // --- Constants ---
        public const int CHUNK_SIZE = 512;
        public const int SAMPLE_RATE = 44100;
        public const int CHANNELS = 2;
        public const int TARGET_FPS = 60;

        public static readonly WaveFormat WAVE_FORMAT =
            WaveFormat.CreateIeeeFloatWaveFormat(SAMPLE_RATE, CHANNELS);

        // --- Singleton ---
        private static readonly AudioEngine instance = new AudioEngine();
        public static AudioEngine Instance => instance;

        // --- Core modules (private) ---
        private readonly IAudioSource audioLoader = new AudioFileLoader();
        private readonly IAudioRouter audioRouter = new AudioRouter();
        private readonly EffectChain effectChain = new EffectChain();
        private readonly AudioDataProvider audioDataProvider;
        private readonly IPlaybackService playbackService;
        private readonly IPlaybackController playbackController;
        private readonly IAudioEventDispatcher audioEventDispatcher;

        // --- Public modules (exposed) ---
        public IAudioRouter AudioRouter => audioRouter;
        public EffectChain EffectChain => effectChain;
        public IPlaybackController PlaybackController => playbackController;
        public IAudioEventDispatcher AudioEventDispatcher => audioEventDispatcher;

        // --- Instance constructor ---
        private AudioEngine()
        {
            audioDataProvider = new AudioDataProvider(effectChain);
            playbackService = new PlaybackService(audioLoader, audioRouter, effectChain, audioDataProvider);
            playbackController = new PlaybackController(playbackService, audioRouter);
            audioEventDispatcher = new AudioEventDispatcher(playbackService, audioDataProvider);
        }
    }
}