using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace CSharpLang70
{
    /// <summary>
    /// Pattern Matching in C# 7.0 now make it easier to write dispatching algorithms.
    /// This allows you to implement dispatch patterns for types and data elements 
    /// that are not related through an inheritance hierarchy.
    /// </summary>
    public class PatternMatchingBehaviour
    {
        /// <summary>
        /// C# 7.0 adds support for using `is` and declaring a variable at the same time.
        /// In previous versions of C# the `is` operator would return true if the operand
        /// was of the type of the RHS (e.g. x is int returns true if x's Type is an int).
        /// Before C# 7.0 you would have writing if(value is int) { int x = (int)value; } but
        /// now you can write `if(value is int x) {}`
        /// </summary>
        [Test]
        public void Should()
        {
            object o = GetValue();
            if (o is int x)
            {
                x++;
            }
            else if (o is float y && y > 10)
            {
                y++;
            }
            else if (o is IEnumerable<int> list)
            {
                list.Count();
            }
        }

        private static object GetValue()
        {
            Something();
            return 10;
        }

        public static void Something()
        {
            int caseSwitch = 1;

            switch (caseSwitch)
            {
                case 1:
                    Console.WriteLine("Case 1");
                    break;
                case 2:
                    Console.WriteLine("Case 2");
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }

        /// <summary>
        /// Note the special value cases in addition to the type cases.
        ///
        /// Note: The order of the switch cases now matter!
        /// Note: The default case is always evaluated last no matter what order it appears in
        /// within the case statement.
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
                case int y when y > 10:
                    y++;
                    break;
                case int x:
                    x++;
                    break;
                case IEnumerable<int> list when list.Any():
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

        [Test]
        public void WorksWithStruct()
        {
            // Previously if you wanted to type check a struct, it would be copied
            // to a new variable
            var r = new Rectangle() {Length = 1};
            object o = r;
            if (o is Rectangle)
            {
                var y = (Rectangle) o;
                y.Length++;
                y.Length.Should().Be(2);

            }

            r.Length.Should().Be(1);

        }

        [Test]
        public void WorksWithStruct2()
        {
            // Previously if you wanted to type check a struct, it would be copied
            // to a new variable
            var r = new Rectangle() { Length = 1 };
            object o = r;
            if (o is Rectangle y)
            {
                y.Length++;
                y.Length.Should().Be(2);

            }

            r.Length.Should().Be(1);

        }
    }

    public struct Rectangle
    {
        public int Length;
    }
}
