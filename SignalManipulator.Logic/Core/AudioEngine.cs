using NAudio.Wave;
using SignalManipulator.Logic.Core.Events;
using SignalManipulator.Logic.Core.Playback;
using SignalManipulator.Logic.Core.Sourcing;
using SignalManipulator.Logic.Providers;

namespace SignalManipulator.Logic.Core
{
    public class AudioEngine
    {
        // Instance
        private static readonly AudioEngine instance = new AudioEngine();
        public static AudioEngine Instance => instance;

        // Consts
        public const int CHUNK_SIZE = 512;
        public const int SAMPLE_RATE = 44100;
        public const int CHANNELS = 2;
        public const int TARGET_FPS = 15;
        public static readonly WaveFormat DEFAULT_WAVE_FORMAT = WaveFormat.CreateIeeeFloatWaveFormat(SAMPLE_RATE, CHANNELS);
        //public WaveFormat WAVE_FORMAT => AudioPlayer.WaveFormat;


        // Modules references
        private static readonly IAudioSource audioLoader = new AudioFileLoader();

        private static readonly AudioRouter audioRouter = new AudioRouter();
        public AudioRouter AudioRouter => audioRouter;

        private static readonly EffectChain effectChain = new EffectChain();
        public EffectChain EffectChain => effectChain;

        private static readonly AudioDataProvider audioDataProvider = new AudioDataProvider(effectChain);

        private static readonly IPlaybackService playbackService = 
            new PlaybackService(audioLoader, audioRouter, effectChain, audioDataProvider);

        private static readonly PlaybackController playbackController = 
            new PlaybackController(audioLoader, playbackService, audioRouter, effectChain);
        public PlaybackController PlaybackController => playbackController;

        private static readonly AudioEventDispatcher audioEventDispatcher = 
            new AudioEventDispatcher(audioLoader, playbackService, audioDataProvider);
        public AudioEventDispatcher AudioEventDispatcher => audioEventDispatcher;

        //private static readonly AudioPlayer audioPlayer = new AudioPlayer(audioRouter, effectChain);
        //public AudioPlayer AudioPlayer => audioPlayer;


        private AudioEngine()
        {
            
        }
    }
}
