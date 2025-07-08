using NAudio.Gui;
using ScottPlot;
using ScottPlot.Plottables;
using SignalManipulator.Logic.Core;
using SignalManipulator.UI.Controls.Plottables.Radars;
using SignalManipulator.UI.Controls.Plottables.Scatters;
using SignalManipulator.UI.Controls.Plottables.Signals;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SignalManipulator.UI.Controls.Plottables
{
    [ExcludeFromCodeCoverage]
    public static class PlotExtensions
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

        public static EnhancedRadar EnhancedRadar(this PlottableAdder adder, double[] values)
        {
            int num = 1;
            List<double[]> list = new List<double[]>(num);
            CollectionsMarshal.SetCount(list, num);
            Span<double[]> span = CollectionsMarshal.AsSpan(list);
            int index = 0;
            span[index] = values;
            List<double[]> series = list;
            return EnhancedRadar(adder, series);
        }

        public static EnhancedRadar EnhancedRadar(this PlottableAdder adder, IEnumerable<IEnumerable<double>> series)
        {
            double num = series.First().Count();
            EnhancedRadar radar = new EnhancedRadar();
            foreach (IEnumerable<double> item2 in series)
            {
                Color color = adder.GetNextColor();
                double[] array = item2.ToArray();
                if (array.Length != num)
                {
                    throw new InvalidOperationException("Every collection in the series must have the same number of items");
                }

                RadarSeries item = new RadarSeries
                {
                    Values = array,
                    FillColor = color.WithOpacity(),
                    LineColor = color.WithOpacity(1.0)
                };
                radar.Series.Add(item);
            }

            double num3 = series.Select((IEnumerable<double> x) => x.Max()).Max();
            radar.PolarAxis.SetCircles(num3, 4);
            radar.PolarAxis.SetSpokes(series.First().Count(), num3 * 1.1, degreeLabels: false);
            adder.Plot.PlottableList.Add(radar);
            adder.Plot.HideAxesAndGrid();
            return radar;
        }

        public static CardioidRadar CardioidRadar(this PlottableAdder adder, string[] labels)
        {
            CardioidRadar radar = new CardioidRadar(labels);
            radar.Color = adder.GetNextColor();
            radar.PolarAxis.SetCircles(1.0, 4);
            radar.PolarAxis.SetSpokes(labels.Length, 1.1, degreeLabels: false);
            adder.Plot.PlottableList.Add(radar);
            adder.Plot.HideAxesAndGrid();
            return radar;
        }

        public static CardioidRadar CardioidRadar(this PlottableAdder adder, int cardioidCount)
        {
            return CardioidRadar(adder, new string[cardioidCount]);
        }

        public static SurroundAnalyzer SurroundAnalizer(this PlottableAdder adder)
        {
            SurroundAnalyzer surroundAnalizer = new SurroundAnalyzer();
            surroundAnalizer.Color = adder.GetNextColor();;
            adder.Plot.PlottableList.Add(surroundAnalizer);
            adder.Plot.HideGrid();
            adder.Plot.Axes.Left.TickGenerator = new ScottPlot.TickGenerators.EmptyTickGenerator();
            adder.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.EmptyTickGenerator();
            adder.Plot.Axes.Right.TickGenerator = new ScottPlot.TickGenerators.EmptyTickGenerator();
            adder.Plot.Axes.Top.TickGenerator = new ScottPlot.TickGenerators.EmptyTickGenerator();
            return surroundAnalizer;
        }
    }
}