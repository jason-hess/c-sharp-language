using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CSharpLang60
{
    public class AwaitInCatchAndFinallyBlocksBehaviour
    {
        /// <summary>
        /// await is now supported in catch and finally blocks.  The benefit of this
        /// is that if the catch or finally block throws an exception in asyncronous code
        /// the same behaviour will occur as if it was synchronous code - the surrounding code
        /// is searched for a catch block.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AwaitShouldWorkInCatchAndFinallyAsync()
        {
            try
            {
                // do something
            } catch (Exception)
            {
                await Trace("in catch");
            }
            finally
            {
                await Trace("in finally");
            }
        }

        private async Task Trace(string message)
        {
            await Task.CompletedTask;
        }
    }
}
