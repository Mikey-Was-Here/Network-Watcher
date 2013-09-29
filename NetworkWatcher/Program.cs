using System.Diagnostics;
using System;
using System.IO;
using System.Windows.Forms;
using NetworkWatcher.Entity;
using System.Threading.Tasks;

namespace NetworkWatcher
{
    internal static class Program
    {
        private static IpBlocks ipBlocks = null;

        public static IpBlocks IpBlocks { get { return ipBlocks; } }

        private static Locations locations = null;

        public static Locations Locations { get { return locations; } }

        private static Countries countries = null;

        public static Countries Countries { get { return countries; } }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Splash splash = new Splash();
            splash.Show();

            //string data = ArinApi.GetOrginization(new System.Net.IPAddress(new byte[] { 1,1,1,1 } ));
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string countryDataFile = Path.Combine(Entity.Config.DataPath, "Country.csv");
            Task t1 = new Task(() =>
            {
                countries = new Countries(countryDataFile);
            });
            t1.Start();

            //string ipBlocksDataFile = Path.Combine(Entity.Config.DataPath, "GeoLiteCity-Blocks.csv");
            //Task t2 = new Task(() =>
            //{
            //    ipBlocks = new IpBlocks(ipBlocksDataFile);
            //});
            //t2.Start();

            //string locationDataFile = Path.Combine(Entity.Config.DataPath, "GeoLiteCity-Location.csv");
            //Task t3 = new Task(() =>
            //{
            //    locations = new Locations(locationDataFile);
            //});
            //t3.Start(); 
            
            Task.WaitAll(t1);

            splash.Hide();

            Application.Run(new frmMain());
        }
    }
}