using NAudio.Wave;
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
        public const int TARGET_FPS = 30;
        public static readonly WaveFormat DEFAULT_WAVE_FORMAT = WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);
        //public WaveFormat WAVE_FORMAT => AudioPlayer.WaveFormat;


        // Modules references
        private static readonly EffectChain effectChain = new EffectChain(Instance);
        public EffectChain EffectChain => effectChain;

        private static readonly AudioPlayer audioPlayer = new AudioPlayer(Instance);
        public AudioPlayer AudioPlayer => audioPlayer;

        private static readonly AudioRouter audioRouter = new AudioRouter(Instance);
        public AudioRouter AudioRouter => audioRouter;


        private static readonly WaveformViewer waveformViewer = new WaveformViewer();
        public WaveformViewer WaveformViewer => waveformViewer;
        private static readonly SpectrumViewer spectrumViewer = new SpectrumViewer();
        public SpectrumViewer SpectrumViewer => spectrumViewer;

        private AudioEngine()
        {
            
        }
    }
}
