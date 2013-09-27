using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;


namespace NetworkWatcher.Entity
{
    public class Locations : ConcurrentDictionary<long, Location>
    {
        public Locations(string fileName)
        {
            Parallel.ForEach(File.ReadAllLines(fileName), (string line) =>
            {
                string[] items = line.Split(new[] { ',' }, StringSplitOptions.None);

                if (items.Length <= 9)
                {
                    int locId;
                    if (int.TryParse(Functions.RemoveQuotes(items[0]), out locId))
                    {
                        string country = Functions.RemoveQuotes(items[1]);
                        string region = Functions.RemoveQuotes(items[2]);
                        string city = Functions.RemoveQuotes(items[3]);
                        string postalCode = Functions.RemoveQuotes(items[4]);
                        string metroCode = Functions.RemoveQuotes(items[7]);
                        string areaCode = Functions.RemoveQuotes(items[8]);

                        float latitude;
                        if (float.TryParse(Functions.RemoveQuotes(items[5]), out latitude))
                        {
                            float longitude;
                            if (float.TryParse(Functions.RemoveQuotes(items[6]), out longitude))
                            {
                                Location location = new Location(locId, country, region, city, postalCode, latitude, longitude, metroCode, areaCode);
                                this.TryAdd(locId, location);
                            }
                        }
                    }
                }
            });
        }

        public string Find(long locId)
        {
            if (!this.ContainsKey(locId))
                return string.Empty;

            return this[locId].Country;
        }
    }
}