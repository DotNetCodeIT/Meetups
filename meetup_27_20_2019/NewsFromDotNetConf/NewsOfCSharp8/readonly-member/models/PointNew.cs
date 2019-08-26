using System;
namespace NewsOfCSharp8.readolymember.Models
{
    public struct PointNew
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Distance => Math.Sqrt(X * X + Y * Y);

        public readonly override string ToString()
        {
            // Errore di compilazione
            // X = 10;

            // Dinstance non è readonly e genera un warning
            return $"({X}, {Y}) is {Distance} from the origin";
        }

        public readonly void Translate(int xOffset, int yOffset)
        {
            // Errore di compilazione
            // X += xOffset;
            // Y += yOffset;
        }
    }
}
