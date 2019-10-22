using System;
using NewsOfCSharp8.defaultinterfacesmember.Interfaces;

namespace NewsOfCSharp8.defaultinterfacesmember.Implementations
{
    public class FullImplementation : IDefaultMember
    {
        public FullImplementation()
        {
        }

        public void DefaultMethod(string str)
        {
            Console.WriteLine($"Hello wold {str} [Redefinited method]");
        }
    }
}
