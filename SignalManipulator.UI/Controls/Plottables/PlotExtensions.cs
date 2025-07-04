using ScottPlot;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.UI.Controls.Plottables
{
    [ExcludeFromCodeCoverage]
    public static class PlotExtensions
    {
        public static WaveformPlot Waveform(this PlottableAdder adder, int sampleRate) => Waveform(adder, sampleRate, "");
        public static WaveformPlot Waveform(this PlottableAdder adder, int sampleRate, string channelName)
        {
            WaveformPlot waveform = new WaveformPlot(sampleRate, channelName)
            {
                Color = adder.GetNextColor()
            };
            adder.Plot.PlottableList.Add(waveform);
            return waveform;
        }
    }
}