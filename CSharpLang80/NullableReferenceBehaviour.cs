#nullable enable
using NUnit.Framework;

namespace CSharpLang80
{
    /// <summary>
    /// C# 8.0 introduces nullable and non-nullable reference types that allow you to specify
    /// when a reference is supposed to or not supposed to be null.  Non-nullable reference types
    /// must be assigned a value and cannot be assigned null.  Nullable references can be assigned
    /// null and may only be dereferenced when the compiler can guarantee the value isn't null.'
    ///
    /// Don't use this feature to remove all null values from your code. Rather, you should declare
    /// your intent to the compiler and other developers that read your code. By declaring your
    /// intent, the compiler informs you when you write code that is inconsistent with that intent.
    /// </summary>
    public class NullableReferenceBehaviour
    {
        [Test]
        public void Method(Person o)
        {
            string result = o.Name;
        }
    }

    public class Person
    {
        public string Name { get; set; } = "";
    }
}
