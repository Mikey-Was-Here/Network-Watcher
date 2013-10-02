using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkWatcher.Entity
{
    public class ProcessInfo
    {
        public string ProcessName { get; private set; }
        public int ProcessId { get; private set; }

        public ProcessInfo(int pid, string name)
        {
            ProcessId = pid;
            ProcessName = name;
        }
    }
}
