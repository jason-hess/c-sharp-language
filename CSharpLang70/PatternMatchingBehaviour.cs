using System;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework.Internal;

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
            var r = new RectangleStruct() { Length = 1 };
            object o = r;
            if (o is RectangleStruct)
            {
                var y = (RectangleStruct)o;
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
            var r = new RectangleStruct() { Length = 1 };
            object o = r;
            if (o is RectangleStruct y)
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

        [Test]
        public void NullSwitchBehaviour()
        {
            Thing aThing = null;
            switch (aThing)
            {
                case Thing theThing:
                    theThing.Should().NotBeNull();
                    break;
                default:
                    false.Should().BeTrue();
                    break;
            }
        }

        [Test]
        public void SHould()
        {
            int[] values = { 2, 4, 6, 8, 10 };
            ShowCollectionInformation(values);

            var names = new List<string>();
            names.AddRange(new string[] { "Adam", "Abigail", "Bertrand", "Bridgette" });
            ShowCollectionInformation(names);

            List<int> numbers = null;
            ShowCollectionInformation(numbers);
        }

        private static void ShowCollectionInformation(object coll)
        {
            switch (coll)
            {
                case Thing thing:
                    break;
                case Array arr:
                    Console.WriteLine($"An array with {arr.Length} elements.");
                    break;
                case IEnumerable<int> ieInt:
                    Console.WriteLine($"Average: {ieInt.Average(s => s)}");
                    break;
                case IList list:
                    Console.WriteLine($"{list.Count} items");
                    break;
                case IEnumerable ie:
                    string result = "";
                    foreach (var e in ie)
                        result += "${e} ";
                    Console.WriteLine(result);
                    break;
                case null:
                    // Do nothing for a null.
                    break;
                default:
                    Console.WriteLine($"A instance of type {coll.GetType().Name}");
                    break;
            }
        }

        [Test]
        public void CSharp70SwitchCaseWhenExample()
        {
            Shape sh = null;
            Shape[] shapes = { new Square(10), new Rectangle(5, 7),
                sh, new Square(0), new Rectangle(8, 8),
                new Circle(3) };
            foreach (var shape in shapes)
                ShowShapeInfo(shape);
        }

        private static void ShowShapeInfo(Shape sh)
        {
            switch (sh)
            {
                // Note that this code never evaluates to true.
                case Shape shape when shape == null:
                    Console.WriteLine($"An uninitialized shape (shape == null)");
                    break;
                case null:
                    Console.WriteLine($"An uninitialized shape");
                    break;
                case Shape shape when sh.Area == 0:
                    Console.WriteLine($"The shape: {sh.GetType().Name} with no dimensions");
                    break;
                case Square sq when sh.Area > 0:
                    Console.WriteLine("Information about square:");
                    Console.WriteLine($"   Length of a side: {sq.Side}");
                    Console.WriteLine($"   Area: {sq.Area}");
                    break;
                case Rectangle r when r.Length == r.Width && r.Area > 0:
                    Console.WriteLine("Information about square rectangle:");
                    Console.WriteLine($"   Length of a side: {r.Length}");
                    Console.WriteLine($"   Area: {r.Area}");
                    break;
                case Rectangle r when sh.Area > 0:
                    Console.WriteLine("Information about rectangle:");
                    Console.WriteLine($"   Dimensions: {r.Length} x {r.Width}");
                    Console.WriteLine($"   Area: {r.Area}");
                    break;
                case Shape shape when sh != null:
                    Console.WriteLine($"A {sh.GetType().Name} shape");
                    break;
                default:
                    Console.WriteLine($"The {nameof(sh)} variable does not represent a Shape.");
                    break;
            }

                
        }

        [Test]
        public void IsBehaviour()
        {
            object o = new Thing();
            if (o is Thing aThing)
            {
                aThing.ToString();
            }
        }

        public class Thing
        {

        }

        public struct RectangleStruct
        {
            public int Length;
        }

        public abstract class Shape
        {
            public abstract double Area { get; }
            public abstract double Circumference { get; }
        }

        public class Rectangle : Shape
        {
            public Rectangle(double length, double width)
            {
                Length = length;
                Width = width;
            }

            public double Length { get; set; }
            public double Width { get; set; }

            public override double Area
            {
                get { return Math.Round(Length * Width, 2); }
            }

            public override double Circumference
            {
                get { return (Length + Width) * 2; }
            }
        }

        public class Square : Rectangle
        {
            public Square(double side) : base(side, side)
            {
                Side = side;
            }

            public double Side { get; set; }
        }

        public class Circle : Shape
        {
            public Circle(double radius)
            {
                Radius = radius;
            }

            public double Radius { get; set; }

            public override double Circumference
            {
                get { return 2 * Math.PI * Radius; }
            }

            public override double Area
            {
                get { return Math.PI * Math.Pow(Radius, 2); }
            }
        }

    }
}
