using FluentAssertions;
using NUnit.Framework;
using static System.String;
using static CSharpLang60.Util.StringExtensions;

namespace CSharpLang60
{
    /// <summary>
    /// C# 6.0 adds `using static` to import all static members (except Extension Methods)
    /// of a single class and its nested types into scope.
    /// </summary>
    public class UsingStaticBehaviour
    {
        // Because of the `using static System.String` above, all `System.String` static members (and nested types)
        // no longer need to be qualified
        [Test]
        public void StaticMembersNoLongerNeedToBeFullyQualified()
        {
            // `IsNullOrEmpty` now resolves to `System.String.IsNullOrEmpty`
            IsNullOrEmpty("hello").Should().BeFalse();
        }

        public void Test(bool? value)
        {
            var isWeekend = value;
            if (isWeekend == null)
            {
                System.Console.WriteLine("HERE");
            }
        }

        /// <summary>
        /// Even with the `using static CSharpLang60.StringExtensions` statement, 
        /// CsharpLang60.StringExtensions.CaseInsensitiveEquals is not imported
        /// since Extension methods are ignored by `using static`
        /// </summary>
        [Test]
        public void ExtensionMethodsAreNotImported()
        {
            "hello".CaseInsensitiveEquals("HELLO").Should().BeTrue();
            // CaseInsensitiveEquals("HELLO").Should().BeTrue(); // CS0103  The name 'CaseInsensitiveEquals' does not exist in the current context CSharpLang60   
        }

        /// <summary>
        /// The `using static CSharpLang60.StringExtensions` statement above imports the 
        /// `NestedClass` type which is a class within the StringExtensions class.
        /// </summary>
        [Test]
        public void UsingStaticImportsNestedTypes()
        {
            ((object)new NestedClass()).Should().NotBeNull();
        }
    }
}