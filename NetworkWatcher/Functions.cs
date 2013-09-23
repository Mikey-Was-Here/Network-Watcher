using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

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

        public static int FirstNonZero(params int[] n)
        {
            for (int i = 0; i < n.Length; i++)
            {
                if (n[i] > 0) return n[i];
            }
            return 0;
        }

        public static IPAddress GetLocalIp4Address()
        {
            string localName = Dns.GetHostName();
            IPHostEntry localHost = Dns.GetHostEntry(localName);
            IPAddress[] localAddress = localHost.AddressList;

            for (int i = 0; i < localAddress.Length; i++)
            {
                string ipData = localAddress[i].AddressFamily.ToString() + " ";
                if (ipData == "Internetwork")
                {
                    return new IPAddress(localAddress[i].GetAddressBytes());
                }
            }
            return null;
        }

        public static bool IsLocal(IPAddress ipa)
        {
            byte[] bytes = ipa.GetAddressBytes();
            if (bytes.Length == 4)
            {
                // 10.0.0.0–10.255.255.255 - Single Class A
                if (bytes[0] == 10)
                    return true;
                // 172.16.0.0–172.31.255.255 - Contiguous range of 16 Class B blocks
                if (bytes[0] == 172 && (bytes[1] >= 16 && bytes[1] <= 31))
                    return true;
                // 192.168.0.0–192.168.255.255 - Contiguous range of 256 Class C blocks
                if (bytes[0] == 192 && bytes[1] == 168)
                    return true;
                // 169.254.0.0/16 Link-local RFC 3927
                if (bytes[0] == 169 && bytes[1] == 254)
                    return true;
            }
            return false;
        }

        public static IPAddress Ping(string host, int hops)
        {
            Ping ping = new Ping();
            byte[] data = new byte[16];
            PingOptions options = new PingOptions(hops, true);
            PingReply reply = ping.Send(host, 30, data, options);
            return reply.Address;
        }

        public static IPAddress ExtrernalAddress()
        {
            return null;
        }

        public static string FormatIp(IPAddress ipa)
        {
            string ipData = string.Empty;
            if (ipa.AddressFamily == AddressFamily.InterNetwork)
            {
                ipData = "IPv4";
                foreach (byte b in ipa.GetAddressBytes())
                {
                    if (ipData[ipData.Length - 1] != ' ')
                        ipData += ".";
                    ipData += string.Format("{0}", b);
                }
            }
            else
            {
                ipData = "IPv6";
                byte[] bytes = ipa.GetAddressBytes();
                for (int i = 0; i < bytes.Length; i = i + 2)
                {
                    if (ipData[ipData.Length - 1] != ' ')
                        ipData += ":";
                    byte b = bytes[i];
                    ipData += string.Format("{0:X2}", b);
                    b = bytes[i+1];
                    ipData += string.Format("{0:X2}", b);
                }
            }
            return ipData;
        }
    }
}