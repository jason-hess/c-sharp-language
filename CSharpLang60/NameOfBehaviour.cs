using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace CSharpLang60
{
    /// <summary>
    /// C# 6.0 now allows you to retrieve the name of an identifier with the `nameof` 
    /// operator keyword
    /// </summary>
    public class NameOfBehaviour
    {
        [Test]
        public void ShouldReturnNonFullyQualifiedNameOfIdentifier()
        {
            nameof(ShouldReturnNonFullyQualifiedNameOfIdentifier).Should().Be("ShouldReturnNonFullyQualifiedNameOfIdentifier");
            nameof(NameOfBehaviour).Should().Be("NameOfBehaviour");
            nameof(Test).Should().Be("Test");
            nameof(CSharpLang60).Should().Be("CSharpLang60");
            var someVariable = 10;
            nameof(someVariable).Should().Be("someVariable");
        }
    }
}