using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang90
{
    /// <summary>
    /// Records are immutable equatable objects.  Historically in C# mutable value and reference
    /// types are mutable and value types are passed by copy.
    /// </summary>
    public record Person(string FirstName, string LastName);

    /// <summary>
    /// Previously you would have had to write a fair bit of code to create immutable objects
    /// </summary>
    public class CSharp80Person
    {
        public CSharp80Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }
        public string LastName { get; }
    }

    /// <summary>
    /// Previously you would have had to write a fair bit of code to create immutable objects
    /// </summary>
    public class CSharp80PersonV2
    {
        public CSharp80PersonV2(string firstName, string lastName) =>
               (FirstName, LastName) = (firstName, lastName);

        public string FirstName { get; }
        public string LastName { get; }
    }

    [TestFixture]
    public class RecordBehaviour
    {
        [Test]
        public void Shoud()
        {
            var firstPerson = new Person("Billy", "Bob");
            firstPerson.FirstName.Should().Be("Billy");
            // firstPerson.FirstName = "Andy"; // Error CS8852  Init - only property or indexer 'Person.FirstName' can only be assigned in an object initializer, or on 'this' or 'base' in an instance constructor or an 'init' accessor.
            var secondPerson = new CSharp80Person("Cody", "Bob");
            secondPerson.FirstName.Should().Be("Cody");
            //secondPerson.FirstName = "Andy"; // Error CS0200  Property or indexer 'CSharp80Person.FirstName' cannot be assigned to --it is read only
            var thirdPerson = new CSharp80PersonV2("Danny", "Bob");
            thirdPerson.FirstName.Should().Be("Danny");
        }
    }
}
