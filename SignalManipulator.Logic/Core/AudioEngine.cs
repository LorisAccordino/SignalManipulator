using NAudio.Wave;

namespace SignalManipulator.Logic.Core
{
    public class AudioEngine
    {
        // Instance
        public static AudioEngine Instance => new AudioEngine();

        // Consts
        public const int CHUNK_SIZE = 512;
        public const int SAMPLE_RATE = 44100; // 44.1 kHz
        public const int CHANNELS = 2; // Stereo Audio
        public const int TARGET_FPS = 30;
        public static readonly WaveFormat WAVE_FORMAT = new WaveFormat(SAMPLE_RATE, CHANNELS);


        // Modules references
        public AudioPlayer AudioPlayer => new AudioPlayer(Instance);
        public AudioRouter AudioRouter => new AudioRouter(Instance);

        private AudioEngine()
        {
            
        }
    }
}
