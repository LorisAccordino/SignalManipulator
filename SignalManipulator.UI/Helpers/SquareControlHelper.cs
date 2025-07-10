using System.Drawing;
using System.Windows.Forms;

namespace SignalManipulator.UI.Helpers
{
    public static class SquareControlHelper
    {
        public static void Attach(Control parent, Control target)
        {
            void ResizeHandler(object? sender, EventArgs e)
            {
                int size = Math.Min(parent.ClientSize.Width, parent.ClientSize.Height);

                // Optional: inner margin
                const int margin = 0;
                size = Math.Max(0, size - margin);

                target.Size = new Size(size, size);
                target.Location = new Point(
                    (parent.ClientSize.Width - size) / 2,
                    (parent.ClientSize.Height - size) / 2
                );
            }

            // Bind the resize
            parent.Resize += ResizeHandler;

            // Call it to force the inital layout
            ResizeHandler(null, EventArgs.Empty);
        }
    }
}