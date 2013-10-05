using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;

namespace NetworkWatcher.Entity
{
    public delegate void ConnectionDataCompleteEventHandler(object sender, EventArgs e);

    public class Connection
    {
        public event ConnectionDataCompleteEventHandler ConnectionDataComplete;

        private enum TriState
        {
            ValueIsNotSet,
            ValueIsSet,
            ValueHasNoValue
        }

        [Flags]
        private enum EventComplete
        {
            None = 0,
            ProcessName = 1,
            ArinInfo = 2,
            HostName = 4,
            GeoLocation = 8
        }

        public IPAddress Remote { get; private set; }
        public IPAddress Local { get; private set; }
        public string Owner { get; private set; }
        public GeoLocationData Location { get; private set; }
        public string HostName { get; private set; }
        public ProcessInfo ProcessInfo { get; set; }

        private EventComplete eventComplete = EventComplete.None;

        private void FillInProcessName(int pid)
        {
            Process p = null;
            try
            {
                p = Process.GetProcessById(pid);
            }
            catch { }

            string name = p == null ? "Unknown" : p.ProcessName;
            this.ProcessInfo = new ProcessInfo(pid, name);
            
            eventComplete = eventComplete | EventComplete.ProcessName;
            FireCompleteEvent();
        }

        public void FillInArinInfo(IPAddress ipa)
        {
            try
            {
                this.Owner = ArinApi.GetOrginization(ipa);
            }
            catch { }
            
            eventComplete = eventComplete | EventComplete.ArinInfo;
            FireCompleteEvent();
        }

        public void FillInHostName(IPAddress ipa)
        {
            string hostName = string.Empty;
            try
            {
                IPHostEntry entry = Dns.GetHostEntry(ipa);
                hostName = entry.HostName;
            }
            catch { }

            HostName = hostName;
            eventComplete = eventComplete | EventComplete.HostName;
            FireCompleteEvent();
        }

        public void FillInGeoLocation(IPAddress ipa)
        {
            try
            {
                this.Location = GeoLocationApi.GetLocation(ipa);
            }
            catch { }
            
            eventComplete = eventComplete | EventComplete.GeoLocation;
            FireCompleteEvent();
        }

        private void FireCompleteEvent()
        {
            if ((int)eventComplete == 15 && ConnectionDataComplete != null)
            {
                ConnectionDataComplete.Invoke(this, new EventArgs());
            }
        }

        public Connection(IPAddress remote, IPAddress local, int owningProcessId)
        {
            Remote = remote;
            Local = local;
            ProcessInfo = new ProcessInfo(owningProcessId, null);
            Task t1 = new Task(() => { FillInArinInfo(Remote); });
            t1.Start();
            Task t2 = new Task(() => { FillInGeoLocation(Remote); });
            t2.Start();
            Task t3 = new Task(() => { FillInHostName(Remote); });
            t3.Start();
            Task t4 = new Task(() => { FillInProcessName(owningProcessId); });
            t4.Start();
        }

        public override int GetHashCode()
        {
            return string.Format("{0}:{1}:{2}",
                this.Remote.GetHashCode(),
                this.Local.GetHashCode(),
                this.ProcessInfo.ProcessId).GetHashCode();
        }

        public IDisposable Subscribe(IObserver<Connection> observer)
        {
            throw new NotImplementedException();
        }
    }
}
