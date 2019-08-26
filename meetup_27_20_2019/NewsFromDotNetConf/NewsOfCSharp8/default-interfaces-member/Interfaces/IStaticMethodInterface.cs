using System;
namespace NewsOfCSharp8.defaultinterfacesmember.Interfaces
{
    public interface IStaticMethodInterface
    {
        public static void ChangeHello(string hello)
        {
            helloString = hello;
        }

        private static string helloString = "Hello world";

        void PrintHello()
        {
            Console.WriteLine(helloString);
        }
    }
}
