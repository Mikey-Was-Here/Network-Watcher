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

        public static IpBlocks Iplocks { get { return ipBlocks; } }

        private static Locations locations = null;

        public static Locations Locations { get { return locations; } }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //string data = ArinApi.GetOrginization(new System.Net.IPAddress(new byte[] { 1,1,1,1 } ));

            string dataFile = Path.Combine(Entity.Config.DataPath, "Country.csv");
            CountryInfos countries = new CountryInfos();
            Task t1 = countries.Read(dataFile);

            dataFile = Path.Combine(Entity.Config.DataPath, "GeoLiteCity-Blocks.csv");
            Task t2 = new Task(() =>
            {
                ipBlocks = new IpBlocks(dataFile);
            });

            dataFile = Path.Combine(Entity.Config.DataPath, "GeoLiteCity-Location.csv");
            Task t3 = new Task(() =>
            {
                locations = new Locations(dataFile);
            });
            t1.Start();
            t2.Start();
            t3.Start();

            Task.WaitAll(t1, t2, t3);

            Application.Run(new frmMain());
        }
    }
}