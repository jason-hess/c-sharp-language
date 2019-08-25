using System;
using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang20
{
    /// <summary>
    /// http://www.yoda.arachsys.com/csharp/csharp2/delegates.html
    ///
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

        // By constraining the type parameter, you increase the number of allowable operations and method calls to those supported by the constraining type

        // Multiple constraints can be applied to the same type parameter, and the constraints themselves can be generic types

        // class EmployeeList<T> where T : Employee, IEmployee, System.IComparable<T>, new()
        // {

        // Generic Type constraints:
        // where T : struct
        // where T : class
        // where T : unmanaged
        // where T : new()
        // where T : BaseClass
        // where T : Interface
        // where TOne : TTwo

        /*
         * When applying the where T : class constraint, avoid the == and != operators on the type parameter because these operators will test for reference identity only, not for value equality. This behavior occurs even if these operators are overloaded in a type that is used as an argument. The following code illustrates this point; the output is false even though the String class overloads the == operator.

        C#

        Copy
        public static void OpEqualsTest<T>(T s, T t) where T : class
        {
        System.Console.WriteLine(s == t);
        }
        private static void TestStringEquality()
        {
        string s1 = "target";
        System.Text.StringBuilder sb = new System.Text.StringBuilder("target");
        string s2 = sb.ToString();
        OpEqualsTest<string>(s1, s2);
        }
        */

        /**
         * Type parameters that have no constraints, such as T in public class SampleClass<T>{}, are called unbounded type parameters. Unbounded type parameters have the following rules:

The != and == operators cannot be used because there is no guarantee that the concrete type argument will support these operators.
They can be converted to and from System.Object or explicitly converted to any interface type.
You can compare them to null. If an unbounded parameter is compared to null, the comparison will always return false if the type argument is a value type.
         */

        /**
         * Type parameters as constraints
The use of a generic type parameter as a constraint is useful when a member function with its own type parameter has to constrain that parameter to the type parameter of the containing type, as shown in the following example:

C#

Copy
public class List<T>
{
public void Add<U>(List<U> items) where U : T {
}
}
In the previous example, T is a type constraint in the context of the Add method, and an unbounded type parameter in the context of the List class.

Type parameters can also be used as constraints in generic class definitions. The type parameter must be declared within the angle brackets together with any other type parameters:

C#

Copy
//Type parameter V is used as a type constraint.
public class SampleClass<T, U, V> where T : V { }
         */

                // have the same type.
                //var badCombined = first.TypeSafeCombine(test); */

    }
}
 