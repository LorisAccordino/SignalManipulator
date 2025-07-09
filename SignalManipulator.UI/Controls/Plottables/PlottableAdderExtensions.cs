using ScottPlot;
using SignalManipulator.UI.Controls.Plottables.Radars;
using SignalManipulator.UI.Controls.Plottables.Scatters;
using SignalManipulator.UI.Controls.Plottables.Signals;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.UI.Controls.Plottables
{
    [ExcludeFromCodeCoverage]
    public static class PlottableAdderExtensions
    {
        public static Waveform Waveform(this PlottableAdder adder, int sampleRate, string channelName = "")
        {
            Waveform waveform = new Waveform(sampleRate, channelName)
            {
                Color = adder.GetNextColor()
            };
            adder.Plot.PlottableList.Add(waveform);
            return waveform;
        }

        public static Spectrum Spectrum(this PlottableAdder adder, int sampleRate, int fftSize, string channelName = "")
        {
            Spectrum spectrum = new Spectrum(sampleRate, fftSize, channelName)
            {
                Color = adder.GetNextColor()
            };
            adder.Plot.PlottableList.Add(spectrum);
            return spectrum;
        }

        public static Lissajous Lissajous(this PlottableAdder adder, int scatterSamples, string channelName = "")
        {
            Lissajous lissajous = new Lissajous(scatterSamples, channelName)
            {
                Color = adder.GetNextColor()
            };
            adder.Plot.PlottableList.Add(lissajous);
            return lissajous;
        }

        public static CardioidRadar CardioidRadar(this PlottableAdder adder, int cardioidCount) 
            => CardioidRadar(adder, new string[cardioidCount]);
        public static CardioidRadar CardioidRadar(this PlottableAdder adder, string[] labels)
        {
            CardioidRadar radar = new CardioidRadar(labels);
            radar.Color = adder.GetNextColor();
            radar.PolarAxis.SetCircles(1.0, 4);
            radar.PolarAxis.SetSpokes(labels.Length, 1.1, degreeLabels: false);
            adder.Plot.PlottableList.Add(radar);
            adder.Plot.HideGridAndTicks();
            return radar;
        }

        public static SurroundAnalyzer SurroundAnalizer(this PlottableAdder adder)
        {
            SurroundAnalyzer surroundAnalizer = new SurroundAnalyzer();
            surroundAnalizer.Color = adder.GetNextColor();;
            adder.Plot.PlottableList.Add(surroundAnalizer);
            adder.Plot.HideGridAndTicks();
            return surroundAnalizer;
        }
    }
}