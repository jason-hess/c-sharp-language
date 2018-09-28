﻿using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang70
{
    public class OutParameterBehaviour
    {
        [Test]
        public void OutParametersCanBeDeclaredInline()
        {
            // In previous versions of C# the caller needed to declare an output
            // variable before passing it to a method.
            int outputParameter;
            bool success = int.TryParse("1", out outputParameter);

            // This can now be done inline:
            success = int.TryParse("2", out int inlineOutputParameter);
            inlineOutputParameter.Should().Be(2);
        }

        // Thought: TryParse is an interesting method because it seems to 
        // have more than one responsibility: 1) Is the string convertable
        // 2) Convert the string
        public void MethodWithOutParameter(out int x)
        {
            x = 1;
        }

        // Question: When are out parameters useful?
        // Possible Answer: When you are already using the return value and want to 
        // return more than one value.  The other otpions would be to use a Tuple,
        // or to return an object, but that could blur the lines.  For instance,
        // TryParse has one value it returns (whether the string is convertable)
        // and a separate concept which is the converted value.
    }
}
