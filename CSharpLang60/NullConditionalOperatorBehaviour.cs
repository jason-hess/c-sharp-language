using FluentAssertions;
using NUnit.Framework;
using System;

namespace CSharpLang60
{
    /// <summary>
    /// C# 6.0 adds the null conditional operator `?.` which short-circuits and 
    /// returns null if its LHS argument is null.  The type of the entire expression
    /// evaluates as if the operand of ?. was not null.
    /// </summary>
    public class NullConditionalOperatorBehaviour
    {
        [Test]
        [TestCase(null, null)]
        [TestCase("bob", 3)]
        public void ShouldReturnNullWhenOperatingOnNull(string stringUnderTest, int expectedValueOfNullConditional)
        {
            // If `underTest` is `null`, then `?.` should short-circuit and return null
            stringUnderTest?.Length.Should().Be(expectedValueOfNullConditional);
        }

        [Test]
        [TestCase(null, null)]
        [TestCase("bob", 3)]
        public void ShouldReturnTypeOfTheRightHandSideOfExpressionEvenWhenNull(string stringUnderTest, int expectedLength)
        {
            // If `underTest` is `null`, then `?.` should short-circuit and return null
            stringUnderTest?.Length.GetType().Should().Be(typeof(int));
        }

        [Test]
        public void IsUsefulToSetDefaultValues()
        {
            var person = new Person { FirstName = null };
            person = null;
            var defaultName = person?.FirstName ?? "Unknown";
        }

        [Test]
        public void NullConditionalCanInvokeMembers()
        {
            Person person = null;
            var personAsString = person?.ToString();
            personAsString.Should().BeNull();
        }

        [Test]
        public void NullConditionalIsUsefulForEventHandlersAndDelegates()
        {
            Person person = new Person();

            // in versions of C# prior to 6.0 you might write something like:

            if(person != null)
            {
                // store action as a temp variable to prevent race condition
                // if person is accessible from elsewhere
                var action = person.PerformAction;
                if(action != null)
                {
                    action();
                }
            }

            // This is what is preferred in C# 6:
            person?.PerformAction?.Invoke();
        }
    }
}