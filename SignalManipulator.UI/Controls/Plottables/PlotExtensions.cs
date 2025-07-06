using ScottPlot;
using SignalManipulator.Logic.Core;
using SignalManipulator.UI.Controls.Plottables.Scatters;
using SignalManipulator.UI.Controls.Plottables.Signals;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.UI.Controls.Plottables
{
    [ExcludeFromCodeCoverage]
    public static class PlotExtensions
    {
        public static Waveform Waveform(this PlottableAdder adder, int sampleRate) => 
            Waveform(adder, sampleRate, "");
        public static Waveform Waveform(this PlottableAdder adder, int sampleRate, string channelName)
        {
            Waveform waveform = new Waveform(sampleRate, channelName)
            {
                Color = adder.GetNextColor()
            };
            adder.Plot.PlottableList.Add(waveform);
            return waveform;
        }

        public static Spectrum Spectrum(this PlottableAdder adder, int sampleRate, int fftSize) => 
            Spectrum(adder, sampleRate, fftSize, "");
        public static Spectrum Spectrum(this PlottableAdder adder, int sampleRate, int fftSize, string channelName)
        {
            Spectrum spectrum = new Spectrum(sampleRate, fftSize, channelName)
            {
                Color = adder.GetNextColor()
            };
            adder.Plot.PlottableList.Add(spectrum);
            return spectrum;
        }

        public static Lissajous Lissajous(this PlottableAdder adder, int scatterSamples) =>
            Lissajous(adder, scatterSamples, "");
        public static Lissajous Lissajous(this PlottableAdder adder, int scatterSamples, string channelName)
        {
            Lissajous lissajous = new Lissajous(scatterSamples, channelName)
            {
                Color = adder.GetNextColor()
            };
            adder.Plot.PlottableList.Add(lissajous);
            return lissajous;
        }
    }
}