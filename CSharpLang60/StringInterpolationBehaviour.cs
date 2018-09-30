using System;
using System.Globalization;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang60
{
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

        [Test]
        public void AnyExpressionCanBeUsedInStringInterpolation1()
        {
            $"{2 + 2}{4.ToString().Length}".Should().Be("41");
        }

        [Test]
        public void AnyExpressionCanBeUsedInStringInterpolation2()
        {
            var list = new[] {1, 2, 3};
            $"{(from item in list select item).Sum()}".Should().Be("6");
        }

        [Test]
        public void MultiLineStringsCanBeCombinedWithInterpolatedStrings()
        {
            var list = new[] { 1, 2, 3 };
            // The expression is evaluated, but I can also have a newline in the string
            $@"{(from item in list
                 select item).Sum()}
".Should().Be("6" + Environment.NewLine);
        }

        /// <summary>
        /// Using the `:` in the expression allows you to include format specifiers
        /// </summary>
        [Test]
        public void AllTheUsualFormatExpressionsCanStillBeUsed()
        {
            $"{2:C}".Should().Be("$2.00");
        }

        /// <summary>
        /// The object produced from a string interpolation is a type that has an 
        /// implicit conversion to either String or FormattableString.
        /// </summary>
        [Test]
        public void InterpolatedStringIsEitherAStringOrFormatableString()
        {
            string result = $"{2}";
            FormattableString formattableResult = $"{2}";
        }

        /// <summary>
        /// Using the FormattableString, you can format for other cultures rather
        /// than the one that hte code is executing on
        /// </summary>
        [Test]
        public void CultureSpecificFormatingCanBeDone()
        {
            FormattableString theString = $"{2:C}";
            theString.ToString(CultureInfo.GetCultureInfo("de-de")).Should().Be("2,00 €");
        }
    }
}