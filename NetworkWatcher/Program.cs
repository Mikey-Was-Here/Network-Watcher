using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Raven.Client.Embedded;

namespace NetworkWatcher
{
    static class Program
    {
        static public EmbeddableDocumentStore DocumentStore;

        private static void InitializeDatabase()
        {
            DocumentStore = new EmbeddableDocumentStore { DataDirectory = "Data" }; 
            DocumentStore.Initialize();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            InitializeDatabase();

            Application.Run(new frmMain());
        }
    }
}
