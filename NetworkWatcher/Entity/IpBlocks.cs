using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NetworkWatcher.Entity
{
    internal class IpBlocks : SortedDictionary<long, IpBlock>
    {
        public IpBlocks(string fileName)
        {
            foreach (string line in File.ReadAllLines(fileName))
            {
                string[] items = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (items.Length < 3)
                    continue;

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
                            this.Add(fromIp, ipBlock);
                        }
                    }
                }
            }
        }

        public int Find(long ip)
        {
            KeyValuePair<long, IpBlock> kvp = (from item in this where ip >= item.Key && ip <= item.Value.EndIpNum select item).FirstOrDefault();
            return kvp.Value.LocId;
        }
    }
}