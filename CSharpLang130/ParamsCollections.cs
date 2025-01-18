using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang130
{

    /// <summar>
    /// `params` arguments now supports Collection types
    /// </summary>
    /// <see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/method-parameters#params-modifier" />
    /// <seealso href="https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-13#params-collections"/>
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
        public void ShouldAdd() => 
            Add(1, 2, 3).Should().Be(6);

        /// <summary>
        /// In C# 13, `params` now supports any of:
        ///   - A single-dimensional array
        ///   - `Span<T>` or `ReadOnlySpan<T>`
        ///   - A type that is supported by a Collection expression
        ///   - A type that implements `IEnumerable<T>`
        ///   - One of the following interface types:
        ///     - System.Collections.Generic.IEnumerable<T>
        ///     - System.Collections.Generic.ICollection<T>
        ///     - System.Collections.Generic.IReadOnlyCollection<T>
        ///     - System.Collections.Generic.IList<T>
        ///     - System.Collections.Generic.IReadOnlyList<T>
        /// </summary>
        public static int AddAsSpan(params ReadOnlySpan<int> values)
        {
            // The benefits here are
            //   - that I now have access to all of the features of ReadOnlySpan<int>,
            //     rather than converting from a single-dimensional array into a Span
            //   - I can reuse other functions that may use a ReadOnlySpan rather
            //     than a single-dimensional array
            var newSpan = values.Slice(1);

            int sum = values[0];
            foreach (int value in newSpan)
            {
                sum += value;
            }
            return sum;
        }

        /// <summary>
        /// The compiler will implement this as a conversion to a Span
        /// and then call the method.
        /// </summary>
        [Test]
        public void ShouldAddAsSpan() => 
            AddAsSpan(1, 2, 3).Should().Be(6);
    }
}
