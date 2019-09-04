using System;
namespace NewsOfCSharp8.IndexIntervals.Implememtations
{
    public class IndexInterval
    {
        string[] number = new string[]
        {
                        // index from start    index from end
            "zero",     // 0                   ^9
            "uno",      // 1                   ^8
            "due",      // 2                   ^7
            "tre",      // 3                   ^6
            "quattro",  // 4                   ^5
            "cinque",   // 5                   ^4
            "sei",      // 6                   ^3
            "sette",    // 7                   ^2
            "otto",     // 8                   ^1
            "nove"      // 9 (or words.Length) ^0
        };

        public IndexInterval()
        {
        }

        public void PrintIndex()
        {
            ConsoleColor currentBackground = Console.BackgroundColor;
            ConsoleColor currentForeground = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"The last word is {number[^1]}");

            Console.BackgroundColor = currentBackground;
            Console.ForegroundColor = currentForeground;
        }


        public void PrintIndexInterval()
        {
            ConsoleColor currentBackground = Console.BackgroundColor;
            ConsoleColor currentForeground = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine($"Interval 1..4");

            foreach(var n in number[1..4])
            {
                Console.WriteLine($"number: {number}");
            }

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"Interval 6..");

            foreach (var n in number[6..])
            {
                Console.WriteLine($"number: {number}");
            }

            Console.BackgroundColor = currentBackground;
            Console.ForegroundColor = currentForeground;
        }
    }
}
