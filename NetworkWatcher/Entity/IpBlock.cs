namespace NetworkWatcher.Entity
{
    public class IpBlock
    {
        public long StartIpNum { get; private set; }

        public long EndIpNum { get; private set; }

        public int LocId { get; private set; }

        public IpBlock(long startIpNum, long endIpNum, int locId)
        {
            StartIpNum = startIpNum;
            EndIpNum = endIpNum;
            LocId = locId;
        }
    }
}