using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang20
{
    public class GenericsBehaviour
    {
        // Generic classes and methods combine reusability, type safety and efficiency in a way that
        // their non-generic counterparts cannot. Generics are most frequently used with collections
        // and the methods that operate on them.  Generics avoid the runtime cost of casts or boxing.
        // A type parameter is a placeholder for a specific type that a client specifies
        // when they create an instance of the generic type.  A generic class or method cannot be
        // used as is, it is more like a blueprint of a Type or method

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

        /// <summary>
        /// Generic Type Constraints inform the compiler about the capabilities a type argument
        /// must have.  Without any constraints, the type argument could be any type.
        /// The compiler can only assume the members of System.Object, which is the ultimate
        /// base class for any .NET type.
        /// By constraining the type parameter, you increase the number of allowable operations and method calls to those supported by the constraining type
        ///
        /// A good rule is to apply the maximum constraints possible that will still let you handle the types you must handle.
        /// </summary>
        public class GenericTypeConstraints
        {
            /// <summary>
            /// Type parameters that have no constraints,
            /// * such as T in public class SampleClass&lt;T&gt;{},
            /// * are called unbounded type parameters.
            /// * Unbounded type parameters have the following rules:
            /// 
            /// The != and == operators cannot be used because there is no guarantee that the concrete type argument will support these operators.
            /// They can be converted to and from System.Object or explicitly converted to any interface type.
            /// You can compare them to null. If an unbounded parameter is compared to null, the comparison will always return false if the type argument is a value type.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            public class NoConstraint<T> // T is a System.Object
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

            public class CanBeStructConstraint<T> where T : struct { } // must be a value type excluding Nullable<T>

            public static void MustBeReferenceTypeConstraint<T>(T value) where T : class { } // must be a reference type (interface, delegate or array)

            public static T MustHaveDefaultConstructor<T>() where T : new()
            {
                return new T();
            }

            public static void MustBeOrInheritFrom<T>() where T : CanBeStructConstraint<int> { }

            public static void MustImplementInterface<T, TU>() where T : IEquatable<TU> { }

            /// <summary>
            /// Multiple constraints can be specified and the constraints themselves can be generic types
            /// </summary>
            /// <typeparam name="T"></typeparam>
            public static void CanSpecifyMultipleConstraints<T>() where T : class, IEquatable<T>, new() { }

            public static void CanReferToOtherTypeConstraintClasses<TParent, TChild>() where TChild : TParent { }

            [Test]
            public void CanPassDelegateToReferenceTypeConstrainedGeneric()
            {
                Action doSomething = CanPassDelegateToReferenceTypeConstrainedGeneric;
                MustBeReferenceTypeConstraint(doSomething);
            }
        }

        /// <summary>
        /// It is important to be careful with == and != in a generic type.  The compiler
        /// does not know the type parameter so == and != compile to reference equality
        /// rather than operator overloads
        /// </summary>
        public class GenericTypeParameterEquality
        {
            public static bool TwoThingsAreEqual<T>(T first, T other) where T : class
            {
                // This will compile to reference equality and not use an operator
                // overloaded method even if one exists
                return first == other;
            }

            /// <summary>
            /// This test demonstrates that even though the two strings are equivalent
            /// the generic type returns false since reference equality is used.
            /// </summary>
            [Test]
            public void GenericTypeParametersAlwaysCompileToReferenceEquality()
            {
                string firstString = "1";
                string seconString = 1.ToString();
                TwoThingsAreEqual(firstString, seconString)
                    .Should()
                    .BeFalse("the compiler will compile reference equality for generic type parameters since it doesn't know about operator overloads for types it doesn't know");
            }
        }

        public class NamingGuidelines
        {
            /// <summary>
            /// Do name generic type parameters with descriptive names,
            /// unless a single letter name is completely self explanatory
            /// and a descriptive name would not add value.
            /// Consider indicating constraints placed on a type parameter
            /// in the name of parameter. For example, a parameter constrained
            /// to ISession may be called TSession.
            /// The code analysis rule CA1715 can be used to ensure that type parameters are named appropriately. **/
            /// </summary>
            public void GenericMethod<TComparable>(TComparable comparable) where TComparable : IComparable { }

            /// <summary>
            /// Consider using T as the type parameter name for types with one single letter type parameter.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="value"></param>
            public void GenericMethod2<T>(T value) { }
        }

        public class GenericClasses
        {


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

        }

        // interfaces can have generic type parameters:
        public class GenericInterfaces
        {
            public interface IGenericInterface<T> { }

            // can specify more than one type parameter
            public interface IGenericInterfaceWithMultipleTypeParameters<T1, T2> { }

            // multiple interfaces can be specified for type constraints:
            // the following means T must implement IComparable<T> like:
            // public class SomeType : IComparable<SomeType> { }
            public class SomeClass<T> where T : IComparable<T>, IEquatable<T>
            {
                // note: the above means T must implement IComparable<T>
                // for example: if T is int, then int must implement IEquatable<int> which it does
            }
        }

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
            public void MethodsCanBeOverloadedWithDifferentTypeParameters<T1, T2>() { }
        }

        /// <summary>
        /// In C# 2.0 and later, single-dimensional arrays that have a lower bound of zero automatically
        /// implement IList<T>. This enables you to create generic methods that can use the same code to
        /// iterate through arrays and other collection types. This technique is primarily useful for
        /// reading data in collections. The IList<T> interface cannot be used to add or remove elements
        /// from an array. An exception will be thrown if you try to call an IList<T> method such as
        /// RemoveAt on an array in this context.
        /// </summary>
        public class ArraysCSharpLang20ImplementGenericIList
        {
            [Test]
            public void ShouldBeList()
            {
                int[] anArray = new int[] { 0, 1, 2 };
                IList<int> aList = anArray;
            }
        }

        // A delegate can define its own type parameters:


        public class DelegatesCanHaveTypeParameters
        {
            public delegate void SomeDelegate<T>(T value);

            public static void DoSomething(int value)
            {

            }

            private SomeDelegate<int> someDelegate = new SomeDelegate<int>(DoSomething);
            // or shorthand in C# 2.0
            private SomeDelegate<int> shorthand = DoSomething;

            public class DelegatesCanUseTheClassGnericTypeParameter<T>
            {
                public delegate void GenericDelegate(T value);
            }



            public class UsingTheGenericDelegate
            {
                public void Method()
                {
                    // but you must close the generic if you want to use the delegate:
                    DelegatesCanUseTheClassGnericTypeParameter<int>.GenericDelegate githeDelegate;
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
        }

        public class AttributesCanReferToGenericTypes
        {
            // Custom Attributes can only reference open generic types
            public class CustomAttribute : Attribute
            {
                public Type type;
            }

            public class SomeClass<T> { }

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

        // Although not part of the language, the .NET Framework 2.0 (which shipped with C# 2)
        // contained the Action<T> and Func<T> and Predicate<T> generic delegates
        // Regarding Func<string, bool> and Predicate<string>:
        // You might think these two types are equivalent. They are not. These two variables cannot be used interchangeably. A variable of one type cannot be assigned the other type. The C# type system uses the names of the defined types, not the structure.
        // All these delegate type definitions in the .NET Core Library should mean that you do not need to define a new delegate type for any new feature you create that requires delegates. These generic definitions should provide all the delegate types you need under most situations. You can simply instantiate one of these types with the required type parameters. In the case of algorithms that can be made generic, these delegates can be used as generic types.

        // public static IEnumerable<TSource> Where<TSource> (this IEnumerable<TSource> source, Func<TSource, bool> predicate);
        // This simple example illustrates how delegates require very little coupling between components. You don't need to create a class that derives from a particular base class. You don't need to implement a specific interface. The only requirement is to provide the implementation of one method that is fundamental to the task at hand.

        // Using the delegate types defined in the Core Framework makes it easier for users to work with the delegates. You don't need to define new types, and developers using your library do not need to learn new, specialized delegate types.

        // TODO: Ready about generic in and out covariances
    }
}

