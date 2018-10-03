using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang70
{
    /// <summary>
    /// C# 7.0 added support for binary literals in your code.  Similar to 0x you can now use 0b to
    /// set binary values.
    /// </summary>
    public class BinaryLiteralsBehaviour
    {
        [Test]
        [TestCase(0b0, 0, TestName = "Literal 0b0 = 0")]
        [TestCase(0b1, 1, TestName = "Literal 0b1 = 1")]
        [TestCase(0b10, 2, TestName = "Literal 0b10 = 2")]
        [TestCase(0b11, 3, TestName = "Literal 0b11 = 3")]
        public void BinaryLiteralsCanBeSpecified(int binaryValue, int expectedValue)
        {
            binaryValue
                .Should()
                .Be(expectedValue);
        }
    }
}
