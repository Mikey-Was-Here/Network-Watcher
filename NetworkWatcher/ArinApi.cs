using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Net.Http.Headers;


namespace NetworkWatcher
{
    class ArinApi
    {
        public static string GetOrginization(IPAddress ipa)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            //client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Accept", "application/xml");
            byte[] ip = ipa.GetAddressBytes();
            string uri = string.Format("http://whois.arin.net/rest/ip/{0}.{1}.{2}.{3}", ip[0], ip[1], ip[2], ip[3]);
            string data = client.GetStringAsync(uri).Result;
            data = data.Replace("<?xml-stylesheet type='text/xsl' href='http://whois.arin.net/xsl/website.xsl' ?>", string.Empty);
            data = data.Replace("<?xml version='1.0'?>", string.Empty);
            XDocument xdoc = XDocument.Parse(data);
            string OrgName = (from el in xdoc.Descendants() where el.Name.LocalName == "orgRef" select el.Attribute("name").Value).FirstOrDefault();
            return OrgName;
        }
        
    }
}
