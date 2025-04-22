using SignalManipulator.Logic.Core.Buffering;
using SignalManipulator.Logic.Core.Sourcing;
using SignalManipulator.Logic.Effects;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SignalManipulator.Logic.Core.Playback
{
    public class PlaybackService : IPlaybackService
    {
        private readonly IAudioSource source;
        private readonly IBufferManager buffer;
        private readonly EffectChain effects;
        private CancellationTokenSource cts;
        private TimeStretchEffect timeStrech;

        public double Speed { get => timeStrech.Speed; set => timeStrech.Speed = value; }
        public bool PreservePitch { get => timeStrech.PreservePitch; set => timeStrech.PreservePitch = value; }

        public event EventHandler<byte[]> OnDataAvailable;
        public event EventHandler OnFinished;

        public PlaybackService(IAudioSource source, IBufferManager buffer, EffectChain effects)
        {
            this.source = source;
            this.buffer = buffer;
            this.effects = effects;
            this.effects.AddEffect<TimeStretchEffect>();
            timeStrech = effects.GetEffect<TimeStretchEffect>(0);
        }

        public void Start()
        {
            cts = new CancellationTokenSource();
            Task.Run(() => PlaybackLoop(cts.Token), cts.Token);
        }

        private async Task PlaybackLoop(CancellationToken ct)
        {
            var buffer = new byte[AudioEngine.CHUNK_SIZE];
            while (!ct.IsCancellationRequested)
            {
                //int read = effects.Output.Read(buffer, 0, buffer.Length);
                int read = effects.EffectList.Last().Read(buffer, 0, buffer.Length);
                if (read == 0) break;
                this.buffer.AddSamples(buffer, 0, read);
                OnDataAvailable?.Invoke(this, buffer.ToArray());
                while (this.buffer.IsFull(AudioEngine.CHUNK_SIZE) && !ct.IsCancellationRequested)
                    await Task.Delay(10, ct);
            }
            OnFinished?.Invoke(this, EventArgs.Empty);
        }

        public void Pause() => cts.Cancel();
        public void Stop()
        {
            cts.Cancel();
            buffer.Clear();
            source.Seek(TimeSpan.Zero);
        }
    }
}