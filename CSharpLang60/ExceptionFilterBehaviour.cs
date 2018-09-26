using System;
using FluentAssertions;
using NUnit.Framework;

namespace CSharpLang60
{
    public class ExceptionFilterBehaviour
    {
        [Test]
        public void RethrowLosesStackTrace()
        {
            Action x = () =>
            {
                int erroCount = 0;
                try
                {
                    Throws();
                }
                catch (Exception ex)
                {
                    erroCount++;
                    throw;
                }
                finally
                {
                    erroCount.Should().Be(1);
                }
            };
            var stackTrace = x.Should().Throw<Exception>().And;
        }

        public void Throws()
        {
            throw new Exception();
        }
    }
}