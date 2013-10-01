using System.Collections.Concurrent;
using System.Net;
using System.Net.Http;
using System.Xml.Linq;
using NetworkWatcher.Entity;

namespace NetworkWatcher
{
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
                    decimal temp = 0;
                    if (el.Name == "Latitude" && !string.IsNullOrEmpty(el.Value))
                    {
                        decimal.TryParse(el.Value, out temp);
                        geoData.Latitude = temp;
                    }
                    if (el.Name == "Longitude" && !string.IsNullOrEmpty(el.Value))
                    {
                        decimal.TryParse(el.Value, out temp);
                        geoData.Longitude = temp;
                    }
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