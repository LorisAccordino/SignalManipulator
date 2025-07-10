using System;

namespace SignalManipulator.UI.Misc
{
    public class AxisNavigator
    {
        public double Zoom { get; private set; } = 1.0;
        public double Pan { get; private set; } = 0.0;

        public double Start { get; private set; } = 0.0;
        public double End { get; private set; } = 0.0;

        public int Capacity { get; private set; } = 1;
        public bool NeedsUpdate { get; private set; }

        public AxisNavigator() : this(0) { }
        public AxisNavigator(int capacity)
        {
            SetCapacity(capacity);
        }

        public void SetCapacity(int capacity)
        {
            Capacity = Math.Max(1, capacity);
            MarkDirty();
        }

        public void SetZoom(double zoom)
        {
            Zoom = Math.Max(0.001, zoom); // Avoid divide by zero
            MarkDirty();
        }

        public void SetPan(double pan)
        {
            Pan = Math.Max(-1, Math.Min(1, pan)); // Clamp to [-1, 1]
            MarkDirty();
        }

        public void Recalculate()
        {
            int view = (int)(Capacity / Zoom);
            double panNorm = (Pan + 1) / 2.0;
            Start = panNorm * (Capacity - view);
            End = Start + view;
            NeedsUpdate = false;
        }

        private void MarkDirty()
        {
            NeedsUpdate = true;
        }
    }
}