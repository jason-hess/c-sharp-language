using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang80
{
    public class IndicesAndRangesBehaviour
    {
        [Test]
        public void RangeShould()
        {
            var names = new List<string>();
            var sublist = "".AsSpan();
            var c = names.Count;
            var x = "123"[0..0];
            x.Length.Should().Be(2);
        }

        [Test]
        public void Should()
        {
            var names = new[] { "Alice", "Bob", "Carol", "Diane", "Esther", "Frank", "Goethe" };

            var lastName = names[1..^1];
            lastName[0].Should().Be("Bob");
            lastName[0] = "Bobby";
            names[1].Should().Be("Bobby");
        }

        [Test]

        public void Should1()
        {
            var names = new[] { 0, 1, 2, 3, 4, 5 };

            for (int i = 1; i < names.Length + 1; i++)
            {
                var x = names[^i];
            }

            var lastName = names[1..^1];
            lastName[0].Should().Be(1);
            lastName[0] = 0;

            names[..].Should().BeEquivalentTo(new[] { 0, 1, 2, 3, 4, 5 });
            names[..^1].Should().BeEquivalentTo(new[] { 0, 1, 2, 3, 4 });
        }

        [Test]
        public void CanDeclareRangeVariable()
        {
            var theRange = 1..^1;
            var theIndex = ^1;
            theIndex.IsFromEnd.Should().BeTrue();
        }



        [Test]
        public void RangesAreExclusive()
        {
            var names = new[] { 0, 1, 2, 3, 4, 5 };

            var range = names[1..1];

            Index start = 0;
            Index theStart = new Index(0, fromEnd: false);



            range.Length.Should().Be(1);
        }

        [Test]
        public void Indexes()
        {
            var numbers = new[] { 0, 1, 2, 3, 4, 5 };
            var reversed = new int[numbers.Length];
            for (int i = 0; i < numbers.Length; i++)
            {
                reversed[i] = numbers[^(i + 1)];
            }

            int index = 1;
            var anIndex = Index.FromEnd(1);
            var name = numbers[^index];

            Range range = 0..^0;
            var subRange = numbers[range];

            var c = new CustomCollection();
        }

        [Test]
        public void Demo()
        {
            int[] numbers = Enumerable.Range(0, 100).ToArray();
            int x = 12;
            int y = 25;
            int z = 36;

            Console.WriteLine($"{numbers[^x]} is the same as {numbers[numbers.Length - x]}");
            Console.WriteLine($"{numbers[x..y].Length} is the same as {y - x}");

            Console.WriteLine("numbers[x..y] and numbers[y..z] are consecutive and disjoint:");
            Span<int> x_y = numbers[x..y];
            Span<int> y_z = numbers[y..z];
            Console.WriteLine($"\tnumbers[x..y] is {x_y[0]} through {x_y[^1]}, numbers[y..z] is {y_z[0]} through {y_z[^1]}");

            Console.WriteLine("numbers[x..^x] removes x elements at each end:");
            Span<int> x_x = numbers[x..^x];
            Console.WriteLine($"\tnumbers[x..^x] starts with {x_x[0]} and ends with {x_x[^1]}");

            Console.WriteLine("numbers[..x] means numbers[0..x] and numbers[x..] means numbers[x..^0]");
            Span<int> start_x = numbers[..x];
            Span<int> zero_x = numbers[0..x];
            Console.WriteLine($"\t{start_x[0]}..{start_x[^1]} is the same as {zero_x[0]}..{zero_x[^1]}");
            Span<int> z_end = numbers[z..];
            Span<int> z_zero = numbers[z..^0];
            Console.WriteLine($"\t{z_end[0]}..{z_end[^1]} is the same as {z_zero[0]}..{z_zero[^1]}");
        }
    }

    
    public class CustomCollection
    {
        private readonly string[] _names = new[] { "Alice", "Bob" };
        public int Length => _names.Length;
        public string this[int index] => _names[index];
        public string[] Slice(int start, int end) => _names[start..^end];
    }

    public class TestClass
    {
        [Test]
        public void ShouldIndex()
        {
            var c = new CustomCollection();
            var v = c[1..^2];
        }
    }
}
