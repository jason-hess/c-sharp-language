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
            public readonly double ReadOnlyDistance => Math.Sqrt(X * X + Y * Y);

            public int X;
            public int Y;

            //public override readonly string ToString() => 
              //  $"{X} {Y} {Distance}"; // WWarning CS8656  Call to non-readonly member 'StructBehaviour.Point.Distance.get' from a 'readonly' member results in an implicit copy of 'this'.

                public void Test()
            {
                Y++;
            }

                // Note: Adding readonly here then makes the compiler make the fields accessed by 
                // this method read-only in the Struct read-only
            //public readonly string ToReadOnlyString() =>
              //  $"{X++} {ReadOnlyDistance}"; // Error CS1604  Cannot assign to 'X' because it is read-only CSharpLang80    


        }
    }
}
