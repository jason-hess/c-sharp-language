using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang60
{
    /// <summary>
    /// Functions and Read-Only Auto Properties can now be implemented with an expression
    /// bodied function.  This is useful for then you have a function or property that 
    /// is implemented with a single line of code.
    /// </summary>
    public class ExpressionBodiedFunctionMemberBehaviour
    {
        [Test]
        public void Should()
        {
            var stringValue = "hello";
            new ExpressionBodiedFunctionMembersExample(stringValue)
                .MethodImplementedWithExpressionBody()
                .Should().Be(stringValue);
        }

        /// <summary>
        /// Only functions and read-only auto-properties can be given expression bodied functions
        /// </summary>
        public class ExpressionBodiedFunctionMembersExample
        {
            private readonly string _value;

            public ExpressionBodiedFunctionMembersExample(string value)
            {
                _value = value;
            }

            public string MethodImplementedWithExpressionBody() => $"{_value}";
            public string ReadOnlyAutoPropertyImplementedWithExpressionBody => "Value";
        }
    }
}