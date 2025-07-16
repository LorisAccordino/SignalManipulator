namespace SignalManipulator.Logic.AudioMath
{
    public static class MiscExtensions
    {
        public static TimeSpan Clamp(this TimeSpan value, TimeSpan min, TimeSpan max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}