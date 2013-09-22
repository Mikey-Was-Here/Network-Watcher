namespace NetworkWatcher
{
    internal class Country
    {
        public string Iso2 { get; private set; }

        public string Name { get; private set; }

        public Country(string iso2, string name)
        {
            Iso2 = iso2;
            Name = Functions.RemoveQuotes(name);
        }
    }
}