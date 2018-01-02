using NUnit.Framework;

namespace CSharpLang60
{
    /// <summary>
    /// Functions and Read-Only Auto Properties can now be implemented with an expression
    /// bodied function.
    /// </summary>
    public class ExpressionBodiedFunctionMembers
    {
        [Test]
        public void Should()
        {
            
        }

        public class ExpressionBodiedFunctionMembersExample
        {
            public override string ToString() => $"{base.ToString()}";
            public string ReadOnlyAutoProperty => "Value";
        }
    }
}