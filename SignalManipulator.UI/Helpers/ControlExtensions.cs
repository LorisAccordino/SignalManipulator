using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;

namespace SignalManipulator.UI.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class ControlExtensions
    {
        private static readonly Size SafeBorder = new Size(50, 50);

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

        public static void AttachContextMenu(this Control control, params (string text, Action action)[] items)
        {
            if (control.ContextMenuStrip == null)
                control.ContextMenuStrip = new ContextMenuStrip();

            foreach (var (text, action) in items)
            {
                // Avoid duplications: check if an item with the same text already exists
                if (!control.ContextMenuStrip.Items.Cast<ToolStripItem>().Any(i => i.Text == text))
                {
                    var item = new ToolStripMenuItem(text);
                    item.Click += (s, e) => action();
                    control.ContextMenuStrip.Items.Add(item);
                }
            }
        }

        public static void FloatControl(this Control control, Action? onDocked = null)
        {
            if (control == null || control.Parent == null)
                throw new ArgumentException("Control must have a parent.");

            // Save a reference to the original parent and position
            Control originalParent = control.Parent;
            int originalIndex = originalParent.Controls.GetChildIndex(control);

            // Remove from the parent container
            originalParent.Controls.Remove(control);

            // Create a new form
            var baseSize = control.MinimumSize + SafeBorder;
            Form floatForm = new Form
            {
                Text = !string.IsNullOrWhiteSpace(control.Text) ? control.Text : control.GetType().Name,
                FormBorderStyle = FormBorderStyle.SizableToolWindow,
                StartPosition = FormStartPosition.Manual,
                MinimumSize = baseSize,
                Size = baseSize,
            };

            // Position the new window nearby the main form
            if (originalParent.FindForm() is Form mainForm)
                floatForm.Location = new Point(mainForm.Left, mainForm.Top) + SafeBorder;
            else
                floatForm.StartPosition = FormStartPosition.CenterParent;

            // Hook again when the window is closed
            floatForm.FormClosed += (s, e) =>
            {
                // Remove control from the float form
                floatForm.Controls.Remove(control);

                // Add control back to original parent container
                originalParent.Controls.Add(control);
                originalParent.Controls.SetChildIndex(control, originalIndex);

                onDocked?.Invoke();
            };

            // Add the control to the new form
            control.Dock = DockStyle.Fill;
            floatForm.Controls.Add(control);
            floatForm.Show();
        }
    }
}