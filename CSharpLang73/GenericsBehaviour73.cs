﻿using System;
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
                .BeEquivalentTo(new [] {Flag.Canadian});
        }

        /* C#

Copy
public static Dictionary<int, string> EnumNamedValues<T>() where T : System.Enum
{
    var result = new Dictionary<int, string>();
    var values = Enum.GetValues(typeof(T));

    foreach (int item in values)
        result.Add(item, Enum.GetName(typeof(T), item));
    return result;
}
The methods used make use of reflection, which has performance implications. You can call this method to build a collection that is cached and reused rather than repeating the calls that require reflection.

You could use it as shown in the following sample to create an enum and build a dictionary of its values and names:

C#

Copy
enum Rainbow
{
    Red,
    Orange,
    Yellow,
    Green,
    Blue,
    Indigo,
    Violet
}
C#

Copy
var map = EnumNamedValues<Rainbow>();

foreach (var pair in map)
    Console.WriteLine($"{pair.Key}:\t{pair.Value}"); */
    }
}