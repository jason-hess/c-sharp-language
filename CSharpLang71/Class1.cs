using NUnit.Framework;

namespace CSharpLang71
{
    public class Class1
    {
        [Test]
        public void Test()
        {
            // In c# 7.1 you can specify the Tuple element names from the names of the 
            // variables used to initialise the Tuple:
            int count = 1;
            int sum = 2;
            var theTuple = (count, sum);
            Assert.AreEqual(count, theTuple.count);

            (var theCount, var theSum) = theTuple;
            var (aCount, aSum) = theTuple;

            var stringContent = "The answer to everything";
            var mixedTuple = (42, stringContent);

            // A name is only projected if it 1) is specified and unique 2) is not "ToString", "ItemX", or "Rest"
            var ToString = "1";
            var anotherTuple = (theCount: count, ToString); // Still allowed so that C# 7.0 code will not break, but the second element will be called Item2
            var firstElement = anotherTuple.theCount;
            var secondElement = anotherTuple.Item2;
        }
    }
}
