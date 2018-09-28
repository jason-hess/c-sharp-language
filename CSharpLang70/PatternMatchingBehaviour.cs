using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CSharpLang70
{
    /// <summary>
    /// Pattern Matching in C# 7.0 now make it easier to write despatching algorithms.
    /// </summary>
    public class PatternMatchingBehaviour
    {
        /// <summary>
        /// C# 7.0 adds support for using `is` and declaring a variable at the same time.
        /// Previously you would have writting if(value is int) { int x = (int)value; } but
        /// now you can write `if(value is int x) {}`
        /// </summary>
        [Test]
        public void Should()
        {
            object o = 10;
            if (o is int x)
            {
                x++;
            }
            else if (o is IEnumerable<int> list)
            {
                list.Count();
            }
        }

        /// <summary>
        /// Note the special value cases in addition to the type cases
        /// </summary>
        [Test]
        public void SimilarlyWithSwitch()
        {
            object o = 10;
            switch (o)
            {
                case 0:
                    break;
                case null:
                    break;
                case int x:
                    x++;
                    break;
                case IEnumerable<int> list:
                    list.Count();
                    break;
            }
        }

        [Test]
        [TestCase(10)]
        [TestCase(new int[0])]
        [TestCase(new[] { 1, 2, 3 })]
        public void WhenCanAlsoBeUsedInPattenMatching(object o)
        {
            switch (o)
            {
                case int x:
                    x++;
                    break;
                case IEnumerable<int> list when list.Any():
                    list.Count();
                    break;
                case IEnumerable<int> list:
                    list.Any();
                    break;

            }
        }
    }
}
