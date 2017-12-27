using System;
using NUnit.Framework;

namespace CSharpLang
{
    public class Class1
    {
        /// <summary>
        /// In C# 7.0 you now can declare a variable for an out parmeter inline
        /// </summary>
        [Test]
        public void CanInlineDeclareOutParameterVariables()
        {
            // prior to C# 7.0
            int result;
            if (int.TryParse("1", out result))
            {
                
            }

            // in C# 7.0 the out variable can be declared inline.
            // It's scope is leaked out to the outer scope
            if (int.TryParse("1", out int theResult))
            {
                
            }
            Console.WriteLine(theResult);

            // in C# 7.0 you can also declare an implicit (i.e. "var") variable
            if (int.TryParse("1", out var aResult))
            {
                
            }
            Console.WriteLine(aResult);

        }

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
        /// </summary>
        [Test]
        public void LanguageSupportForTuples()
        {
            // prior to C# 7.0
            var firstTuple = new Tuple<int, int>(1, 2);
            var secondTuple = Tuple.Create(1, 2);
            var value = secondTuple.Item2 + secondTuple.Item1;

            // In C# 7.0 Tuples are now understood in the language:
            var thirdTuple = (1, 2);
            value = thirdTuple.Item1 + thirdTuple.Item2;

            // Tuple member names can also be specified
            var fourthTuple = (Alpha: "1", Beta: "2");
            var stringValue = $"{fourthTuple.Alpha}{fourthTuple.Beta}";
            // Note: These names are only preserved at compile time
            // Question: Do they work across assmeblies?

            // Tuple member names can be re-specified
            (string Alpha, string Beta) fifthTuple = ("1", "2");
            Console.WriteLine(fifthTuple.Alpha);

            // Methods can specify Tuple member names
            var sixthTuple = TupleReturningMethod();
            var member = sixthTuple.Alpha;
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
