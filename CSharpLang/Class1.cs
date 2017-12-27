using System;
using NUnit.Framework;

namespace CSharpLang
{
    public class Class1
    {
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

            // in C# 7.0 you can also declare an implicit variable
            if (int.TryParse("1", out var aResult))
            {
                
            }
            Console.WriteLine(aResult);

        }
    }
}
