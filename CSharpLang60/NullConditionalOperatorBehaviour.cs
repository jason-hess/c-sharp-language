using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang60
{
    /// <summary>
    /// C# 6.0 adds the null conditional operator `?.` which short-circuits and 
    /// returns null if its LHS argument is null.
    /// </summary>
    public class NullConditionalOperatorBehaviour
    {
        [Test]
        [TestCase(null, null)]
        [TestCase("bob", 3)]
        public void ShouldReturnNullWhenOperatingOnNull(string underTest, int expectedLength)
        {
            // If `underTest` is `null`, then `?.` should short-circuit and return null
            underTest?.Length.Should().Be(expectedLength);
        }


        [Test]
        [TestCase(null, null)]
        [TestCase("bob", 3)]
        public void ShouldReturnTypeOfFullExpressionEvenWhenNull(string underTest, int expectedLength)
        {
            // If `underTest` is `null`, then `?.` should short-circuit and return null
            underTest?.Length.GetType().Should().Be(typeof(int));
        }
    }

    /// <summary>
    /// `string`s can now be composed without need for the `+` operator
    /// </summary>
    public class StringInterpolationBehaviour
    {
        [Test]
        public void ShouldInterpolate()
        {
            int x = 1;
            int y = 2;
            $"x: {x} y: {y}".Should().Be("x: 1 y: 2");
        }
    }
}