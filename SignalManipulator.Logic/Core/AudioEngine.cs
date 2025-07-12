using NAudio.Wave;
using SignalManipulator.Logic.Core.Playback;
using SignalManipulator.Logic.Core.Routing;
using SignalManipulator.Logic.Core.Source;
using SignalManipulator.Logic.Effects;
using SignalManipulator.Logic.Events;
using SignalManipulator.Logic.Providers;

namespace SignalManipulator.Logic.Core
{
    public class AudioEngine
    {
        // --- Constants ---
        public const int SAMPLE_RATE = 44100;
        public const int CHANNELS = 2;
        public const int TARGET_FPS = 30;

        public const int MAX_MAGNITUDE_DB = 125;
        public const int FFT_SIZE = 8192;

        public static int CurrentFFTSize = FFT_SIZE;


        public static readonly WaveFormat WAVE_FORMAT =
            WaveFormat.CreateIeeeFloatWaveFormat(SAMPLE_RATE, CHANNELS);

        // --- Singleton ---
        private static readonly AudioEngine instance = new AudioEngine();
        public static AudioEngine Instance => instance;

        // --- Core modules (private) ---
        private readonly FileAudioSource audioFileLoader = new FileAudioSource();
        private readonly AudioRouter audioRouter = new AudioRouter();
        private readonly EffectChain effectChain = new EffectChain();
        private readonly AudioDataProvider audioDataProvider;
        private readonly PlaybackService playbackService;
        private readonly AudioPlayer playbackController;
        private readonly IAudioEventDispatcher audioEventDispatcher;

        // --- Public modules (exposed) ---
        public AudioRouter AudioRouter => audioRouter;
        public EffectChain EffectChain => effectChain;
        public AudioPlayer PlaybackController => playbackController;
        public IAudioEventDispatcher AudioEventDispatcher => audioEventDispatcher;

        // --- Instance constructor ---
        private AudioEngine()
        {
            audioDataProvider = new AudioDataProvider(effectChain);
            playbackService = new PlaybackService(audioFileLoader, audioRouter, effectChain, audioDataProvider);
            playbackController = new AudioPlayer(playbackService, audioRouter);
            audioEventDispatcher = new AudioEventDispatcher(playbackService, audioDataProvider);
        }
    }
}