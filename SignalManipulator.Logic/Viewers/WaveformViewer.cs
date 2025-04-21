using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Helpers;
using System;

namespace SignalManipulator.Logic.Viewers
{
    public class WaveformViewer
    {
        private AudioEngine audioEngine = AudioEngine.Instance;

        public event Action OnUpdate;
        public event Action<double[]> OnWaveformUpdated;
        public int SampleRate => audioEngine.AudioPlayer.WaveFormat.SampleRate;

        public WaveformViewer()
        {
            audioEngine.AudioPlayer.OnUpdate += (s, e) => OnUpdate();
            audioEngine.AudioPlayer.OnUpdateData += (s, buffer) =>
            {
                var waveform = AudioHelper.ConvertPcmToDouble(buffer, audioEngine.AudioPlayer.WaveFormat);
                OnWaveformUpdated?.Invoke(waveform);
            };
        }
    }
}