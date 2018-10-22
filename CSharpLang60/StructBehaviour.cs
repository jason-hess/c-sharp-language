using CSharpLang60.Util;
using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang60
{
    /// <summary>
    /// Demonstrate behaviour before C# 6.0
    /// </summary>
    public class StructBehaviour
    {
        [Test]
        public void Should()
        {
            var x = new Rectangle();
            x.Length = 10;
            var y = x;
            x.Length = 11;
            y.Length.Should().Be(10);
        }
    }
}
