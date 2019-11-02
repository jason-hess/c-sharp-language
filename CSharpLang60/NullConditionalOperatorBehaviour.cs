using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using CSharpLang60.Util;

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

            if (person != null)
            {
                // store action as a temp variable to prevent race condition
                // if person is accessible from elsewhere
                var action = person.PerformAction;
                if (action != null)
                {
                    action();
                }
            }

            // This is what is preferred in C# 6.  Note that any race conditions in the above
            // conditional is eliminated.
            person?.PerformAction?.Invoke();
        }

        delegate void M(int x);

        public void Implementation(int x)
        {
            return;
        }

        [Test]
        public void CanBeUsedForDelegates()
        {
            /**
             * Finally, let's update the LogMessage method so that it is robust for those cases when no output mechanism is selected. The current implementation will throw a NullReferenceException when the WriteMessage delegate does not have an invocation list attached. You may prefer a design that silently continues when no methods have been attached. This is easy using the null conditional operator, combined with the Delegate.Invoke() method:
               
               C#
               
               Copy
               public static void LogMessage(string msg)
               {
               WriteMessage?.Invoke(msg);
               }
               The null conditional operator (?.) short-circuits when the left operand (WriteMessage in this case) is null, which means no attempt is made to log a message.
               
               You won't find the Invoke() method listed in the documentation for System.Delegate or System.MulticastDelegate. The compiler generates a type safe Invoke method for any delegate type declared. In this example, that means Invoke takes a single string argument, and has a void return type.
               
               
             */
            M myMethod = Implementation;
            myMethod?.Invoke(0);
        }

        [Test]
        public void DisposeExample()
        {
            object o = "hello";
            (o as IDisposable)?.Dispose();
            (o as IList<int>)?.Clear();


        }
    }
}