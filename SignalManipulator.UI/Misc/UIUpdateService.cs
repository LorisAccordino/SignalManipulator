using SignalManipulator.Logic.Core;
using System.Collections.Concurrent;
using System.Windows.Forms;

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

        private readonly System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer? delayedStopTimer = null;
        private bool isDelayedStopScheduled = false;

        private readonly List<Action> subscribers = new List<Action>();
        private readonly ConcurrentQueue<Action> oneShotQueue = new ConcurrentQueue<Action>();

        private UIUpdateService()
        {
            timer = new System.Windows.Forms.Timer { Interval = AudioEngine.TARGET_FPS };
            timer.Tick += (s, e) => Update();
        }

        private void Update()
        {
            // Execute one-shot queued action
            while (oneShotQueue.TryDequeue(out var action))
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    // Log errors
                    Console.WriteLine($"[UIUpdateService] One-shot action failed: {ex}");
                }
            }

            // Execute every normal subscriber
            foreach (var cb in subscribers)
            {
                try
                {
                    cb();
                }
                catch (Exception ex)
                {
                    // Log errors
                    Console.WriteLine($"[UIUpdateService] Subscriber failed: {ex}");
                }
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

        public void Start() => timer.Start();

        public void Stop()
        {
            timer.Stop();
            ForceUpdate();
            isDelayedStopScheduled = false;
        }

        public void ForceUpdate() => Update();
    }
}