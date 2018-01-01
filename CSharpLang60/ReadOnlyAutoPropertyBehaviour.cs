using NUnit.Framework;

namespace CSharpLang60
{
    /// <summary>
    /// C# 6.0 now allows the creation of read-only properties that can only be
    /// set from within the constructor.
    /// </summary>
    public class ReadOnlyAutoPropertyBehaviour
    {
        [Test]
        public void Should()
        {
            var underTest = new ReadOnlyAutoPropertyExample();
            underTest.ReadOnlyPropertiesCanOnlyBeSetFromConstructor();
        }

        public class ReadOnlyAutoPropertyExample
        {
            public int AutoProperty { get; set; }
            public int ReadOnlyAutoProperty { get; }
            public int AutoPropertyWithPrivateSetter { get; private set; }

            public ReadOnlyAutoPropertyExample()
            {
                AutoProperty = 10;
                AutoPropertyWithPrivateSetter = 10;
                ReadOnlyAutoProperty = 10;
            }

            public void ReadOnlyPropertiesCanOnlyBeSetFromConstructor()
            {
                AutoPropertyWithPrivateSetter = 11;
                AutoProperty = 12;
                // ReadOnlyAutoProperty = 13; // CS0200  Property or indexer 'ReadOnlyAutoPropertyBehaviour.ReadOnlyAutoPropertyExample.ReadOnlyAutoProperty' cannot be assigned to --it is read only   
            }
        }
    }
}
