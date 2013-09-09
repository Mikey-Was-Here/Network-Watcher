using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using NetworkWatcher.Entity;

namespace NetworkWatcher
{
    static class Program
    {
        private static IpBlocks ipBlocks = null;
        public static IpBlocks Iplocks { get { return ipBlocks; } }

        private static Locations locations = null;
        public static Locations Locations { get { return locations; } }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string dataFile = Path.Combine(Entity.Config.DataPath, "Country.csv");
            Countries countries = new Countries(dataFile);

            dataFile = Path.Combine(Entity.Config.DataPath, "GeoLiteCity-Blocks.csv");
            ipBlocks = new IpBlocks(dataFile);

            dataFile = Path.Combine(Entity.Config.DataPath, "GeoLiteCity-Location.csv");
            locations = new Locations(dataFile);

            Application.Run(new frmMain());
        }
    }
}
