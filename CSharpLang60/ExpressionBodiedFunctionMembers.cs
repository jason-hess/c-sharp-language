using FluentAssertions;
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
            var toStringValue = "hello";
            new ExpressionBodiedFunctionMembersExample(toStringValue)
                .ToString()
                .Should().Be(toStringValue);
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

            public override string ToString() => $"{_value}";
            public string ReadOnlyAutoProperty => "Value";
        }
    }
}