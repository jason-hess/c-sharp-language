using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang20
{
    /// <summary>
    /// The null-coalescing operator <code>??</code> returns the left-hand operand
    /// if the operand is not null, otherwise the right-hand operand is returned.
    /// </summary>
    public class NullCoalescingOperatorBehaviour
    {
        [Test]
        public void ReturnsRightHandSideIfLeftHandOperandIsNull()
        {
            int? leftHandValue = 10;
            int finalValue = leftHandValue ?? 11;
            finalValue.Should().Be(10);
        }

        [Test]
        public void LeftHandOperandMustBeNullable()
        {
            int? leftHandValue = 10;
            int rightHandValue = 11;
            int finalValue = leftHandValue ?? rightHandValue;
            // finalValue = rightHandValue ?? 11; // Error CS0019  Operator '??' cannot be applied to operands of type 'int' and 'int' 
        }

        [Test]
        public void TheOperandsMustBeSubsetsOfTheSameNullableType()
        {
            // the result of the operation is a non-nullable type
            int? x = 10;
            // int? y = x ?? ""; // CS0019  Operator '??' cannot be applied to operands of type 'int?' and 'string' 
        }
    }
}