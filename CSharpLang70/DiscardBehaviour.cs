using NUnit.Framework;

namespace CSharpLang70
{
    /// <summary>
    /// C# 7.0 adds support for write-only variables called discards that you don't intend to 
    /// use.  Discards are named with the underscore character.  Discards can reduce memory 
    /// allocations and increase the readability of your code.
    ///
    /// See: https://docs.microsoft.com/en-us/dotnet/csharp/discards
    /// </summary>
    public class DiscardBehaviour
    {
        [Test]
        public void SupportedWhenDeconstructingTuples()
        {
            var tuple = (Year: 1996, Age: 21, Sex: "Male");
            // Deconstruct the tuple, but don't care about the year:
            var (_, age, _) = tuple;
            Assert.AreEqual(tuple.Age, age);
        }

        [Test]
        public void SupportedWhenCallingMethodsWithOutParameters()
        {
            int.TryParse("1", out int _);
            int.TryParse("1", out var _);
            int.TryParse("1", out _);
        }

        [Test]
        public void Supported()
        {
            var _ = 1 + 2;
        }
    }
}