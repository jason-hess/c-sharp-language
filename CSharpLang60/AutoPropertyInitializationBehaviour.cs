using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang60
{
    public class AutoPropertyInitializationBehaviour
    {
        [Test]
        public void Should()
        {
            var underTest = new AutoPropertyInitializationExample();
            underTest.ReadOnlyAutoProperty.Should().Be(10);
            underTest.AutoProperty.Should().Be(10);
        }

        public class AutoPropertyInitializationExample
        {
            public int AutoProperty { get; set; } = 10;
            public int ReadOnlyAutoProperty { get; } = 10;
        }
    }

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
            public override string ToString() => "Value";
            public string ReadOnlyAutoProperty => "Value";
        }
    }
}