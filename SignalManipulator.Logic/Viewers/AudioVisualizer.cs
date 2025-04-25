using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Utils;
using System;

namespace SignalManipulator.Logic.Viewers
{
    public class AudioVisualizer
    {
        private AudioEngine audioEngine = AudioEngine.Instance;

        public event Action OnUpdate;
        public event Action<AudioFrame> OnFrameAvailable;
        public event Action<FrequencySpectrum> OnSpectrumAvailable;
        public event Action OnStopped;
        public event Action OnStarted;

        public int SampleRate => audioEngine.AudioPlayer.WaveFormat?.SampleRate ?? AudioEngine.DEFAULT_WAVE_FORMAT.SampleRate;

        public bool EnableSpectrum { get; set; } = false;

        public AudioVisualizer()
        {
            audioEngine.AudioPlayer.OnLoad += (s, e) => OnStarted?.Invoke();
            audioEngine.AudioPlayer.OnStopped += (s, e) => OnStopped?.Invoke();
            audioEngine.AudioPlayer.OnUpdate += () => OnUpdate?.Invoke();

            audioEngine.AudioPlayer.OnDataAvailable += (stereoSamples) =>
            {
                AudioFrame frame = new AudioFrame(stereoSamples);
                OnFrameAvailable?.Invoke(frame);

                if (EnableSpectrum)
                {
                    double[] freqs, magnitudes;
                    magnitudes = AudioMathHelper.CalculateFFT(frame.DoubleStereo, SampleRate, out freqs);
                    OnSpectrumAvailable?.Invoke(new FrequencySpectrum(freqs, magnitudes));
                }
            };
        }
    }
}