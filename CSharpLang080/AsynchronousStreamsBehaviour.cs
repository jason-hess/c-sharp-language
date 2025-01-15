using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang80
{
    public class AsynchronousStreamsBehaviour
    {
        [Test]
        public async Task TestEnumerator()
        {
            await foreach(var s in GetStrings())
            {
                s.Length.Should().Be(1);
            }
        }

        public async IAsyncEnumerable<string> GetStrings()
        {
            await Task.CompletedTask;
            yield return "1";
            yield return "2";
            yield return "3";
        }
    }
}
