// You can enable nullable and nonnullable reference types with the following:
#nullable enable 
using NUnit.Framework;

namespace CSharpLang80
{
    /// <summary>
    /// C# 8.0 introduces nullable and non-nullable reference types that allow you to specify
    /// when a reference is supposed to or not supposed to be null.  Non-nullable reference types
    /// must be assigned a value and cannot be assigned null.  Nullable references can be assigned
    /// null and may only be dereferenced when the compiler can guarantee the value isn't null.
    ///
    /// Don't use this feature to remove all null values from your code. Rather, you should declare
    /// your intent to the compiler and other developers that read your code. By declaring your
    /// intent, the compiler informs you when you write code that is inconsistent with that intent.
    ///
    /// Any reference type can have one of four nullabilities, which describes when warnings are generated:
    /// 
    /// Nonnullable: Null can't be assigned to variables of this type. Variables of this type don't need to be null-checked before dereferencing.
    /// Nullable: Null can be assigned to variables of this type. Dereferencing variables of this type without first checking for null causes a warning.
    /// Oblivious: This is the pre-C# 8 state. Variables of this type can be dereferenced or assigned without warnings.
    /// Unknown: This is generally for type parameters where constraints don't tell the compiler that the type must be nullable or nonnullable.
    /// The nullability of a type in a variable declaration is controlled by the nullable context in which the variable is declared.
    /// </summary>
    public class NullableReferenceBehaviour
    {
        [Test]
        [TestCase(null)]
        public void Method(Person o)
        {
            var result = o?.Name;
            string nonNullableReferenceType = null;
            string? nullableReferenceType = null;
            // The compiler warns you if you are dereferencing a nullable reference that might be null
            // You can override that behaviour by using the null-forgiving operator:
            int length = nullableReferenceType!.Length;
        }
    }

    public class Person
    {
        public string Name { get; set; } = "";

    }
}
