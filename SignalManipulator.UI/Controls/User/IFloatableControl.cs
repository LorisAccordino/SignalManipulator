namespace SignalManipulator.UI.Controls.User
{
    public interface IFloatableControl
    {
        bool IsFloating { get; set; }
        void Float();
    }
}