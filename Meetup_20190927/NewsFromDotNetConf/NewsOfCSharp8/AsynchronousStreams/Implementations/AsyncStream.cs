using System;
using System.Threading.Tasks;

namespace NewsOfCSharp8.AsynchronousStreams.Implementations
{
    public class AsyncStream
    {
        public AsyncStream()
        {
        }

        private async System.Collections.Generic.IAsyncEnumerable<int> GenerateSequence()
        {
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(100);
                yield return i;
            }
        }

        private System.Collections.Generic.IEnumerable<int> GenerateSequenceNormal()
        {
            for (int i = 0; i < 20; i++)
            {
                yield return i;
            }
        }

        public async ValueTask Print()
        {
            ConsoleColor currentBackground = Console.BackgroundColor;
            ConsoleColor currentForeground = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"await foreach");

            await foreach (var number in GenerateSequence())
            {
                Console.WriteLine($"index {number}");
            }

            Console.BackgroundColor = currentBackground;
            Console.ForegroundColor = currentForeground;
        }

        public async ValueTask PrintParallel()
        {
            ConsoleColor currentBackground = Console.BackgroundColor;
            ConsoleColor currentForeground = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine($"Parallel foreach");

            Parallel.ForEach(GenerateSequenceNormal(), number =>
            {
                Console.WriteLine($"index {number}");
            });

            Console.BackgroundColor = currentBackground;
            Console.ForegroundColor = currentForeground;
        }
    }
}
