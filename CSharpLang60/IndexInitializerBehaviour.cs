using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpLang60
{
    public class IndexInitializerBehaviour
    {
        [Test]
        public void InPreviousVersionsYouCouldInitialiseSequenceStyleCollections()
        {
            var list = new List<string>()
            {
                "one", "two", "three"
            };
        }

        [Test]
        public void CanNowInitializeIndexedCollections()
        {
            var dictionary = new Dictionary<int, string>
            {
                [1] = "one",
                [2] = "two",
                [3] = "three",
            };
        }
    }
}
