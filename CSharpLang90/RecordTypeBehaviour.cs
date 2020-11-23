﻿using FluentAssertions;
using NUnit.Framework;
using System;

namespace CSharpLang90
{
    /// <summary>
    /// Records are immutable equatable types.  Historically in C# mutable value and reference
    /// types are mutable and value types are passed by copy (and it is encouraged that value
    /// types are immutable and marked readonly)
    /// </summary>
    /// <remarks>Records are compiled into classes</remarks>
    public record Person(string FirstName, string LastName);

    /// <summary>
    /// The above `positional record` declaration is the same as the following full declartion.
    /// Positional record declarations get a Deconstructor defined but full declarations do not.
    /// </summary>
    public record FullPerson
    {
        public FullPerson(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }
        public string LastName { get; }
    }

    /// <summary>
    /// Previously you would have had to write a fair bit of code to define an immutable class
    /// </summary>
    public class CSharp80Person : IEquatable<CSharp80Person>
    {
        public CSharp80Person(string firstName, string lastName) =>
               (FirstName, LastName) = (firstName, lastName);

        public string FirstName { get; }
        public string LastName { get; }

        public override bool Equals(object? obj)
            => Equals(obj as CSharp80Person);

        public bool Equals(CSharp80Person? other)
        {
            if (other == null) return false;
            return (FirstName, LastName) == (other.FirstName, other.LastName);
        }

        public override int GetHashCode() => (FirstName, LastName).GetHashCode();

    }

    /// <summary>
    /// Records can inherit from other records
    /// </summary>
    public record AgedPerson(string FirstName, string LastName, int Age) : Person(FirstName, LastName);

    /// <summary>
    /// Records can be sealed 
    /// </summary>
    public sealed record SealedPerson(string FirstName, string LastName, int Age) : Person(FirstName, LastName);

    /// <summary>
    /// Records can have members defined and can define members instead of the default members.
    /// If a record type has a method that matches the signature of any synthesized method, the 
    /// compiler doesn't synthesize that method. 
    /// </summary>
    public record TallPerson(string Name, int HeightInMetres)
    {
        public int HeightInCentimetres => HeightInMetres * 100;

        /// <summary>
        /// By default GetHashCode is generated by the compiler to return a combination of
        /// the hash code values of all properties on the Record.  You can explicitly provide
        /// your own implementations of the default methods generated by the compiler for 
        /// Records.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => HeightInMetres;
    }

    [TestFixture]
    public class RecordBehaviour
    {
        [Test]
        public void ShouldBeImmutable()
        {
            var firstPerson = new Person("Billy", "Bob");
            firstPerson.FirstName.Should().Be("Billy");
            // firstPerson.FirstName = "Andy"; // Error CS8852  Init - only property or indexer 'Person.FirstName' can only be assigned in an object initializer, or on 'this' or 'base' in an instance constructor or an 'init' accessor.
            var secondPerson = new CSharp80Person("Cody", "Bob");
            secondPerson.FirstName.Should().Be("Cody");
            //secondPerson.FirstName = "Andy"; // Error CS0200  Property or indexer 'CSharp80Person.FirstName' cannot be assigned to --it is read only
        }

        /// <summary>
        /// Two records are equal to each other if their properties are equal and their types are the same
        /// </summary>
        [Test]
        public void ShouldBeEquatableWhenAllPropertiesAreTheSame() =>
            new Person("Bob", "Jones")
            .Should()
            .Be(new Person("Bob", "Jones"));

        public record LikePerson(string FirstName, string LastName);

        /// <summary>
        /// Two records are equal to each other if their properties are equal and their types are the same
        /// </summary>
        [Test]
        public void ShouldNotBeEquatableWhenAllPropertiesAreTheSameButTypesAreNotTheSame() =>
            new Person("Bob", "Jones")
            .Should()
            .NotBe(new LikePerson("Bob", "Jones"));

        /// <summary>
        /// Records provide default implementations for ToString(), Equals(), GetHashCode() and others
        /// </summary>
        [Test]
        public void ShouldSameHashcodeAllPropertiesAreTheSame() =>
            new Person("Bob", "Jones")
            .GetHashCode()
            .Should()
            .Be(new Person("Bob", "Jones").GetHashCode());

        [Test]
        public void ShouldHaveConsistentStringRepresentation() =>
            new Person("Beth", "Jones").ToString()
            .Should()
            .Be("Person { FirstName = Beth, LastName = Jones }");

        [Test]
        public void ShouldSupportCopyWith()
        {
            // Records can by copied with the `with` syntax
            var firstPerson = new Person("Alice", "Jones");
            // The `with` statement causes the protected copy constructor to be called
            var brother = firstPerson with { FirstName = "Bob" };
            // Identical copy
            var secondPerson = firstPerson with { };

            firstPerson.Should().Be(secondPerson);
            Equals(firstPerson, secondPerson).Should().BeTrue();

            firstPerson.Should().NotBeSameAs(secondPerson);
            ReferenceEquals(firstPerson, firstPerson).Should().BeFalse();

            // Operator overloaded to call Equals
            (firstPerson == secondPerson).Should().BeTrue();

            // Destructor is synthesised
            var (first, last) = firstPerson;
        }

        public record ExplicitCopy(string Name)
        {
            public ExplicitCopy(ExplicitCopy other)
            {
                Name = "";
            }

            // public ExplicitCopy Clone() { } // Error CS8859  Members named 'Clone' are disallowed in records.

        }

        [Test]
        public void CanProvideCopyConstructor()
        {
            // You can define your own copy constructor
            var first = new ExplicitCopy("Alice");
            var other = first with { };
            other.Name.Should().Be("");
        }
    }
}
