using NUnit.Framework;

namespace CSharpLang10
{

    /// <summary>
    /// C# 1.0 provides support for late-bound method implementations
    /// </summary>
    public class DelegateBehaviour
    {

        // You define a delegate type using syntax that is similar to defining 
        // a method signature.  You just add the delegate keyword.
        // Under the hood the compiler will generate a class derived from 
        // `System.Delegate` that matches the signature. The compiler also generates
        // add and remove handlers for this new type so that clients of this class
        // can add and remove methods from an instance's invocation list.
        // The compiler will enforce that the signature of the method being
        // added or removed matches the signature used when declaring the method.
        // Note: ***Although this looks like a variable is being declared, 
        //       it's actually a type that is being declared***
        public delegate void Swap(ref int first, ref int second);

        public Swap SwapEvent;

        // You can define delegate types inside classes, directly inside namespaces,
        // or even in the global namespace.

        // After defining the delegate, you can create an instance of that type.
        // Like all variables in C#, you cannot declare delegate instances directly
        // in a namespace, or in the global namespace.

        public Swap SwapMethod;
        private int _value;

        public void SwapOne(ref int first, ref int second)
        {
            int temp = first;
            first = second;
            second = temp;
        }

        public void SwapTwo(ref int first, ref int second)
        {
            _value = first;
        }

        [Test]
        [TestCase(1, 2)]
        public void ShouldSwap(int first, int second)
        {
            SwapMethod += SwapOne;
            SwapMethod += SwapTwo;

            if (SwapMethod == null) return;

            // You invoke the methods in the delegate by invoking the delegate
            // The following invokes the methods attached to the delegate:
            // Note: If no methods have been attached to the delegate,
            //       then NullReferenceException is thrown.
            SwapMethod(ref first, ref second);
            FluentAssertions.AssertionExtensions.Should(first).Be(2);
            FluentAssertions.AssertionExtensions.Should(_value).NotBe(0);
        }

        // This simple example illustrates how delegates require very little coupling between components. You don't need to create a class that derives from a particular base class. You don't need to implement a specific interface. The only requirement is to provide the implementation of one method that is fundamental to the task at hand.

    }
}
