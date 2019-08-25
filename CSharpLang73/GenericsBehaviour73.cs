using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang73
{
    public class GenericsBehaviour73
    {
        /// <summary>
        /// In C# 7.3 you can specify the type constraint enum to ensure the
        /// generic type is an enum by specifying `System.Enum` as a base class constraint
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
                .BeEquivalentTo(new[] { Flag.Canadian });
        }

        /// <summary>
        /// Beginning with C# 7.3, you can use the unmanaged constraint to specify that a
        /// type parameter is a non-pointer unmanaged type.
        /// </summary>
        /// <remarks>
        /// An unmanaged type is any type that isn't a reference type or constructed type
        /// (a type that includes at least one type argument), and doesn't contain reference
        /// type or constructed type fields at any level of nesting.
        /// In other words, an unmanaged type is one of the following:
        /// - sbyte, byte, short, ushort, int, uint, long, ulong, char, float, double, decimal, or bool
        /// - Any enum type
        /// - Any pointer type
        /// - Any user-defined struct type that is not a constructed type and contains fields of unmanaged types only
        /// </remarks>
        public class UnmanagedTypeConstraint
        {
            public static string TypeName<T>(T value) where T : unmanaged => 
                typeof(T).FullName;

            [Test]
            public void CanConstrainGenericTypeToUnmanagedType()
            {
                short p = 0;
                var genericInstance = TypeName(p);
            }
        }

        /// <summary>
        /// Starting in C# 7.3 you can specify a genetic delegate type constraint.
        /// </summary>
        public class DelegateTypeConstraint
        {
            public static TDelegate Combine<TDelegate>(TDelegate first, TDelegate second) where TDelegate : Delegate => 
                Delegate.Combine(first, second) as TDelegate;

            [Test]
            public void CanConstraintGenericTypeParametersToDelegate()
            {
                var x = 10;

                Action first = () => x++;
                Action second = () => x++;

                var combined = Combine(first, second);
                combined();

                x.Should().Be(12);

                Func<bool> booleanDelegate = () => true;

                // var anotherCombined = Combine(first, booleanDelegate); // Error CS0411 The type arguments for method 'GenericsBehaviour73.DelegateTypeConstraint.Combine<TDelegate>(TDelegate, TDelegate)' cannot be inferred from the usage.

            }
        }
    }
}
