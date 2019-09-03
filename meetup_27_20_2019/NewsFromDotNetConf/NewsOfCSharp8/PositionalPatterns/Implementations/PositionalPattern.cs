using System;
using NewsOfCSharp8.PositionalPatterns.Model;

namespace NewsOfCSharp8.PositionalPatterns.Implementations
{
    public class PositionalPattern
    {
        public PointModel pointModel;

        public PositionalPattern(PointModel pointModel)
        {
            this.pointModel = pointModel;
        }

        public void PrintDeconstructor()
        {

            ConsoleColor currentBackground = Console.BackgroundColor;
            ConsoleColor currentForeground = Console.ForegroundColor;

            int x = 0, y = 0;

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"Point({pointModel.X},{pointModel.Y})");
            pointModel.Deconstruct(out x, out y);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"({x}, {y})");

            Console.ForegroundColor = currentForeground;
            Console.BackgroundColor = currentBackground;

        }

        public void PrintWhenClause()
        {
            ConsoleColor currentBackground = Console.BackgroundColor;
            ConsoleColor currentForeground = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"Point({pointModel.X},{pointModel.Y})");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Quadrant: {WhenClause()}");


            Console.ForegroundColor = currentForeground;
            Console.BackgroundColor = currentBackground;

        }

        private string WhenClause() => pointModel switch
        {
            (0, 0) => "origin",
            var (x, y) when x > 0 && y > 0 => "dx-up",
            var (x, y) when x < 0 && y > 0 => "sx-up",
            var (x, y) when x < 0 && y < 0 => "sx-dw",
            var (x, y) when x > 0 && y < 0 => "dx-dw",
            var (_, _) => "border",
            _ => "Unknown"
        };


    }
}
