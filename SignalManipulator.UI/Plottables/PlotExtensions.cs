using ScottPlot;

namespace SignalManipulator.UI.Plottables
{
    public static class PlotExtensions
    {
        public static void HideGridAndTicks(this Plot plot)
        {
            plot.Axes.Left.TickGenerator = new ScottPlot.TickGenerators.EmptyTickGenerator();
            plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.EmptyTickGenerator();
            plot.Axes.Right.TickGenerator = new ScottPlot.TickGenerators.EmptyTickGenerator();
            plot.Axes.Top.TickGenerator = new ScottPlot.TickGenerators.EmptyTickGenerator();
        }
    }
}