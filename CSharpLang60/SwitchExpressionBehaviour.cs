using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace CSharpLang60
{
    /// <summary>
    /// C# 6.0 (and prior) switch statement behaviour
    /// </summary>
    class SwitchExpressionBehaviour
    {
        [Test]
        [TestCase(null)]
        public void Should(Thing theThing)
        {
            Thing aThing = new Thing();
            //switch (aThing) // Error CS0151 A switch expression or case label must be a bool, char, string, integral, enum, or corresponding nullable type in C# 6 and earlier.
            {
            }

            var aRealThing = aThing is Thing;
            if(aRealThing != null)
            {
                return;
            }
        }

        [Test]
        public void SwitchCanBeEmpty()
        {
            int x = 10;
            switch(x)
            {

            }
        }

        [Test]
        public void SwitchMustBailOutOfEachMatch()
        {
            var x = new Random().Next();

            switch (x)
            {
                // case 1: // Error CS0163  Control cannot fall through from one case label('case 1:') to another
                   // x = 10;
                // case 2: // Error CS8070  Control cannot fall out of switch from final case label('case 2:')
                   // x = 13;
            }
        }

        [Test]
        public void SwitchMustBeOneOfShortIntLongBoolStringCharOrEnum()
        {
            float x = 10;
            // switch(x) // Error CS0151 A switch expression or case label must be a bool, char, string, integral, enum, or corresponding nullable type in C# 6 and earlier.
            {
            }
        }

        [Test]
        public void SwitchSectionCanHaveTwoLabels()
        {
            switch(10)
            {
                case 1:
                case 2:
                    var result = "1 or 2";
                    return; // good to specify here although not needed?
            }
        }

        public class Thing
        {

        }
    }
}
