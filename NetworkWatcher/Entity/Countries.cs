using System;
using System.Collections.Generic;
using System.IO;

namespace NetworkWatcher.Entity
{
    internal class Countries : SortedDictionary<string, Country>
    {
        public Countries(string fileName)
        {
            foreach (string line in File.ReadAllLines(fileName))
            {
                string[] items = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                Country country = new Country(items[0], items[1]);
                this.Add(items[0], country);
            }
        }
    }
}