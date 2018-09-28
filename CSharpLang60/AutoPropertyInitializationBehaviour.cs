using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang60
{
    /// <summary>
    /// In C# 6.0 you can initialise an auto-property inline.
    /// </summary>
    public class AutoPropertyInitializationBehaviour
    {
        AutoPropertyInitializationExample _underTest;

        [SetUp]
        public void SetUp()
        {
            _underTest = new AutoPropertyInitializationExample();
        }

        [Test]
        public void ReadOnlyAutoPropertyShouldBeTen()
        {
            _underTest.ReadOnlyAutoProperty.Should().Be(11);
        }

        [Test]
        public void AutoPropertyShouldBeTen()
        {
            _underTest.AutoProperty.Should().Be(10);
        }
        public class AutoPropertyInitializationExample
        {
            public AutoPropertyInitializationExample() { }

            public AutoPropertyInitializationExample(int defaultValue)
            {
                // Read-Only auto-properties can be set inline or from a constructor.
                ReadOnlyAutoProperty = defaultValue;
            }

            /// <summary>
            /// An example of an read-write auto-property being initialized inline
            /// </summary>
            public int AutoProperty { get; set; } = 10;

            /// <summary>
            /// Read-Only auto-properties can also be initialized inline
            /// </summary>
            public int ReadOnlyAutoProperty { get; } = 11;

            public int AutoPropertyWithPrivateSetterCanSetInline { get; private set; } = 12;
        }
    }
}