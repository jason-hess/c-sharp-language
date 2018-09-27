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
            nameof(CSharpLang60.NameOfBehaviour).Should().Be("NameOfBehaviour");
        }

        [Test]
        public void ShouldReturnNonFullyQualifiedMethodName()
        {
            nameof(ShouldReturnNonFullyQualifiedMethodName).Should().Be("ShouldReturnNonFullyQualifiedMethodName");
        }

        [Test]
        public void ShouldReturnNonFullyQualifiedClassName()
        {
            nameof(NameOfBehaviour).Should().Be("NameOfBehaviour");
        }

        [Test]
        public void ReturnsNonFullyQualifiedAttributeName()
        {
            nameof(TestAttribute).Should().Be("TestAttribute");
        }

        [Test]
        public void ReturnsNonFullyQualifiedAttributeNameWithoutAttributeSuffix()
        {
            nameof(Test).Should().Be("Test");
        }

        [Test]
        public void ReturnsNonFullyQualifiedNamespaceName()
        {
            nameof(CSharpLang60).Should().Be("CSharpLang60");
        }

        [Test]
        public void ReturnsVariableName()
        {
            var someVariable = 10;
            nameof(someVariable).Should().Be("someVariable");
        }
    }
}