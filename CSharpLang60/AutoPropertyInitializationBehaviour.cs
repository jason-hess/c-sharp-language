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
            _underTest.ReadOnlyAutoPropertySetInline.Should().Be(11);
        }

        [Test]
        public void AutoPropertyShouldBeTen()
        {
            _underTest.AutoPropertySetInline.Should().Be(10);
        }

        public class AutoPropertyInitializationExample
        {
            public AutoPropertyInitializationExample() { }

            public AutoPropertyInitializationExample(int defaultValue)
            {
                // Read-Only auto-properties can be set inline or from a constructor.
                ReadOnlyAutoPropertySetInline = defaultValue;
            }

            /// <summary>
            /// An example of an read-write auto-property being initialized inline
            /// </summary>
            public int AutoPropertySetInline { get; set; } = 10;

            /// <summary>
            /// Read-Only auto-properties can also be initialized inline
            /// </summary>
            public int ReadOnlyAutoPropertySetInline { get; } = 11;

            public int AutoPropertyWithPrivateSetterSetInline { get; private set; } = 12;

            public void SetValue(int value)
            {
                AutoPropertyWithPrivateSetterSetInline = value;
            }
        }
    }
}