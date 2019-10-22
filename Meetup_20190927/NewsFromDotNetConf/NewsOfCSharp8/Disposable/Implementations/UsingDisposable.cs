using System;
using NewsOfCSharp8.Disposable.Models;

namespace NewsOfCSharp8.Disposable.Implementations
{
    public class UsingDispodable
    {
        public UsingDispodable()
        {
        }

        public void Print()
        {
            ConsoleColor currentBackground = Console.BackgroundColor;
            ConsoleColor currentForeground = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"pre using declared");
            using var disposableClass = new DisposableClass();
            using var disposableStruct = new DisposableStruct();
            // different from using (var d = new DisposableClass())

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"post using declared ");

            Console.BackgroundColor = currentBackground ;
            Console.ForegroundColor = currentForeground;

            Console.WriteLine($"After this line the objects dispose");

        }
    }
}
