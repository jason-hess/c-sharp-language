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
    ///
    /// Pattern matching is a feature that allows you to implement method dispatch on
    /// properties other than the type of an object. You're probably already familiar
    /// with method dispatch based on the type of an object. In object-oriented
    /// programming, virtual and override methods provide language syntax to
    /// implement method dispatching based on an object's type.
    /// Base and Derived classes provide different implementations.
    /// Pattern matching expressions extend this concept so that you can
    /// easily implement similar dispatch patterns for types and data elements
    /// that aren't related through an inheritance hierarchy
    ///
    /// Pattern matching supports is expressions and switch expressions.
    /// Each enables inspecting an object and its properties to determine
    /// if that object satisfies the sought pattern. You use the when keyword
    /// to specify additional rules to the pattern.
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
        /// Note: The order of the switch cases now matter!  The first to match is executed, and the rest are skipped.
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

        [Test]
        public void IsNull()
        {
            string aString = null;
            aString = null;
            if (aString is null)
            {

            }
        }
    }

    public struct Rectangle
    {
        public int Length;
    }
}
