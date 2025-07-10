using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.UI.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class DesignModeHelper
    {
        public static bool IsDesignMode => LicenseManager.UsageMode == LicenseUsageMode.Designtime;
    }
}