using System;
namespace NewsOfCSharp8.IndexIntervals.Implememtations
{
    public class IndexInterval
    {
        string[] number = new string[]
        {
                        // index from start    index from end
            "zero",     // 0                   ^10 (Lenght)
            "uno",      // 1                   ^9
            "due",      // 2                   ^8
            "tre",      // 3                   ^7
            "quattro",  // 4                   ^6
            "cinque",   // 5                   ^5
            "sei",      // 6                   ^4
            "sette",    // 7                   ^3
            "otto",     // 8                   ^2
            "nove"      // 9 (or words.Length) ^1
        };              // 10(or words.Length) ^0

        public IndexInterval()
        {
        }

        public void PrintIndex()
        {
            ConsoleColor currentBackground = Console.BackgroundColor;
            ConsoleColor currentForeground = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"The last word is {number[^1]}"); // nove
            Console.WriteLine($"The first word is {number[^number.Length]}"); //zero
            //Console.WriteLine($"this is an exception {number[^0]}");
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
