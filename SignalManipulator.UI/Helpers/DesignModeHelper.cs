using System.ComponentModel;

namespace SignalManipulator.UI.Helpers
{
    public static class DesignModeHelper
    {
        public static bool IsDesignMode { get => LicenseManager.UsageMode == LicenseUsageMode.Designtime; }
    }
}