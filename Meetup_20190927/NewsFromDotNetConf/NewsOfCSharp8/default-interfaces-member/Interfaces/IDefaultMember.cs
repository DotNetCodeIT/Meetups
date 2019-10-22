using System;
namespace NewsOfCSharp8.defaultinterfacesmember.Interfaces
{
    public interface IDefaultMember
    {
        void DefaultMethod(string str)
        {
            Console.WriteLine($"Hello wold {str} [Default version]");
        }
    }
}
