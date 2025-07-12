namespace SignalManipulator.Controls
{
    public interface IFloatableControl
    {
        bool IsFloating { get; set; }
        void Float();
    }
}