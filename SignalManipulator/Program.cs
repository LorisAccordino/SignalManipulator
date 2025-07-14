using SignalManipulator.EffectUI;
using SignalManipulator.Logic.Core.Effects.Loaders;

namespace SignalManipulator
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            EffectUILoader.LoadFromAssembly(typeof(EffectUIForm).Assembly);
            Application.Run(new MainForm());
        }
    }
}