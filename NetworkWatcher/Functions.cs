using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkWatcher
{
    public static class Functions
    {
        public static string RemoveQuotes(string value)
        {
            if (value.StartsWith("\"") && value.Length > 1) value = value.Substring(1);
            if (value.EndsWith("\"") && value.Length > 1) value = value.Substring(0, value.Length - 1);
            return value;
        }
    }
}
