using NAudio.Wave;

namespace SignalManipulator.Logic.Core
{
    public class AudioEngine
    {
        // Instance
        private static readonly AudioEngine instance = new AudioEngine();
        public static AudioEngine Instance => instance;

        // Consts
        public const int CHUNK_SIZE = 512;
        public const int SAMPLE_RATE = 44100; // 44.1 kHz
        public const int CHANNELS = 2; // Stereo Audio
        public const int TARGET_FPS = 30;
        public static readonly WaveFormat WAVE_FORMAT = WaveFormat.CreateIeeeFloatWaveFormat(SAMPLE_RATE, CHANNELS);


        // Modules references
        private static readonly EffectChain effectChain = new EffectChain(Instance);
        public EffectChain EffectChain => effectChain;

        private static readonly AudioPlayer audioPlayer = new AudioPlayer(Instance);
        public AudioPlayer AudioPlayer => audioPlayer;

        private static readonly AudioRouter audioRouter = new AudioRouter(Instance);
        public AudioRouter AudioRouter => audioRouter;

        private AudioEngine()
        {
            
        }
    }
}
