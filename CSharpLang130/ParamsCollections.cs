using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang130
{

    public class ParamsCollections
    {
        /// <summary>
        /// Prior to C# 13, the `params` keyword only support single-dimensional arrays
        /// </summary>
        public static int Add(params int[] values)
        {
            int sum = 0;
            foreach (int value in values)
            {
                sum += value;
            }
            return sum;
        }

        [Test]
        public void ShouldAdd() => Add(1, 2, 3).Should().Be(6);
    }
}
