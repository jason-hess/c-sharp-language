using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpLang60
{
    /// <summary>
    /// C# 6.0 adds support for indexed collection initialization.  Prior to C# 6.0 you
    /// could have sequence style collection initializers (for List) but could not
    /// initialize a Dictionary with an initializer.
    /// </summary>
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
