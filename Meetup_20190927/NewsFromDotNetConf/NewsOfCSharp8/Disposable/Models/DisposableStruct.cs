using System;
namespace NewsOfCSharp8.Disposable.Models
{
    public struct DisposableStruct : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("Dispose struct");
        }
    }
}
