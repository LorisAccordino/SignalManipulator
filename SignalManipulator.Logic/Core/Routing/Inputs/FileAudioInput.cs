using NAudio.Wave;
using SignalManipulator.Logic.AudioMath;
using SignalManipulator.Logic.Info;
using System.Diagnostics;
using System.Windows.Forms;

namespace SignalManipulator.Logic.Core.Routing.Inputs
{
    public class FileAudioInput : ISeekableAudioInput, ILoadableAudioInput, IInfoProviderAudioInput
    {
        private ExtendedWaveFileReader? reader;

        public event EventHandler? Ready;
        public bool IsReady { get; private set; } = false;

        public event EventHandler<byte[]>? OnBytes;
        public bool IsRealTime => false;

        public WaveFormat WaveFormat => reader?.WaveFormat ?? AudioEngine.WAVE_FORMAT;
        public AudioInfo Info => reader?.Info ?? AudioInfo.Default;

        public async void Load(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            var converter = new WaveConverter();

            converter.ProgressChanged += (s, progress) => Debug.WriteLine($"Progress: {progress:P1}");

            converter.ConversionFailed += (s, ex) => MessageBox.Show($"Error during audio loading: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            var outputPath = await converter.ConvertToWavAsync(filePath);
            if (outputPath is not null)
            {
                reader = new ExtendedWaveFileReader(outputPath);
                IsReady = true;
                Ready?.Invoke(this, EventArgs.Empty);
            }
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            int read = reader?.Read(buffer, offset, count) ?? 0;
            if (read > 0) OnBytes?.Invoke(this, buffer[..read]);
            return read;
        }

        public int Read(float[] samples, int offset, int count)
        {
            byte[] buffer = new byte[count * 4];
            int read = Read(buffer, 0, count * 4);
            buffer.CopyToFloats(samples, offset, read / 4);
            return read / 4;
        }

        public void Start() { }  // Nop
        public void Stop() { }   // Nop

        public void Seek(TimeSpan position)
        {
            if (reader != null && reader.CanSeek)
            {
                var safePosition = position.Clamp(TimeSpan.Zero, reader.TotalTime);
                reader.CurrentTime = safePosition;
            }
        }

        public void Dispose() => reader?.Dispose();
    }
}