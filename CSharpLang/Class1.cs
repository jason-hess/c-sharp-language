﻿using System;
using NUnit.Framework;

namespace CSharpLang
{
    public class Class1
    {
        /// <summary>
        /// Tuple names can now be specified.  Prior to C# 7.0 Tuples existed but their 
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
        /// more than one value from a method, you could return a Tuple
        /// </summary>
        [Test]
        public void LanguageSupportForTuples()
        {
            // prior to C# 7.0
            var firstTuple = new Tuple<int, int>(1, 2);
            var secondTuple = Tuple.Create(1, 2);
            int secondTupleValue = secondTuple.Item2 + secondTuple.Item1;

            // In C# 7.0 Tuples are now understood in the language:
            var thirdTuple = (1, 2);
            secondTupleValue = thirdTuple.Item1 + thirdTuple.Item2;

            // Tuple member names can also be specified
            var fourthTuple = (Alpha: "1", Beta: "2");
            var stringValue = $"{fourthTuple.Alpha}{fourthTuple.Beta}";
            // Note: These names are not preserved at compile time. 
            //       The complier replaces them with .Item1, .Item2, etc.
            // Question: Do they work across assmeblies?
            // Answer: Yes.  Roslyn adds the TupleElementNamesAttribute
            // so when Visual Studio consumes the assmbly it can assist with
            // IntelliSense
            var tupleThing = new TupleAssembly.TupleClass();
            var alphaValue = tupleThing.TupleMethod().Alpha;

            // Tuple member names can be re-specified
            (string Alpha, string Beta) fifthTuple = ("1", "2");
            Console.WriteLine(fifthTuple.Alpha);

            // Methods can specify Tuple member names
            var sixthTuple = TupleReturningMethod();
            var member = sixthTuple.Alpha;

            // The following will ignore the Tuple element names on the RHS of the assignment.
            (string Gamma, string Delta) seventh = (Alpha: "1", Beta: "2");

            // Deconstruction:

            // Tuples can also be deconstructed into variables by not giving a name
            // to the Tuple:
            (string Alpha, string Beta) = ("1", "2");
            // Alpha above is declared in local scope:
            var alphaCanBeAccessed = Alpha;

            // The second way to deconstruct a Tuple is with this syntax:
            var (Gamma, Delta) = ("1", "2");
            var gammaIsNowInLocalScope = Gamma;

            // Tuples can also be deconstructed on Classes by providing a `Deconstruct` method.
            // This method name must be `Deconstruct` and provide output parameters and return void.
            var deconstructableThing = new TupleAssembly.TupleClass();
            // The compiler will resolve the following to a `void` method in TupleClass that
            // has two `out` parameters.  
            // Note: You are not bound by the names of the parameters on the Deconstruct method
            var (jack, jones) = deconstructableThing;

            // Performance:
            // Unlike the original System.Tuple in .NET 4.0 the System.ValueTuple (available via NuGet
            // or .NET 4.7) is a Struct which C# requires for its lanugage support.  All the Tuples
            // created with the C# Tuple language support are `ValueTuple`s.
            var valueTuple = ValueTuple.Create(1);
            // Note: ValueTuples are mutable whereas `System.Tuple`s were immutable

            // In c# 7.1 you can specify the Tuple element names from the names of the 
            // variables used to initialise the Tuple:
            int count = 1;
            int sum = 2;
            var theTuple = (count, sum);
            Assert.AreEqual(count, theTuple.count);

            var stringContent = "The answer to everything";
            var mixedTuple = (42, stringContent);

            // A name is only projected if it 1) is specified and unique 2) is not "ToString", "ItemX", or "Rest"
            var ToString = "1";
            var anotherTuple = (theCount: count, ToString); // Still allowed so that C# 7.0 code will not break, but the second element will be called Item2
            var firstElement = anotherTuple.theCount;
            var secondElement = anotherTuple.Item2;

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
