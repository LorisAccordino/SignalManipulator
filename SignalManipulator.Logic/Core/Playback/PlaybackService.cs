using NAudio.Wave;
using SignalManipulator.Logic.Core.Routing;
using SignalManipulator.Logic.Core.Routing.Inputs;
using SignalManipulator.Logic.Data;
using SignalManipulator.Logic.Info;

namespace SignalManipulator.Logic.Core.Playback
{
    public class PlaybackService : ISampleProvider
    {
        // References
        private readonly IAudioInput input;
        private readonly AudioRouter router;
        private readonly PlaybackModifiers modifiers = new PlaybackModifiers();

        // Properties
        public WaveFormat WaveFormat => Info.Technical.WaveFormat;
        public ISampleProvider SampleProvider => modifiers;
        public AudioInfo Info
        {
            get
            {
                if (input is IInfoProviderAudioInput infoProvider)
                    return infoProvider.Info;
                throw new NotSupportedException("This input does not expose AudioInfo.");
            }
        }
        public double Speed { get => modifiers.Speed; set => modifiers.Speed = value; }
        public bool PreservePitch { get => modifiers.PreservePitch; set => modifiers.PreservePitch = value; }
        public double Volume { get => modifiers.Volume; set => modifiers.Volume = value; }


        // Events
        public event EventHandler<AudioInfo>? LoadCompleted;
        public event EventHandler<AnalyzedAudioSlice>? AudioDataReady;

        public PlaybackService(IAudioInput input, AudioRouter router)
        {
            this.input = input;
            this.router = router;

            input.Ready += OnInputReady;
            router.PlaybackStopped += (s, e) => OnStopped();
        }

        private void OnInputReady(object? sender, EventArgs e)
        {
            modifiers.SetSource(input);
            //Stop(); // Ensure stopped state
            LoadCompleted?.Invoke(this, Info);
        }

        public void Load(string path)
        {
            if (input is ILoadableAudioInput loadable)
                loadable.Load(path);
            else
                throw new NotSupportedException("This input cannot be loaded from a file.");
        }

        public void Play() => router.Play();
        public void Pause() => router.Pause();
        public void Stop() => router.Stop();

        private void OnStopped()
        {
            Seek(TimeSpan.Zero);
            modifiers.Reset();
        }

        public void Seek(TimeSpan position)
        {
            if (input is ISeekableAudioInput seekable)
                seekable.Seek(position);
            else
                throw new NotSupportedException("This input cannot be seeked.");
        }

        public int Read(float[] samples, int offset, int count) => modifiers.Read(samples, offset, count);
    }
}