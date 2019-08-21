using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang20
{
    /// <summary>
    /// http://www.yoda.arachsys.com/csharp/csharp2/delegates.html
    ///
    /// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history
    /// </summary> 
    public class GenericsBehaviour
    {
        // generics avoid the runtime cost of casts or boxing
        // Generic classes and methods combine reusability, type safety and efficiency in a way that
        // their non-generic counterparts cannot. Generics are most frequently used with collections
        // and the methods that operate on them. 
        public class GenericContainer<T> 
        {
            private readonly T _containedInstance;

            public GenericContainer(T instance)
            {
                _containedInstance = instance;
            }

            public T Value
            {
                get
                {
                    return _containedInstance;
                }
            }
        }

        [Test]
        public void CanInstantiateClassWithVariousTypes()
        {
            GenericContainer<int> container = new GenericContainer<int>(10);
            container.Value.Should().Be(10);
        }
    }
}