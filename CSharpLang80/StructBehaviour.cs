using System;

namespace CSharpLang80
{
    public class StructBehaviour
    {
        // You can now apply the readonly modifier to any member of a struct
        // It indicates that the member does not modify state
        public struct Point
        {
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }


            public double Distance => Math.Sqrt(X * X + Y * Y);

            public int X;
            public int Y;

            public override readonly string ToString() => 
                $"{X} {Y} {Distance}";
        }
    }
}
