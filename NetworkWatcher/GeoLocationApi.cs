using System.Collections.Concurrent;
using System.Net;
using System.Net.Http;
using System.Xml.Linq;

namespace NetworkWatcher
{
    public class GeoLocationData
    {
        public IPAddress IpAddress;
        public string CountryCode;
        public string CountryName;
        public string RegionCode;
        public string RegionName;
        public string City;
        public string ZipCode;
        public string Latitude;
        public string Longitude;
        public string MetroCode;
        public string AreaCode;
    }

    public class GeoLocationApi
    {
        public static ConcurrentDictionary<IPAddress, GeoLocationData> GeoCache = new ConcurrentDictionary<IPAddress, GeoLocationData>();

        public static GeoLocationData GetLocation(IPAddress ipa)
        {
            if (GeoCache.ContainsKey(ipa))
            {
                return GeoCache[ipa];
            }
            /*
             * <Response>
             *      <Ip>192.151.154.154</Ip>
             *      <CountryCode>US</CountryCode>
             *      <CountryName>United States</CountryName>
             *      <RegionCode>MO</RegionCode>
             *      <RegionName>Missouri</RegionName>
             *      <City>Kansas City</City>
             *      <ZipCode>64116</ZipCode>
             *      <Latitude>39.1472</Latitude>
             *      <Longitude>-94.5735</Longitude>
             *      <MetroCode>616</MetroCode>
             *      <AreaCode>816</AreaCode>
             * </Response>*/
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Accept", "application/xml");
                byte[] ip = ipa.GetAddressBytes();
                string uri = string.Format("http://freegeoip.net/xml/{0}.{1}.{2}.{3}", ip[0], ip[1], ip[2], ip[3]);
                string data = client.GetStringAsync(uri).Result;
                data = data.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", string.Empty);
                XDocument xdoc = XDocument.Parse(data);

                GeoLocationData geoData = new GeoLocationData();

                foreach (XElement el in xdoc.Descendants())
                {
                    if (el.Name == "CountryCode") geoData.CountryCode = el.Value;
                    if (el.Name == "CountryName") geoData.CountryName = el.Value;
                    if (el.Name == "RegionCode") geoData.RegionCode = el.Value;
                    if (el.Name == "RegionName") geoData.RegionName = el.Value;
                    if (el.Name == "City") geoData.City = el.Value;
                    if (el.Name == "ZipCode") geoData.ZipCode = el.Value;
                    if (el.Name == "Latitude") geoData.Latitude = el.Value;
                    if (el.Name == "Longitude") geoData.Longitude = el.Value;
                    if (el.Name == "MetroCode") geoData.MetroCode = el.Value;
                    if (el.Name == "AreaCode") geoData.AreaCode = el.Value;
                }

                GeoCache.TryAdd(ipa, geoData);

                return geoData;
            }
            catch
            {
                return null;
            }
        }
    }
}