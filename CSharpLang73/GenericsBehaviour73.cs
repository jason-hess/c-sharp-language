using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang73
{
    public class GenericsBehaviour73
    {
        /// <summary>
        /// In C# 7.3 you can specify the type constraint enum to ensure the
        /// generic type is an enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class EnumGenericTypeConstraint<T> where T : System.Enum
        {
            private readonly T _value;

            public EnumGenericTypeConstraint(T value)
            {
                _value = value;
            }

            public IEnumerable<T> NotValues =>
                typeof(T)
                    .GetEnumValues() // Note: Roslyn understands that T has GetEnumValues()
                    .Cast<T>()
                    .Where(enumValue => !enumValue.Equals(_value));
        }

        public enum Flag
        {
            Australian,
            Canadian
        }

        [Test]
        public void CanAddEnumAsTypeConstraint()
        {
            var flagType = new EnumGenericTypeConstraint<Flag>(Flag.Australian);
            var notValues = flagType.NotValues;
            // var intType = new EnumGenericTypeConstraint<int>(); // Error CS0315  The type 'int' cannot be used as type parameter 'T' in the generic type or method 'GenericsBehaviour73.EnumGenericTypeConstraint<T>'.There is no boxing conversion from 'int' to 'System.Enum'
            flagType.NotValues
                .Should()
                .BeEquivalentTo(new[] {Flag.Canadian});
        }
    }
}
