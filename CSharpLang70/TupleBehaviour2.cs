using FluentAssertions;
using NUnit.Framework;
using System;
using TupleAssembly;

namespace CSharpLang
{
    /// <summary>
    /// Tuple element names can now be specified.  Prior to C# 7.0 Tuples existed but their 
    /// members could only be accessed through Item1, Item2, and so on.  Tuple.Create
    /// is no longer needed.
    /// 
    /// Tuples are useful when you need a new type, but don't need to go to the effort
    /// of creating a new `class` or `struct`.
    /// 
    /// You must include the System.ValueTuple NuGet package on platforms that do not
    /// include it.  This is because the .NET Standard and C# rev independently of each other.
    /// 
    /// Guideline: Tuples are most useful for `private` and `internal` methods.  Perhaps
    /// for public APIs, still return `struct`s or `class`es.  Tuples don't support 
    /// much unlike classes and structs.
    /// 
    /// If you want data and behaviour then use a class, otherwise if you want to return
    /// more than one value from a method, you could return a Tuple.
    /// </summary>
    public class TupleBehaviour2
    {
        /// <summary>
        /// .NET 4.0 introduced `System.Tuple` which is a `class` that was 
        /// intended as a way to store multiple values without the need 
        /// for creating a new `class` or `struct`
        /// </summary>
        [Test]
        public void SystemTuple()
        {
            var tuple = new Tuple<int, int>(1, 2);
            // Tuple elments in Sytem.Tuple are accessed through .Item1, .Item2, etc.

            Assert.AreNotEqual(tuple.Item1, tuple.Item2);
        }

        /// <summary>
        /// Tuples can also be created with Tuple.Create()
        /// </summary>
        [Test]
        public void SystemTupleCreate()
        {
            var tuple = Tuple.Create("A", "B", "C");
            var secondTuple = new Tuple<string, string, string>("A", "B", "C");

            Assert.AreEqual(tuple, secondTuple);
            Assert.AreEqual(tuple.Item1, secondTuple.Item1);
        }

        /// <summary>
        /// The usefulness of Tuples is that they can be quickly and briefly
        /// used to return complex data from a method.
        /// </summary>
        [Test]
        public void TuplesCanBeReturnedFromMethods()
        {
            var metrics = SumAndCount(1, 2, 3);
            var sum = metrics.Item1;
            var count = metrics.Item2;
        }

        private static Tuple<int, int> SumAndCount(params int[] ints)
        {
            var sum = 0;
            var count = 0;
            foreach (var i in ints)
            {
                sum += i;
                count += 1;
            }
            return new Tuple<int, int>(sum, count);
        }

        /// <summary>
        /// .NET 4.7 and the out of band System.ValueTuple (via NuGet) added
        /// a `struct` alternative to the `System.Tuple` class to improve performance.
        /// </summary>
        [Test]
        public void SystemValueTuple()
        {
            var valueTuple = new ValueTuple<int, int>(1, 2);
            Assert.AreEqual(valueTuple.Item1 + 1, valueTuple.Item2);
        }

        /// <summary>
        /// Unlike `System.Tuple`, `System.ValueTuple` is mutable.
        /// </summary>
        [Test]
        public void ValueTuplesAreMutable()
        {
            var tuple = ValueTuple.Create(1, 2);
            tuple.Item1 = 2;

            Assert.AreEqual(tuple.Item1, tuple.Item2);
        }

        /// <summary>
        /// C# 7.0 added language support for `System.ValueTuple`
        /// </summary>
        [Test]
        public void CSharp7Support()
        {
            (int, int) tuple = (1, 2); // Creates a ValueTuple under the hood
            Assert.AreEqual(tuple.Item1 + 1, tuple.Item2);
        }

        /// <summary>
        /// In the C# 7.0 Tuple support, Tuple elements can be given names.  These names
        /// are compiled down to .Item1, .Item2, etc.
        /// </summary>
        /// <remarks>Under the hood the compiler is adding the TupleElementNamesAttribute
        /// so that these Tuple names can be used across assemblies.</remarks>
        [Test]
        public void TupleElementNamesCanBeSpecified()
        {
            var tuple = (Sum: 1, Count: 2);
            Assert.AreEqual(tuple.Sum + 1, tuple.Count);
        }

        [Test]
        public void CanBeUsedAcrossAssemblies()
        {
            var tupleGenerator = new TupleClass();
            var tuple = tupleGenerator.TupleMethod();

            var tupleElement = tuple.Alpha;

            Assert.AreEqual("1", tuple.Alpha);
        }

        /// <summary>
        /// The Tuple elmeent names on the RHS of the assignment are forgotten
        /// </summary>
        [Test]
        public void TupleElementNamesCanBeRespecified()
        {
            (string Alpha, string Beta) tuple = (Gamma: "1", Delta: "2");

            tuple.Alpha.Should().Be("1");
        }

        [Test]
        public void TuplesCanBeDeconstructedIntoVariables()
        {
            // Declare two variables Alpha and Beta from the .Item1 and .Item2 variables of the Tuple:
            (string alpha, string beta) = ("1", "2");

            alpha.Should().Be("1");
        }

        [Test]
        public void TuplesCanBeDeconstructedImplicitly()
        {
            // Both declarations achieve the same thing
            (var theCount, var theSum) = (1, 2);
            var (alpha, beta) = ("1", "2");

            alpha.Should().Be("1");
        }

        [Test]
        public void Test()
        {
            // Tuples can also be deconstructed on Classes by providing a `Deconstruct` method.
            // This method name must be `Deconstruct` and provide output parameters and return void.
            var deconstructableThing = new TupleAssembly.TupleClass();
            // The compiler will resolve the following to a `void` method in TupleClass that
            // has two `out` parameters.  
            // Note: You are not bound by the names of the parameters on the Deconstruct method
            var (jack, jones) = deconstructableThing;
        }

        [Test]
        public void SystemValueTuplesAreMutable()
        {
            // Performance:
            // Unlike the original System.Tuple in .NET 4.0 the System.ValueTuple (available via NuGet
            // or .NET 4.7) is a Struct which C# requires for its lanugage support.  All the Tuples
            // created with the C# Tuple language support are `ValueTuple`s.
            var valueTuple = ValueTuple.Create(1);
            // Note: ValueTuples are mutable whereas `System.Tuple`s were immutable



            // var yetAnotherTuple = ("1", ToString: "2"); // CS8126  Tuple element name 'ToString' is disallowed at any position.	

            // Tuple assignment:
            // Tuples can be assigned to each other if 1) They have the same cardinality 2) Implicit conversions exist between the same elements in each Tuple:
            var firstAssignmentTuple = (1, "1");
            var secondAssignmentTuple = (Value: 2, StringValue: "2");
            var thirdAssignmentTuple = (TheValue: 3, "3");
            var fourthAssignmentTuple = (4L, "4");

            fourthAssignmentTuple = thirdAssignmentTuple = secondAssignmentTuple = firstAssignmentTuple;
            // Results in (long, string)

            // fourthAssignmentTuple = (1, 2, 3); // CS0029  Cannot implicitly convert type '(int, int, int)' to '(long, string)'    

            // https://docs.microsoft.com/en-us/dotnet/csharp/tuples#comments-container

        }

        /// <summary>
        /// Methods returning Tuples are useful
        /// </summary>
        /// <returns></returns>
        public (string Alpha, string Beta) TupleReturningMethod()
        {
            return ("1", "2");
        }


    }
}
