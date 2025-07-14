namespace SignalManipulator.Logic.Data
{
    public class CircularBuffer<T>
    {
        public bool IsFull => queue.Count >= Capacity;

        private readonly Queue<T> queue = new();
        private int capacity;

        public int Capacity
        {
            get => capacity;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Capacity must be greater than 0.");

                capacity = value;

                // If the new capacity is smaller than the current items, remove the oldest
                while (queue.Count > capacity) queue.Dequeue();
            }
        }

        public CircularBuffer(int capacity)
        {
            Capacity = capacity;
        }

        public void Add(T item)
        {
            if (IsFull)
                queue.Dequeue(); // Remove oldest
            queue.Enqueue(item);
        }

        public void AddRange(T[] items)
        {
            foreach (var item in items)
                Add(item);
        }

        public T[] ToArray() => queue.ToArray();

        public void Clear() => queue.Clear();

        public void CopyTo(T[] array, int arrayIndex)
        {
            queue.CopyTo(array, arrayIndex);
        }
    }

}
