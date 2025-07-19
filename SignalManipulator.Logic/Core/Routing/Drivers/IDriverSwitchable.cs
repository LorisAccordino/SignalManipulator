namespace SignalManipulator.Logic.Core.Routing.Drivers
{
    public interface IDriverSwitchable
    {
        void SetDriver(AudioDriverType driver);
        AudioDriverType GetDriver();
    }
}