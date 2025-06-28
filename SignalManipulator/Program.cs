using System;
using System.IO;
using System.Windows.Forms;

namespace SignalManipulator
{
    internal static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());

            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                File.WriteAllText("fatal_error.txt", ex.ToString());
            }

        }
    }
}