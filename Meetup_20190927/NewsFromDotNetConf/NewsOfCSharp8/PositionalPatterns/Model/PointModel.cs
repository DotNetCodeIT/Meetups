using System;
namespace NewsOfCSharp8.PositionalPatterns.Model
{
    public class PointModel
    {

        public int X { get; }
        public int Y { get; }

        public PointModel(int x, int y) => (X, Y) = (x, y);

        public void Deconstruct(out int x, out int y) =>
            (x, y) = (X, Y);
    }
}
