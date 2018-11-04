using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang20
{
    /// <summary>
    /// C# 2.0 added support for partial classes.  During complication the two definitions
    /// are combined into a single.  The benefit of these definitions are mostly for generated
    /// code.
    /// </summary>
    public partial class PartialClassBehaviour
    {
        public partial class ThePartialClass
        {
            public string Description
            {
                get { return "PartialClassBehaviour"; }
            }
        }

        public partial class ThePartialClass
        {
            public int Length
            {
                get { return Description.Length; }
            }
        }

        [Test]
        public void CompilerShouldCombineDefinition()
        {
            ThePartialClass underTest = new ThePartialClass();
            underTest.Description.Should().Be("PartialClassBehaviour");
            underTest.Length.Should().BeGreaterThan(0);
        }
    }
}
