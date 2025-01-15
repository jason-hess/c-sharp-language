using NUnit.Framework;

namespace CSharpLang90
{
    public class InitOnlySetterBehaviour
    {
        /// <summary>
        /// In C# 9.0 you can define a property with `init` access which means it can be set
        /// at initialisation and then read-only from there on.
        /// </summary>
        public class InitOnlySetters
        {
            public string? Name { get; init; }
        }
        
        [Test]
        public void Should()
        {
            var thing = new InitOnlySetters { Name = "Thing" };
            // thing.Name = "Another Thing"; // Error CS8852  Init - only property or indexer 

            // `with` only works for Record types
            //var anotherThing = thing with { Name = "Another Thing" }; // Error CS8858  The receiver type 'InitOnlySetterBehaviour.InitOnlySetters' is not a valid record type.
        }

        public struct SupportedByStructToo
        {
            public string? Name { get; init; }
        }

        /// <summary>
        /// Init only setters can be set in subclass constructors
        /// </summary>
        public class Subclass : InitOnlySetters
        {
            public Subclass()
            {
                Name = "Hello";
            }

            public void Other()
            {
                // Name = "Goodbye"; // Error CS8852  Init - only property or indexer 'InitOnlySetterBehaviour.InitOnlySetters.Name' can only be assigned in an object initializer, or on 'this' or 'base' in an instance constructor or an 'init' accessor.
            }
        }
    }
}
