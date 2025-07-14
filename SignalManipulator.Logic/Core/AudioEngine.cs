using NAudio.Wave;
using SignalManipulator.Logic.Core.Effects;
using SignalManipulator.Logic.Core.Playback;
using SignalManipulator.Logic.Core.Routing;
using SignalManipulator.Logic.Core.Source;
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
        private readonly EffectChain effectChain;
        private readonly AudioDataProvider audioDataProvider;
        private readonly PlaybackService playbackService;
        private readonly AudioPlayer audioPlayer;

        // --- Public modules (exposed) ---
        public AudioRouter AudioRouter => audioRouter;
        public EffectChain EffectChain => effectChain;
        public AudioPlayer AudioPlayer => audioPlayer;
        public AudioDataProvider AudioDataProvider => audioDataProvider;

        // --- Instance constructor ---
        private AudioEngine()
        {
            // Instantiate references
            playbackService = new PlaybackService(audioFileLoader, audioRouter);
            effectChain = new EffectChain(playbackService);
            audioDataProvider = new AudioDataProvider(effectChain);
            audioPlayer = new AudioPlayer(playbackService, audioRouter);

            // Init events
            InitializeEvents();

            // Load built-in effects
            EffectPluginLoader.LoadBuiltinEffects();
        }

        private void InitializeEvents()
        {
            audioRouter.InitOutputs(audioDataProvider as IWaveProvider);
            audioPlayer.OnStopped += OnStopped;
        }

        private void OnStopped(object? sender,  EventArgs e)
        {
            effectChain.Reset();
            audioDataProvider.ClearBuffer();
        }
    }
}