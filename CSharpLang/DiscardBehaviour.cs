using NUnit.Framework;

namespace CSharpLang
{
    /// <summary>
    /// C# 7.0 adds support for write-only variables that you don't intend to 
    /// use.
    /// </summary>
    public class DiscardBehaviour
    {
        [Test]
        public void Test()
        {
            var tuple = (Year: 1996, Age: 21, Sex: "Male");
            // Deconstuct the tuple, but don't care about the year:
            var (_, age, _) = tuple;
            Assert.AreEqual(tuple.Age, age);
        }
    }
}