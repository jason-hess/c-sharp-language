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

            // in C# 7.0
            if (int.TryParse("1", out int theResult))
            {
                
            }
        }
    }
}
