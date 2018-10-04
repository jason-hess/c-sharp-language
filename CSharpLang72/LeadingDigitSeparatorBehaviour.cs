using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang72
{
    /// <summary>
    /// Before C# 7.2 the digit separator within a numeric literal needed to be between two numeric values (e.g. 10_000).
    /// In C# 7.2 you can now start a numeric literal with a digit separator.
    /// </summary>
    public class DigitSeparatorCanBeginLiteral
    {
        [Test]
        public void Should()
        {
            const int numericLiteralStartingWithDigitSeparator = 0b_10_10_10;
            numericLiteralStartingWithDigitSeparator
                .Should()
                .Be(0b_10_10_10);
        }
    }

    // Span is a window onto a whole or part of an array

}
