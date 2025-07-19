namespace SignalManipulator.Logic.Core.Routing
{
    public interface IDriverSwitchable
    {
        void SetDriver(AudioDriverType driver);
        AudioDriverType GetDriver();
    }
}