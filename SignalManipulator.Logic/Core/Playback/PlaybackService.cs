using NAudio.Wave;
using SignalManipulator.Logic.Core.Sourcing;
using SignalManipulator.Logic.Effects;
using SignalManipulator.Logic.Providers;
using SignalManipulator.Logic.Utils;
using System;

namespace SignalManipulator.Logic.Core.Playback
{
    public class PlaybackService : IPlaybackService
    {
        private readonly IAudioSource source;
        //private readonly IBufferManager buffer;
        private readonly EffectChain effects;
        private readonly AudioRouter router;
        //private readonly TapSampleProvider tap;
        private readonly TapProvider tap;
        private readonly System.Timers.Timer updateTimer;

        //private CancellationTokenSource cts;
        private TimeStretchEffect timeStrech;

        public double Speed { get => timeStrech.Speed; set => timeStrech.Speed = value; }
        public bool PreservePitch { get => timeStrech.PreservePitch; set => timeStrech.PreservePitch = value; }

        public event Action OnUpdate;
        public event Action<float[]> OnDataAvailable;
        public event EventHandler OnFinished;

        //public PlaybackService(IAudioSource source, IBufferManager buffer, EffectChain effects)
        public PlaybackService(IAudioSource source, EffectChain effects, AudioRouter router)
        {
            this.source = source;
            //this.buffer = buffer;
            this.effects = effects;
            this.router = router;

            this.effects.AddEffect<TimeStretchEffect>();
            timeStrech = effects.GetEffect<TimeStretchEffect>(0);

            // Insert a Tap to catch the waveform
            //tap = new TapSampleProvider(effects);
            //tap = new TapSampleProvider(source.SourceProvider);
            tap = new TapProvider(effects);
            tap.OnSamples += (samples) => OnDataAvailable?.Invoke(samples);
            //tap.OnBytes += (bytes) => OnDataAvailable?.Invoke(bytes.AsFloats());

            // Initialize the audio
            router.InitOutputs(tap as IWaveProvider);

            updateTimer = new System.Timers.Timer(1000.0 / AudioEngine.TARGET_FPS);
            updateTimer.Elapsed += (s, e) => OnUpdate?.Invoke();
        }

        /*public void Start()
        {
            //cts = new CancellationTokenSource();
            //Task.Run(() => PlaybackLoop(cts.Token), cts.Token);
            //updateTimer.Start();
        }*/

        public void Play()
        {
            updateTimer.Start();
            router.CurrentDevice.Play();
        }

        /*
        private async Task PlaybackLoop(CancellationToken ct)
        {
            var buffer = new byte[AudioEngine.CHUNK_SIZE];
            while (!ct.IsCancellationRequested)
            {
                //int read = effects.Output.Read(buffer, 0, buffer.Length);
                //int read = effects.EffectList.Last().Read(buffer, 0, buffer.Length);
                int read = source.SourceProvider.Read(buffer, 0, buffer.Length);
                if (read == 0) break;
                this.buffer.AddSamples(buffer, 0, read);
                //OnDataAvailable?.Invoke(this, buffer.ToArray());
                while (this.buffer.IsFull() && !ct.IsCancellationRequested)
                    await Task.Delay(10, ct);
            }
            OnFinished?.Invoke(this, EventArgs.Empty);
        }
        */

        public void Pause() => router.CurrentDevice.Pause();
        public void Stop()
        {
            router.CurrentDevice.Stop();
            updateTimer.Stop();
            //cts.Cancel();
            //buffer.Clear();
            source.Seek(TimeSpan.Zero);
        }
    }
}