using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang10
{
    /// <summary>
    /// A struct is a value type. When a struct is created, the variable to which the struct
    /// is assigned holds the struct's actual data. When the struct is assigned to a new variable,
    /// it is copied. The new variable and the original variable therefore contain two separate
    /// copies of the same data. Changes made to one copy do not affect the other copy.
    ///
    /// Structs are best suited to small structures that contain primarily data that is not
    /// modified after the struct is created.
    ///
    /// See: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/structs
    /// See: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/index
    /// </summary>
    public class StructBehaviour
    {
        public int Calculate(int a, int b, int operation)
        {
            int y = 1;
            switch (y)
            {
                case 10: return 0;
                default: return 1;
            }
        }

        [Test]
        public void StructsCanBeCreatedWithoutNewButFieldsMustFirstBeInitialized()
        {
            Rectangle r; // this variable contains the value

            // Error CS0151 A switch expression or case label must be a bool, char, string, integral, enum, or corresponding nullable type in C# 6 and earlier
            r.Width = 10;
            r.Width.Should().Be(10);

            object x = "";
            int o = (int)x;
            switch (o)
            {
                case 2: return;
                case 3: return;
            }
        }

        [Test]
        public void FieldsOfStructsAreInitializedToDefaultValuesWhenDefaultNewIsCalled()
        {
            // Structs have an implicit default constructor which assign all members to default values.
            Rectangle r = new Rectangle(); // initializes all members to default.  This must be called before you access
            // if you don't initialize a member

            r.Length.Should().Be(0);

            Rectangle r2;
            r2.Length = 1;
            r2.Length.Should().Be(1);
        }

        [Test]
        public void StructsAreAlwaysCopied()
        {
            Rectangle r = new Rectangle();
            r.Length = 10;
            Rectangle r2 = r;
            r2.Length.Should().Be(10);
            r2.Length = 9;
            r2.Length.Should().Be(9);
            r.Length.Should().Be(10);
        }

        [Test]
        public void NonDefaultConstructorsCanBeDefined()
        {
            Rectangle r = new Rectangle(10, 12);
            r.Width.Should().Be(10);
        }

        [Test]
        public void StructsCanBeNullable()
        {
            // Nullable<Rectangle> r = null;  Generics are implemented in C# 2
        }
    }

    public interface Polygon
    {

    }

    // All structs inherit from `ValueType` which inherits from `Object`
    // structs cannot inherit from another struct or class and cannot be inherited from
    public struct Rectangle : Polygon  // structs can implement interfaces!
    {
        // public Rectangle() { } // CS0568 structs cannot contain explicit parameterless constructors

        public Rectangle(int width, int length)
        {
            Width = width;
            Length = length;
            Colour = new Colour();
        }

        // structs cannot have a default constructor or a finalizer
        public int Width;
        public int Length;

        // Structs can contain references to reference types
        public Colour Colour;

        // Within a `struct` definition, fields cannot be initialized unless they are static or const
        // public int Area = 10; // CS0573 non static struct member cannot have an initializer
    }

    public class Colour { }
}
