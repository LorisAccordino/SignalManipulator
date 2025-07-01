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
        private readonly List<Action> subscribers = new();
        private readonly ConcurrentQueue<Action> oneShotQueue = new();

        private readonly List<Action> onStartActions = new();
        private readonly List<Action> onStopActions = new();

        private bool isTimerRunning = false;

        private UIUpdateService()
        {
            timer = new Timer { Interval = AudioEngine.TARGET_FPS };
            timer.Tick += (s, e) => Update();
        }

        private void Update()
        {
            // One-shot queue
            while (oneShotQueue.TryDequeue(out var action))
            {
                try { action(); }
                catch (Exception ex) { Console.WriteLine($"[UIUpdateService] One-shot failed: {ex}"); }
            }

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


        public void SetFPS(int fps)
        {
            timer.Interval = (int)(1000.0 / fps);
        }

        public void Start()
        {
            if (isTimerRunning) return;

            foreach (var action in onStartActions)
            {
                try { action(); }
                catch (Exception ex) { Console.WriteLine($"[UIUpdateService] OnStart failed: {ex}"); }
            }

            ForceUpdate();
            timer.Start();
            isTimerRunning = true;
        }

        public void Stop()
        {
            if (!isTimerRunning) return;

            foreach (var action in onStopActions)
            {
                try { action(); }
                catch (Exception ex) { Console.WriteLine($"[UIUpdateService] OnStop failed: {ex}"); }
            }

            timer.Stop();
            ForceUpdate();

            isTimerRunning = false;
        }

        public void ForceStart() { isTimerRunning = false; Start(); }
        public void ForceUpdate() => Update();
        public void ForceStop() { isTimerRunning = true; Stop(); }


        // Extra: Register actions for lifecycle hooks
        public void RegisterOnStart(Action action)
        {
            if (action != null && !onStartActions.Contains(action))
                onStartActions.Add(action);
        }

        public void RegisterOnStop(Action action)
        {
            if (action != null && !onStopActions.Contains(action))
                onStopActions.Add(action);
        }

        public void ClearHooks()
        {
            onStartActions.Clear();
            onStopActions.Clear();
        }
    }
}