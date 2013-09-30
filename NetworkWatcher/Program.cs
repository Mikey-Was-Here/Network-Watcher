using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NetworkWatcher.Entity;
using NetworkWatcher.Forms;

namespace NetworkWatcher
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            frmSplash splash = new frmSplash();
            splash.Show();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int i = 100; i > 0; i--)
            {
                Thread.Sleep(10);
                splash.Opacity = (double)i / (double)100;
                splash.Refresh();
            }

            splash.Hide();

            Application.Run(new frmMain());
        }
    }
}