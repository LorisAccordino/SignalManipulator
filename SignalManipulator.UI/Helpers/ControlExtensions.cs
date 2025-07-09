using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace SignalManipulator.UI.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class ControlExtensions
    {
        public static void SafeInvoke(this Control control, Action action)
        {
            if (control.InvokeRequired)
                control.Invoke(action);
            else
                action();
        }

        public static void SafeAsyncInvoke(this Control control, Action action)
        {
            if (control.InvokeRequired)
                control.BeginInvoke(action);
            else
                action();
        }

        public static void FloatControl(this Control control)
        {
            if (control == null || control.Parent == null)
                throw new ArgumentException("Control must have a parent.");

            // Save a reference to the original parent and position
            Control originalParent = control.Parent;
            int originalIndex = originalParent.Controls.GetChildIndex(control);

            // Remove from the parent container
            originalParent.Controls.Remove(control);

            // Create a new form
            Form floatForm = new Form
            {
                Text = control.Text != "" ? control.Text : control.GetType().Name,
                FormBorderStyle = FormBorderStyle.SizableToolWindow,
                StartPosition = FormStartPosition.Manual,
                Size = control.Size
            };

            // Position the new window nearby the main form
            if (originalParent.FindForm() is Form mainForm)
            {
                floatForm.Location = new System.Drawing.Point(mainForm.Right + 10, mainForm.Top);
            }

            // Hook again when the window is closed
            floatForm.FormClosed += (s, e) =>
            {
                floatForm.Controls.Remove(control);
                originalParent.Controls.Add(control);
                originalParent.Controls.SetChildIndex(control, originalIndex);
            };

            // Add the control to the new form
            control.Dock = DockStyle.Fill;
            floatForm.Controls.Add(control);
            floatForm.Show();
        }

        public static void AttachContextMenu(this Control control, params (string text, Action action)[] items)
        {
            var menu = new ContextMenuStrip();
            foreach (var (text, action) in items)
            {
                var item = new ToolStripMenuItem(text);
                item.Click += (s, e) => action();
                menu.Items.Add(item);
            }
            control.ContextMenuStrip = menu;
        }

    }
}