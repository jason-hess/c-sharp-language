using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CSharpLang60
{
    /// <summary>
    /// In C# 6.0 await is now supported in catch and finally blocks.  The benefit of this
    /// is that if the catch or finally block throws an exception in asynchronous code
    /// the same behaviour will occur as if it was synchronous code - the surrounding code
    /// is searched for a catch block.
    /// </summary>
    /// <returns></returns>
    public class AwaitInCatchAndFinallyBlocksBehaviour
    {
        [Test]
        public async Task AwaitShouldWorkInCatchAndFinallyAsync()
        {
            try
            {
                await DoSomethingAsync();
            }
            catch (Exception)
            {
                await Trace("in catch");
            }
            finally
            {
                await Trace("in finally");
            }
        }

        private static async Task DoSomethingAsync()
        {
            await Task.CompletedTask;
        }

        private static async Task Trace(string message)
        {
            await Task.CompletedTask;
        }
    }
}
