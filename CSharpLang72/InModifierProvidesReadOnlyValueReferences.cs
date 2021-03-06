﻿using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang73
{
    /// <summary>
    /// In C# 7.3 you can use the <code>in</code> modifier on parameters to specify that the called
    /// method won't modify the value of the value reference.
    /// </summary>
    public class InModifierProvidesReadOnlyValueReferences
    {
        [Test]
        public void InModifierMakesParameterReadOnly()
        {
            ReceiveReadOnlyValueByReference(1, 1)
                .Should()
                .Be(2);
        }

        #region Private Members

        // thoughts: I don't know if I would use this.  Without it, length if modified 
        // won't be returned anyway... it could be useful for performance reasons
        private static int ReceiveReadOnlyValueByReference(in int length, in int expectedValue)
        {
            // length++; // CS8331	Cannot assign to variable 'in int' because it is a readonly variable
            return length + expectedValue;
        }

        #endregion
    }
}
