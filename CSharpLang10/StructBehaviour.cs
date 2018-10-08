using System;
using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang10
{
    /// <summary>
    /// In C# 1.0
    ///
    /// See: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/structs
    /// </summary>
    public class StructBehaviour
    {
        [Test]
        public void FieldsOfStructsAreInitializedToDefaultValuesWhenNewIsNotCalled()
        {
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
    }

    public struct Rectangle
    {
        // structs cannot have a default constructor or a finalizer
        public int Width;
        public int Length;
    }
}
