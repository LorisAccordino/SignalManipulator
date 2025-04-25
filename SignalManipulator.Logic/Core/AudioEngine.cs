using NAudio.Wave;
using SignalManipulator.Logic.Core.Playback;
using SignalManipulator.Logic.Viewers;

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
        public const int TARGET_FPS = 20;
        public static readonly WaveFormat DEFAULT_WAVE_FORMAT = WaveFormat.CreateIeeeFloatWaveFormat(SAMPLE_RATE, CHANNELS);
        //public WaveFormat WAVE_FORMAT => AudioPlayer.WaveFormat;


        // Modules references
        private static readonly AudioRouter audioRouter = new AudioRouter();
        public AudioRouter AudioRouter => audioRouter;

        private static readonly EffectChain effectChain = new EffectChain(Instance);
        public EffectChain EffectChain => effectChain;

        private static readonly AudioPlayer audioPlayer = new AudioPlayer(audioRouter, effectChain);
        public AudioPlayer AudioPlayer => audioPlayer;

        private static readonly AudioVisualizer audioViewer = new AudioVisualizer();
        public AudioVisualizer AudioViewer => audioViewer;

        private AudioEngine()
        {
            
        }
    }
}
