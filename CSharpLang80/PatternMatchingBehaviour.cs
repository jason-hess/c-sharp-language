using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
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
        public void ShouldDoSomething()
        {
            var x = (1, 2);
            switch (x)
            {
                case (1, 2): return;
            }
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

        public void MySwitchExample()
        {
            Thing o = new Thing();
            switch (o)
            {
                case { Property: 1 }:
                    return;
            }

            bool? isWeekday = null;

#nullable enable
            List<Thing> list = new List<Thing>();
            list.Add(null);

            GenericList<Thing> gList = new GenericList<Thing>();

        }
    }

    public class Thing
    {
        public int Property { get; set; }
    }

    public class GenericList<T> where T : notnull
    {

    }
}
