﻿using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpLang60
{
    /// <summary>
    /// C# 6.0 adds support for indexed collection initialization.  Prior to C# 6.0 you
    /// could have a sequence style collection initializer (for List) but could not
    /// initialize a Dictionary with an initializer.
    /// </summary>
    public class IndexInitializerBehaviour
    {
        [Test]
        public void CanNowInitializeIndexedCollections()
        {
            // also useful for when indexer takes more than one parameter
            var dictionary = new Dictionary<int, string>
            {
                [1] = "one",
                [2] = "two",
                [3] = "three",
            };

            // previous format
            var dictionaryToo = new Dictionary<int, string>
            {
                { 1, "one"}
            };
        }

        [Test]
        public void InPreviousVersionsYouCouldInitialiseSequenceStyleCollections()
        {
            var list = new List<string>()
            {
                "one", "two", "three"
            };
        }
    }
}
