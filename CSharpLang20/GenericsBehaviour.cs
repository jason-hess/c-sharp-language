using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang20
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history
    /// </summary> 
    public class GenericsBehaviour
    {
        // generics avoid the runtime cost of casts or boxing
        // Generic classes and methods combine reusability, type safety and efficiency in a way that
        // their non-generic counterparts cannot. Generics are most frequently used with collections
        // and the methods that operate on them. 

        // a type parameter is a placeholder for a specific type that a client specifies
        // when they create an instance of the generic type

        // A generic class cannot be used as is.  It is more like a blueprint of a Type.
        public class GenericContainer<T>
        {
            private readonly T _containedInstance;

            public GenericContainer(T instance)
            {
                _containedInstance = instance;
            }

            public T Value
            {
                get
                {
                    return _containedInstance;
                }
            }
        }

        [Test]
        public void CanInstantiateClassWithVariousTypes()
        {
            // Generic types allow the client to specify the type when the class
            // is instantiated
            GenericContainer<int> container = new GenericContainer<int>(10);
            container.Value.Should().Be(10);
        }

        // Constraints inform the compiler about the capabilities a type argument must have.
        // Without any constraints, the type argument could be any type. The compiler can
        // only assume the members of System.Object, which is the ultimate base class
        // for any .NET type.

        public class GenericTypeConstraints
        {

            public class CanBeStructConstraint<T> where T : struct { } // must be a value type excluding Nullable<T>

            public static void MustBeReferenceTypeConstraint<T>(T value) where T: class { } // must be a reference type (interface, delegate or array)

            public static T MustHaveDefaultConstructor<T>() where T : new()
            {
                return new T();
            }

            public static void MustBeOrInheritFrom<T>() where T : CanBeStructConstraint<int>
            {

            }

            public static void MustImplementInterface<T, TU>() where T : IEquatable<TU>
            {

            }

            public static void CanReferToOtherTypeConstraintClasses<TParent, TChild>() where TChild : TParent
            {

            }

            [Test]
            public void CanPassDelegateToReferenceTypeConstrainedGeneric()
            {
                Action doSomthing = CanPassDelegateToReferenceTypeConstrainedGeneric;
                MustBeReferenceTypeConstraint(doSomthing);
            }
        }

        public class GenericTypeParameterEquality
        {
            public static bool TwoThingsAreEqual<T>(T first, T other) where T : class
            {
                // This will compile to reference equality and not use an operator
                // overloaded method even if one exists
                return first == other;
            }

            [Test]
            public void GenericTypeParametersAlwaysCompileToReferenceEquality()
            {
                System.String firstString = "1";
                System.String seconString = 1.ToString();
                TwoThingsAreEqual(firstString, seconString)
                    .Should()
                    .BeFalse("the compiler will compile reference equality for generic type parameters since it doesn't know about operator overloads for types it doesn't know");
            }
        }

        /**
         * Type parameters that have no constraints,
         * such as T in public class SampleClass<T>{},
         * are called unbounded type parameters.
         * Unbounded type parameters have the following rules:

The != and == operators cannot be used because there is no guarantee that the concrete type argument will support these operators.
They can be converted to and from System.Object or explicitly converted to any interface type.
You can compare them to null. If an unbounded parameter is compared to null, the comparison will always return false if the type argument is a value type.
         */
        public class UnboundedTypeParameters
        {
            public class UnboundedGenericTypeParameter<T>
            {
                public void DoSomething(T value, T other) 
                {
                    // if (value == other) // Error CS0019  Operator '==' cannot be applied to operands of type 'T' and 'T'
                    // {
                    // 
                    // }
                    object asObject = value;
                    IEquatable<int> anInterface = (IEquatable<int>)value;
                    if (value == null)
                    {
                        // if T is a value type then == null returns false
                    }

                    // string asString = (string) value; // Error CS0030  Cannot convert type 'T' to 'string' 

                }
            }
        }

        // By constraining the type parameter, you increase the number of allowable operations and method calls to those supported by the constraining type

        // Multiple constraints can be applied to the same type parameter, and the constraints themselves can be generic types

        // class EmployeeList<T> where T : Employee, IEmployee, System.IComparable<T>, new()
        // {

        // Tips
        // A good rule is to apply the maximum constraints possible that will still let you handle the types you must handle.

        /** Type parameter naming guidelines
           Do name generic type parameters with descriptive names, unless a single letter name is completely self explanatory and a descriptive name would not add value.
           
           C#
           
           Copy
           public interface ISessionChannel<TSession> { /*...* / }
           public delegate TOutput Converter<TInput, TOutput>(TInput from);
           public class List<T> { /*...* / }
           Consider using T as the type parameter name for types with one single letter type parameter.
           
           C#
           
           Copy
           public int IComparer<T>() { return 0; }
           public delegate bool Predicate<T>(T item);
           public struct Nullable<T> where T : struct { /*...* / }
           Do prefix descriptive type parameter names with "T".
           
           C#
           
           Copy
           public interface ISessionChannel<TSession>
           {
           TSession Session { get; }
           }
           Consider indicating constraints placed on a type parameter in the name of parameter. For example, a parameter constrained to ISession may be called TSession.
           
           The code analysis rule CA1715 can be used to ensure that type parameters are named appropriately. **/

        // Classes can be generalised:

            public class BaseClass { }

        public class GenericBaseClass<T>
        {
        }

        public class CanInheritFromConcreteBase<T> : BaseClass { }

        public class CanInheritFromClosedConstructedBase<T> : GenericBaseClass<int> { }

        public class CanInheritFromOpenConstructedBase<T> : GenericBaseClass<T> { }

        // Generic classes are invariant. In other words, if an input parameter specifies a List<BaseClass>, you will get a compile-time error if you try to provide a List<DerivedClass>.

        // interfaces can have generic type parameters:

        public interface IGenericInterface<T> { }
        // can specify more than one type parameter
        public interface IGenericInterfaceWithMultipleTypeParameters<T1, T2> { }

        // multiple interfaces can be specified for type constraints:
        public class SomeClass<T> where T : IComparable<T>, IEquatable<T> { }

        public class MethodsCanHaveGenericTypeParameters
        {
            public void MethodWithGenericTypeParameter<T>(T value) { }
        }

    }
}
 