using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang71
{
    public class TupleBehaviour
    {
        /// <summary>
        /// In C# 7.1 you can specify Tuple element names from the names of the variables
        /// used to initialise the Tuple.
        /// </summary>
        [Test]
        public void TupleElementNamesCanBeSpecifiedImplicitly()
        {
            var count = 1;
            var sum = 2;
            var tuple = (count, sum);
            tuple.count.Should().Be(count);
        }

        /// <summary>
        /// In C# 7.1 Tuple elements will still receive default names (e.g. Item1) 
        /// if not specified or projected.
        /// </summary>
        [Test]
        public void TupleElementNamesAreOnlySpecifiedImplicitlyIfSpecified()
        {
            var stringContent = "The answer to everything";
            var mixedTuple = (42, stringContent);

            mixedTuple.Item1.Should().Be(42);
            mixedTuple.stringContent.Should().Be(stringContent);
        }

        /// <summary>
        /// A name is only projected if it 
        /// 1) is specified and unique 
        /// 2) is not "ToString", "ItemX", or "Rest" (case sensitive)
        /// </summary>
        [Test]
        public void ATupleElementNameIsProjectedOnlyIfItIsUniqueAndNotReserved()
        {
            // var tuple = (ToString: "1", 42); // CS8126  Tuple element name 'ToString' is disallowed at any position.	

            var ToString = "1";
            var anotherTuple = (theCount: 10, ToString); // Still allowed so that C# 7.0 code will not break, but the second element will be called Item2
            var firstElement = anotherTuple.theCount;
            var secondElement = anotherTuple.Item2;
        }
    }
}
