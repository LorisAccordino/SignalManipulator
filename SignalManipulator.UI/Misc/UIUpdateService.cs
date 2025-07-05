using SignalManipulator.Logic.Core;
using System.Collections.Concurrent;
using Timer = System.Windows.Forms.Timer;

namespace SignalManipulator.UI.Misc
{
    public class UIUpdateService
    {
        private static UIUpdateService instance;
        public static UIUpdateService Instance
        {
            get
            {
                if (instance == null)
                    instance = new UIUpdateService();
                return instance;
            }
        }

        private readonly Timer timer;
        private readonly Timer wakeOnTimer;
        private readonly List<Action> subscribers = new();
        private readonly ConcurrentQueue<Action> oneShotQueue = new();

        private UIUpdateService()
        {
            timer = new Timer { Interval = AudioEngine.TARGET_FPS };
            wakeOnTimer = new Timer() { Interval = AudioEngine.TARGET_FPS };
            timer.Tick += (s, e) => Update();
            wakeOnTimer.Tick += (s, e) => WakeOnUpdate();
            wakeOnTimer.Start();
        }

        private void WakeOnUpdate()
        {
            // One-shot queue
            while (oneShotQueue.TryDequeue(out var action))
            {
                try { action(); }
                catch (Exception ex) { Console.WriteLine($"[UIUpdateService] One-shot failed: {ex}"); }
            }
        }

        private void Update()
        {
            // Periodic subscribers
            foreach (var cb in subscribers)
            {
                try { cb(); }
                catch (Exception ex) { Console.WriteLine($"[UIUpdateService] Subscriber failed: {ex}"); }
            }
        }

        public void Register(Action updateCallback)
        {
            if (!subscribers.Contains(updateCallback))
                subscribers.Add(updateCallback);
        }

        public void Unregister(Action updateCallback)
        {
            subscribers.Remove(updateCallback);
        }

        public void Enqueue(Action oneShotAction)
        {
            if (oneShotAction != null)
                oneShotQueue.Enqueue(oneShotAction);
        }

        public void SetFPS(int fps) => timer.Interval = (int)(1000.0 / fps);
        public void Start() => timer.Start();
        public void Stop() => timer.Stop();
        public void ForceUpdate() => Update();
    }
}