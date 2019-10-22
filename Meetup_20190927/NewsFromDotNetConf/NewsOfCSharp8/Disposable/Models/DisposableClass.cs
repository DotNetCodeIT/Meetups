using System;
namespace NewsOfCSharp8.Disposable.Models
{
    public class DisposableClass: IDisposable
    {
        public DisposableClass()
        {
        }

        //public Guid Id { get; set; } = Guid.NewGuid();

        public void Dispose()
        {
            Console.WriteLine("Dispose object");
        }
    }
}
