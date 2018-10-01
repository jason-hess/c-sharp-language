using NUnit.Framework;

namespace CSharpLang
{
    /// <summary>
    /// C# 7.0 adds support for write-only variables called discards that you don't intend to 
    /// use.  Discards are named with the underscore character.  Discards can reduce memory 
    /// allocations and increase the readability of your code.
    /// </summary>
    /// <see cref="https://docs.microsoft.com/en-us/dotnet/csharp/discards"/>
    public class DiscardBehaviour
    {
        [Test]
        public void SupportedWhenDeconstructingTuples()
        {
            var tuple = (Year: 1996, Age: 21, Sex: "Male");
            // Deconstuct the tuple, but don't care about the year:
            var (_, age, _) = tuple;
            Assert.AreEqual(tuple.Age, age);
        }

        [Test]
        public void SupportedWhenCallingMethodsWithOutParameters()
        {
            int.TryParse("1", out int _);
        }

        [Test]
        public void Supported()
        {
            int _ = 1 + 2;
        }
    }
}