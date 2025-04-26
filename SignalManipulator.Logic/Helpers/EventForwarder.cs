using System;

namespace SignalManipulator.Logic.Helpers
{
    public static class EventForwarder
    {
        public static void Forward<T>(Action<Action<T>> subscribe, Action<T> forwardTo)
        {
            subscribe(data => forwardTo?.Invoke(data));
        }

        public static void Forward(Action<Action> subscribe, Action forwardTo)
        {
            subscribe(() => forwardTo?.Invoke());
        }
    }
}