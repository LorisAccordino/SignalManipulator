using SignalManipulator.UI.Helpers;
using System.ComponentModel;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls
{
    public class FloatableControl : UserControl, IFloatableControl
    {
        private bool isFloating;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsFloating { get => isFloating; set => isFloating = value; }

        public void Float()
        {
            if (isFloating) return;
            isFloating = true;
            this.FloatControl(() => isFloating = false);
        }

        public FloatableControl()
        {
            this.AttachContextMenu(("Undock", Float));
        }
    }
}