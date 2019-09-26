#nullable enable
using NUnit.Framework;

namespace CSharpLang80
{
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
