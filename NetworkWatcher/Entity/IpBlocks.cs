using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NetworkWatcher.Entity
{
    internal class IpBlocks : SortedDictionary<long, IpBlock>
    {
        private static object lo = new object();
        public IpBlocks(string fileName)
        {
            Parallel.ForEach(File.ReadAllLines(fileName), (string line) =>
            {
                string[] items = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (items.Length >= 3)
                {
                    long fromIp;
                    if (long.TryParse(Functions.RemoveQuotes(items[0]), out fromIp))
                    {
                        long toIp;
                        if (long.TryParse(Functions.RemoveQuotes(items[1]), out toIp))
                        {
                            int loc;
                            if (int.TryParse(Functions.RemoveQuotes(items[2]), out loc))
                            {
                                IpBlock ipBlock = new IpBlock(fromIp, toIp, loc);
                                lock (lo)
                                {
                                    this.Add(fromIp, ipBlock);
                                }
                            }
                        }
                    }
                }
            });
        }

        public int Find(long ip)
        {
            KeyValuePair<long, IpBlock> kvp = (from item in this where ip >= item.Key && ip <= item.Value.EndIpNum select item).FirstOrDefault();
            return (kvp.Value != null) ? kvp.Value.LocId : -1;
        }
    }
}