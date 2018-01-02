using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang60
{
    /// <summary>
    /// In C# 6.0 you can now initialise an auto-property inline.
    /// </summary>
    public class AutoPropertyInitializationBehaviour
    {
        [Test]
        public void Should()
        {
            var underTest = new AutoPropertyInitializationExample();
            underTest.ReadOnlyAutoProperty.Should().Be(10);
            underTest.AutoProperty.Should().Be(10);
        }

        public class AutoPropertyInitializationExample
        {
            /// <summary>
            /// An example of an read-write auto-property being initialized inline
            /// </summary>
            public int AutoProperty { get; set; } = 10;

            /// <summary>
            /// Read-Only auto-properties can also be initialized inline
            /// </summary>
            public int ReadOnlyAutoProperty { get; } = 10;
        }
    }
}