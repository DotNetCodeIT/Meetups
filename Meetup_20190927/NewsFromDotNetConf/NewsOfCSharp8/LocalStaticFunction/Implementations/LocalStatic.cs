using System;
namespace NewsOfCSharp8.LocalStaticFunction.Implementations
{
    public class LocalStatic
    {
        public LocalStatic()
        {
        }

        public void Print()
        {
            int a = 5;
            int b = 6;

            Console.WriteLine(Sum(a, b));

            static int Sum(int _a, int _b)
            {
                // return a + b; a e b sono inaccessibili perchè la funzione è statica
                return _a + _b;
            }
        }
    }
}
