using System;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    public static class ControlExtensions
    {
        public static void SafeInvoke(this Control control, Action action)
        {
            if (control.InvokeRequired)
                control.Invoke(action);
            else
                action();
        }
    }
}
