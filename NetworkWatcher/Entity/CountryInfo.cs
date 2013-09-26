using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace NetworkWatcher
{
    public class CountryInfo
    {
        public string Iso2Code { get; private set; }
        public string Name { get; private set; }

        public CountryInfo(string iso2Code, string name)
        {
            Iso2Code = iso2Code;
            Name = Functions.RemoveQuotes(name);
        }
    }

    public class CountryInfos
    {
        private Dictionary<string, CountryInfo> Items = new Dictionary<string, CountryInfo>();

        public CountryInfos()
        {
        }

        public Task Read(string FileName)
        {
            Task t = new Task(() =>
            {
                foreach (string line in File.ReadAllLines(FileName))
                {
                    string[] items = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    CountryInfo country = new CountryInfo(items[0], items[1]);
                    Items.Add(country.Iso2Code, country);
                }
            });

            return t;
        }

        public void Add(CountryInfo country)
        {
            Items.Add(country.Iso2Code, country);
        }

        public CountryInfo this[string iso2Code]
        {
            get
            {
                return Items[iso2Code];
            }
        }
    }
}
