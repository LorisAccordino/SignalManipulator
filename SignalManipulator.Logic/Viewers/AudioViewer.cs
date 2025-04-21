using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Utils;
using System;

namespace SignalManipulator.Logic.Viewers
{
    public class AudioViewer
    {
        private AudioEngine audioEngine = AudioEngine.Instance;

        public event Action OnUpdate;
        public event Action<double[]> OnWaveformUpdated;
        public event Action<FrequencySpectrum> OnSpectrumUpdated;
        public event Action OnStopped;
        public event Action OnStarted;

        public int SampleRate => audioEngine.AudioPlayer.WaveFormat.SampleRate;

        public bool EnableSpectrum { get; set; } = false;

        public AudioViewer()
        {
            audioEngine.AudioPlayer.OnLoad += (s, e) => OnStarted?.Invoke();
            audioEngine.AudioPlayer.OnStopped += (s, e) => OnStopped?.Invoke();
            audioEngine.AudioPlayer.OnUpdate += (s, e) => OnUpdate?.Invoke();

            audioEngine.AudioPlayer.OnUpdateData += (s, buffer) =>
            {
                double[] waveform = AudioMathHelper.ConvertPcmToDouble(buffer, audioEngine.AudioPlayer.WaveFormat);

                OnWaveformUpdated?.Invoke(waveform);

                if (EnableSpectrum)
                {
                    double[] freqs, magnitudes;
                    magnitudes = AudioMathHelper.CalculateFFT(waveform, SampleRate, out freqs);
                    OnSpectrumUpdated?.Invoke(new FrequencySpectrum(freqs, magnitudes));
                }
            };
        }
    }
}