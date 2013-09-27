using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace NetworkWatcher
{
    public class IpInfo
    {
        public IPAddress RemoteAddress { get; private set; }
        public int ProcessId { get; private set; }
        public string ProcessName { get; private set; }
        public Country CountryInfo { get; private set; }
        public string HostName { get; private set; }
        public string ArinInfo { get; private set; }
    }
}
