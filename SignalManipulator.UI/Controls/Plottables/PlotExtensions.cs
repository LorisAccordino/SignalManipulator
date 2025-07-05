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
        public static WaveformPlot Waveform(this PlottableAdder adder, int sampleRate) => 
            Waveform(adder, sampleRate, "");
        public static WaveformPlot Waveform(this PlottableAdder adder, int sampleRate, string channelName)
        {
            WaveformPlot waveform = new WaveformPlot(sampleRate, channelName)
            {
                Color = adder.GetNextColor()
            };
            adder.Plot.PlottableList.Add(waveform);
            return waveform;
        }

        public static SpectrumPlot Spectrum(this PlottableAdder adder, int sampleRate, int fftSize) => 
            Spectrum(adder, sampleRate, fftSize, "");
        public static SpectrumPlot Spectrum(this PlottableAdder adder, int sampleRate, int fftSize, string channelName)
        {
            SpectrumPlot spectrum = new SpectrumPlot(sampleRate, fftSize, channelName)
            {
                Color = adder.GetNextColor()
            };
            adder.Plot.PlottableList.Add(spectrum);
            return spectrum;
        }

        public static LissajousPlot Lissajous(this PlottableAdder adder, int scatterSamples) =>
            Lissajous(adder, AudioEngine.SAMPLE_RATE, scatterSamples, "");
        public static LissajousPlot Lissajous(this PlottableAdder adder, int sampleRate, int scatterSamples) =>
            Lissajous(adder, sampleRate, scatterSamples, "");
        public static LissajousPlot Lissajous(this PlottableAdder adder, int sampleRate, int scatterSamples, string channelName)
        {
            LissajousPlot lissajous = new LissajousPlot(sampleRate, scatterSamples, channelName)
            {
                Color = adder.GetNextColor()
            };
            adder.Plot.PlottableList.Add(lissajous);
            return lissajous;
        }
    }
}