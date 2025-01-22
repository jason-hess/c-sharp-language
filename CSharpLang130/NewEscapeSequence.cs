using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang130
{
    /// <summary>
    /// You can use \e as a character literal escape sequence for the ESCAPE character, Unicode U+001B. 
    /// Previously, you used \u001b or \x1b. Using \x1b wasn't recommended because if the next characters 
    /// following 1b were valid hexadecimal digits, those characters became part of the escape sequence.
    /// </summary>
    /// <see href="https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-13#new-escape-sequence" />
    internal class NewEscapeSequence
    {
        [Test]
        public void ShouldEscape()
        {
            '\e'.Should().Be('\x1b');   
        }
    }
}
