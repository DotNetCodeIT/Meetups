using System;
using System.Collections.Generic;
using NewsOfCSharp8.patternmatching.Models;

namespace NewsOfCSharp8.patternmatching.Implementations
{
    public class PatternMatching
    {
        List<City> cities = new List<City>()
        {
            new City("IT", "Rome"),
            new City("IT", "Florence"),
            new City("FR", "Paris"),
            new City("US", "Paris"),
            new City("DE", "Berlin")
        };

        public PatternMatching()
        {
        }

        public void PrintPatternMatching()
        {
            foreach(var city in cities)
            {

                ConsoleColor currentBackground = Console.BackgroundColor;
                ConsoleColor currentForeground = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{city.Country} {city.Name} ");
                var name = GetStateName(city);
                var nameExt = GetStateNameEx(city);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($" * name: {name} ");
                Console.WriteLine($" * nameext: {nameExt}");

                Console.ForegroundColor = currentForeground;
                Console.BackgroundColor = currentBackground;
            }
        }


        public string RockPaperScissors(string country, string city)
            => (country, city) switch
            {
                ("IT", "Rome") => "Italy (Rome)",
                (_, "Paris") => "France or Texas",
                ("IT", _) => "Italy",
                _ => "Other", // Forma compatta per ( _, _) => "Other"
            };

        private string GetStateName(City address)
            => address switch
            {
                { Country: "IT", Name: "Rome" } => "Italy (Rome)",
                { Country: _, Name:"Paris" } => "France or Texas",
                { Country: "IT",Name: _} => "Italy",
                _ => "Other"
            };

        private string GetStateNameEx(City address)
        {
            var countryName = string.Empty;
            switch (address)
            {
                case { Country: "IT", Name: "Rome" }:
                    countryName = "Italy (Rome)";
                    break;
                case { Country: _, Name: "Paris" }:
                    countryName = "France or Texas";
                    break;
                case { Country: "IT", Name: _ }:
                    countryName = "Italy";
                    break;
                default:
                    countryName = "Other";
                    break;

            }
            return countryName;
        }
    }
}
