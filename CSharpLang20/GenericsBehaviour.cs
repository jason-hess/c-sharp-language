using System;
using System.Collections.Generic;
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
            public void WithinAGenericClassMethodsCanUseTheTypeWithoutSpecifyingTheGenericType(T value) { }
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

            public void RoslynWillInferTheTypeParameter()
            {
                int x = 0;
                MethodWithGenericTypeParameter(x);
                MethodWithGenericTypeParameter<int>(0);
                // note: type inference only works for parameters.  
                // inferring return types or types used within the method is not done
                // by the compiler
            }

            public void MethodsCanBeOverloadedWithDifferentTypeParameters() { }
            public void MethodsCanBeOverloadedWithDifferentTypeParameters<T>() { }
            public void MethodsCanBeOverloadedWithDifferentTypeParameters<T, U>() { } 
        }

        /**
         * In C# 2.0 and later, single-dimensional arrays that have a lower bound of zero automatically implement IList<T>. This enables you to create generic methods that can use the same code to iterate through arrays and other collection types. This technique is primarily useful for reading data in collections. The IList<T> interface cannot be used to add or remove elements from an array. An exception will be thrown if you try to call an IList<T> method such as RemoveAt on an array in this context.
         */

        public class ArraysInCS20AreILists
        {
            [Test]
            public void ShouldBeList()
            {
                int[] anArray = new int[] {0, 1, 2};
                IList<int> aList = anArray;
            }
        }

        // A delegate can define its own type parameters:

        public delegate void SomeDelegate<T>(T value);

        public class DelegatesCanHaveTypeParameters
        {
            public static void DoSomething(int value)
            {

            }

            private SomeDelegate<int> someDelegate = new SomeDelegate<int>(DoSomething);
            // or shorthand in C# 2.0
            private SomeDelegate<int> shorthand = DoSomething;
        }

        public class DelegatesCanUseTheClassGnericTypeParameter<T>
        {
            public delegate void GenericDelegate(T value);
        }

        public class UsingTheGenericDelegate
        {
            public void Method()
            {
                // but you must close the generic if you want to use the delegate:
                DelegatesCanUseTheClassGnericTypeParameter<int>.GenericDelegate theDelegate;
            }
        }

        /** 
        Generic delegates are especially useful in defining events based on the typical design pattern because the sender argument can be strongly typed and no longer has to be cast to and from Object.
           
           C#
           
           Copy
           delegate void StackEventHandler<T, U>(T sender, U eventArgs);
           
           class Stack<T>
           {
           public class StackEventArgs : System.EventArgs { }
           public event StackEventHandler<Stack<T>, StackEventArgs> stackEvent;
           
           protected virtual void OnStackChanged(StackEventArgs a)
           {
           stackEvent(this, a);
           }
           }
           
           class SampleClass
           {
           public void HandleStackChange<T>(Stack<T> stack, Stack<T>.StackEventArgs args) { }
           }
           
           public static void Test()
           {
           Stack<double> s = new Stack<double>();
           SampleClass o = new SampleClass();
           s.stackEvent += o.HandleStackChange;
           } */

        // Custom Attributes can only reference open generic types
        public class CustomAttribute : Attribute
        {
            public Type type; 
        }

        [Custom(type = typeof(SomeClass<>))]
        public class DecoratedClass<T>
        {
            [Custom(type = typeof(SomeClass<int>))]
            public void Method() { }


            // [Custom(type = typeof(SomeClass<T>))] // error: an attribute cannot use type parameters 
            public void Method2() { }
        }

        // generic types cannot inherit from System.Attribute
    }
}
 
 