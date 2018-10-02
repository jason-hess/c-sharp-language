using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang70
{
    /// <summary>
    /// C# 7.0 allows you to include the underscore in numeric literals (any numeric literal!) for readability.
    /// </summary>
    public class DigitSeparatorBehaviour
    {
        [Test]
        [TestCase(0b1010_1010, 0b10101010, TestName = "Literal 0b1010_1010 = 0b10101010")]
        [TestCase(10_000_000, 10000000, TestName = "Literal 10_000_000 = 10000000")]
        [TestCase(10.000_000_000, 10.000000000, TestName = "Literal 10.000_000_000 = 10.000000000")]
        public void Should(object value, object expectedValue)
        {
            value.Should().Be(expectedValue);
        }

        [Test]
        public void CanBeUsedWithDecimal()
        {
            123_456M.Should().Be(123456M);
        }
    }
}
