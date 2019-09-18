using System;
namespace NewsOfCSharp8.patternmatching.Models
{
    public class City
    {
        public City()
        {
        }

        public City(string country, string name)
        {
            this.Country = country;
            this.Name = name;
        }

        public string Country { get; set; }
        public string Name { get; set; }

    }
}
