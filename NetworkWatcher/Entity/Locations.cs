using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NetworkWatcher.Entity
{
    public class Locations : SortedDictionary<long, Location>
    {
        public Locations(string fileName)
        {
            foreach (string line in File.ReadAllLines(fileName))
            {
                string[] items = line.Split(new[] { ',' }, StringSplitOptions.None);

                if (items.Length < 9)
                    continue;

                int locId;
                if (!int.TryParse(Functions.RemoveQuotes(items[0]), out locId))
                    continue;

                string country = Functions.RemoveQuotes(items[1]);
                string region = Functions.RemoveQuotes(items[2]);
                string city = Functions.RemoveQuotes(items[3]);
                string postalCode = Functions.RemoveQuotes(items[4]);
                string metroCode = Functions.RemoveQuotes(items[7]);
                string areaCode = Functions.RemoveQuotes(items[8]);

                float latitude;
                if (!float.TryParse(Functions.RemoveQuotes(items[5]), out latitude))
                    continue;

                float longitude;
                if (!float.TryParse(Functions.RemoveQuotes(items[6]), out longitude))
                    continue;

                Location location = new Location(locId, country, region, city, postalCode, latitude, longitude, metroCode, areaCode);
                this.Add(locId, location);
            }
        }

        public string Find(long locId)
        {
            if (!this.ContainsKey(locId))
                return string.Empty;

            return this[locId].Country;
        }

    }
}