using System;
using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang30
{
    public class LambdaExpressionBehaviour
    {
        // An expression lambda has an expression as its body:
        // (parameters) => expression

        // A statement lambda has a statement block as its body:
        // (parameters) => { statements }

        // Any lambda expression can be converted to a delegate type.
        // The delegate type to which a lambda expression can be converted
        // is defined by the types of its parameters and return value.
        // If a lambda expression doesn't return a value, it can be
        // converted to one of the Action delegate types; otherwise,
        // it can be converted to one of the Func delegate types.

        [Test]
        public void Should()
        {
            Func<int> lambda = () => 10;
            lambda()
                .Should()
                .Be(10);
        }
    }
}
