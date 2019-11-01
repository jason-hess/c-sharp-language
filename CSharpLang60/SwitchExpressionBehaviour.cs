using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace CSharpLang60
{
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

        public class Thing
        {

        }
    }
}
