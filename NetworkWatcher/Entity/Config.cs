using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
