using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace NetworkWatcher.Entity
{
    public class Connection
    {
        public IPAddress Remote { get; private set; }
        public IPAddress Local { get; private set; }
        public string Owner { get; private set; }
        public GeoLocationData Location { get; private set; }
        public string HostName { get; private set; }
        public ProcessInfo Process { get; set; }

        public Connection(IPAddress remote, IPAddress local, string owner, GeoLocationData location, string hostName, ProcessInfo process)
        {
            Remote = remote;
            Local = local;
            Owner = owner;
            Location = location;
            HostName = hostName;
            Process = process;
        }

        public override int GetHashCode()
        {
            return string.Format("{0}:{1}:{2}:{3}:{4}:{5}",
                Remote.GetHashCode(),
                Local.GetHashCode(),
                Owner.GetHashCode(),
                Location.GetHashCode(),
                HostName.GetHashCode(),
                Process.GetHashCode()).GetHashCode();
        }
    }
}
