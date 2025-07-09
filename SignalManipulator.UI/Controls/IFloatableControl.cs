namespace SignalManipulator.UI.Controls
{
    public interface IFloatableControl
    {
        bool IsFloating { get; set; }
        void Float();
    }
}