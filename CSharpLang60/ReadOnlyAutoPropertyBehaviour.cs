using NUnit.Framework;

namespace CSharpLang60
{
    /// <summary>
    /// C# 6.0 now allows the creation of read-only properties that can only be
    /// set from within the constructor or inline.
    /// </summary>
    public class ReadOnlyAutoPropertyBehaviour
    {
        ReadOnlyAutoPropertyExample _underTest;

        [SetUp]
        public void SetUp()
        {
            _underTest = new ReadOnlyAutoPropertyExample();
        }

        [Test]
        public void Should()
        {
            _underTest.ReadOnlyPropertiesCanOnlyBeSetFromConstructorOrInline();
        }

        public class ReadOnlyAutoPropertyExample
        {
            public int AutoProperty { get; set; }

            /// <remarks>
            /// In previous versions of C# you could define a private setter so external code
            /// could not modify the value.
            /// </remarks>
            public int AutoPropertyWithPrivateSetter { get; private set; }

            /// <remarks>
            /// In C# 6 you can now define a read-only property which can only be set inline
            /// or from the constructor.
            /// </remarks>
            public int ReadOnlyAutoProperty { get; }

            public int ReadOnlyAutoPropertySetInline { get; } = 10;

            public ReadOnlyAutoPropertyExample()
            {
                AutoProperty = 10;
                AutoPropertyWithPrivateSetter = 10;
                ReadOnlyAutoProperty = 10;
            }

            public void ReadOnlyPropertiesCanOnlyBeSetFromConstructorOrInline()
            {
                AutoPropertyWithPrivateSetter = 11;
                AutoProperty = 12;
                // ReadOnlyAutoProperty = 13; // CS0200  Property or indexer 'ReadOnlyAutoPropertyBehaviour.ReadOnlyAutoPropertyExample.ReadOnlyAutoProperty' cannot be assigned to --it is read only   
            }
        }
    }
}