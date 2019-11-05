using System.Diagnostics;
using NUnit.Framework;

namespace CSharpLang80
{
    public class PatternMatchingBehaviour
    {
        public enum LotrCharacter
        {
            Wizard, Hobbit
        }

        [Test]
        public void Should()
        {
            object o = LotrCharacter.Hobbit;
            var name = o switch
            {
                LotrCharacter.Wizard => "Gandalf",
                LotrCharacter c => c switch
                {
                    LotrCharacter.Hobbit => "Hobbit",
                    LotrCharacter.Wizard => "Gandalf"
                }
            };
        }

        [Test]
        public void Should2()
        {
            object x = 10;
            var result = x switch
            {
                10 => "ten",
                int y => y.ToString() switch
                {
                    "11" => "eleven"
                },
                Thing y => null,
                _ => "unknown",
            };
        }

        public string ToString(int value)
        {
            string v = value switch
            {
                1 => "one"
            };
            return v;
        }
    }

    public class Thing
    {

    }
}
