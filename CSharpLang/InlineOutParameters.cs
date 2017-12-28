using System;
using NUnit.Framework;

namespace CSharpLang
{
    public class OutParmeterExamples 
    {
        // Thought: TryParse is an interesting method because it seems to 
        // have more than one responsibility: 1) Is the string convertable
        // 2) Convert the string
        public void Test(out string x)
        {
            x = "";
        }

        // Question: When are out parameters useful?
        // Possible Answer: When you are already using the return value and want to 
        // return more than one value.  The other otpions would be to use a Tuple,
        // or to return an object, but that could blur the lines.  For instance,
        // TryParse has one value it returns (whether the string is convertable)
        // and a separate concept which is the converted value.
    }

    /// <summary>
    /// In C# 7.0 you now can declare a variable for an out parmeter inline
    /// </summary>
    public class InlineOutParameters
    {
        /// <summary>
        /// Prior to C# 7, variables for out parameters were declared separately
        /// </summary>
        [Test]
        public void PriorOutParameterVariableDeclaration()
        {
            int result; // note: this could be initialized
            if (int.TryParse("1", out result))
            {

            }

            int anotherResult = 0;
            if (int.TryParse("1", out anotherResult))
            {
                
            }
        }

        /// <summary>
        /// In C# 7 out parameter variables can be declared inline.
        /// Their scope leaks to the outer scope.
        /// </summary>
        [Test]
        public void Can()
        {
            // in C# 7.0 the out variable can be declared inline.
            if (int.TryParse("1", out int theResult))
            {

            }
            // It's scope is leaked out to the outer scope
            Console.WriteLine(theResult);

            // in C# 7.0 you can also declare an implicit (i.e. "var") variable
            // for an out parameter
            if (int.TryParse("1", out var aResult))
            {

            }
            Console.WriteLine(aResult);
        }
    }
}