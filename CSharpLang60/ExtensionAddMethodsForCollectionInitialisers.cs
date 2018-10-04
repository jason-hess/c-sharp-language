using FluentAssertions;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpLang60
{
    /// <summary>
    /// C# 6 adds the ability to add collection initializer support to classes that
    /// don't have an Add method but that you wish to use a collection initializer with.  
    /// For example, if you have a class called People that is defined elsewhere
    /// then you can define an Add extension method in your own code to allow you
    /// to use an extension initializer.
    /// </summary>
    public class ExtensionAddMethodsForCollectionInitializerBehaviour
    {
        [Test]
        public void CanNowUseExtensionMethodsToEnableCollectionInitializer()
        {
            // Because the PeopleExtensions class implements an Add method for People, 
            // and People implements IEnumerable<> then a Collection Initializer can 
            // be used.
            var people = new People
            {
                new Person()
            };

            people.Count().Should().Be(1);
        }

        public class People : IEnumerable<Person>
        {
            readonly List<Person> _people = new List<Person>();

            public void AddPerson(Person person) => _people.Add(person);

            public IEnumerator<Person> GetEnumerator() => _people.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }

    public static class PeopleExtensions
    {
        public static void Add(this ExtensionAddMethodsForCollectionInitializerBehaviour.People people, Person person)
        {
            people.AddPerson(person);
        }
    }
}
