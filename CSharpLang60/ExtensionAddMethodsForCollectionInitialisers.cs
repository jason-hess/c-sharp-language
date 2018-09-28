using FluentAssertions;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpLang60
{
    public class ExtensionAddMethodsForCollectionInitialisersBehaviour
    {
        [Test]
        public void CanNowUseExtensionMethodsToEnableCollectionInitializers()
        {
            // Because the PeopleExtensions class implements an Add method for People, 
            // and People implements IEnumerable<> then a Collection Initializer can 
            // be used.
            People people = new People
            {
                new Person()
            };

            people.Count().Should().Be(1);
        }

        public class People : IEnumerable<Person>
        {
            List<Person> _people = new List<Person>();

            public void AddPerson(Person person) => _people.Add(person);

            public IEnumerator<Person> GetEnumerator() => _people.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }

    public static class PeopleExtensions
    {
        public static void Add(this ExtensionAddMethodsForCollectionInitialisersBehaviour.People people, Person person)
        {
            people.AddPerson(person);
        }
    }
}
