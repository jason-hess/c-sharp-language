using NUnit.Framework;

namespace CSharpLang20
{
    /// <summary>
    /// C# 2.0 introduced the System.Nullable which was a generic type that had
    /// operators overloaded.
    /// </summary>
    public class NullableTypeBehaviour
    {
        [Test]
        public void Could()
        {
            System.Nullable<int> nullableValue = null;
            nullableValue = 10;
            if (nullableValue.HasValue)
            {
                int value = nullableValue.Value;
            }
        }

        [Test]
        public void NullableIsStruct()
        {
            int? x = 10;
            System.ValueType y = x;
        }
    }
}