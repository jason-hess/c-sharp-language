using System;
using System.Threading.Tasks;

namespace CSharpLang71
{
    /// <summary>
    /// In C# 7.2 you can now write an async Main method that returns a Task or a Task of int.
    /// </summary>
    class AsyncMainIsNowPossible
    {
        /// <remarks>
        /// This method could also return a Task if it doesn't return a value.
        /// </remarks>
        static async Task<int> Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            await Task.CompletedTask;
            return 0;
        }
    }
}
