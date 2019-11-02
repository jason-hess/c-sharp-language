using System;
using CSharpLang60.Util;
using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang60
{
    public class AsAndIsBehaviour
    {
        [Test]
        public void AsIsCastWithoutException()
        {
            object aString = null;
            Thing aThing = (aString as Thing);
        }

        [Test]
        public void IsReturnsTrueIfInstanceOfTypeOrSubTypeAndNotNull()
        {
            object aThing = new Thing();
            if (aThing is Thing)
            {
                true.Should().BeTrue();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public class Thing
        {

        }

        public class AnotherThing
        {

        }
    }
}
