using SignalManipulator.UI.Helpers;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    public class FloatableControl : UserControl, IFloatableControl
    {
        private bool isFloating;
        public bool IsFloating { get => isFloating; set => IsFloating = value; }

        public void Float()
        {
            isFloating = true;
            this.FloatControl(() => isFloating = false);
        }

        public FloatableControl()
        {
            this.AttachFloatContextMenu();
        }
    }
}