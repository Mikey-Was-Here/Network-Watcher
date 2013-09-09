using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkWatcher.Entity
{
    public class Location
    {
        public int LocId { get; private set; }
        public string Country { get; private set; }
        public string Region { get; private set; }
        public string City { get; private set; }
        public string PostalCode { get; private set; }
        public float Latitude { get; private set; }
        public float Longitude { get; private set; }
        public string MetroCode { get; private set; }
        public string AreaCode { get; private set; }

        public Location(int locId, string country, string region, string city, string postalCode, float latitude, float longitude, string metroCode, string areaCode)
        {
            LocId = locId;
            Country = country;
            Region = region;
            City = city;
            PostalCode = postalCode;
            Latitude = latitude;
            Longitude = longitude;
            MetroCode = metroCode;
            AreaCode = areaCode;
        }
    }
}
