using System;
namespace NewsOfCSharp8.readolymember.Models
{
    public struct PointOld
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Distance => Math.Sqrt(X * X + Y * Y);

        public override string ToString()
        {
            X = 10;
            return $"({X}, {Y}) is {Distance} from the origin";
        }
            
    }
}
