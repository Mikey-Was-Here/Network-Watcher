using System;
using System.IO;

namespace NetworkWatcher.Entity
{
    public class Config
    {
        public static string DataPath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            }
        }
    }
}