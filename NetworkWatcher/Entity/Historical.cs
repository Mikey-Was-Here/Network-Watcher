using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkWatcher.Entity
{
    public class Historical
    {
        private static Dictionary<long, Connections> history = new Dictionary<long, Connections>();
        private static long nextVersion = 0;

        public static Connections Add()
        {
            long localVersion = Interlocked.Increment(ref nextVersion);
            Connections connections = new Connections(localVersion);
            history.Add(localVersion, connections);
            return connections;
        }
    }
}
