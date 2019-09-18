using System;
namespace NewsOfCSharp8.NonNullableObject.Models
{
    public class NonNullableObj
    {
        public NonNullableObj()
        {
        }

        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
