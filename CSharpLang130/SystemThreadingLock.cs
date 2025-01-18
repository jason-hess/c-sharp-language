using NUnit.Framework;

namespace CSharpLang130
{
    /// <summary>
    /// C# 13 introduces the System.Threading.Lock class which offers better performance
    /// than the System.Threading.Monitor.
    /// </summary>
    public class SystemThreadingLock
    {
        /// <summary>
        /// The `lock` keyword is simply syntactic sugar for the System.THreading.Monitor
        /// class.  Historically, 
        /// 
        ///     var o = new object();
        ///     lock(o) { Console.WriteLine("locked"); }
        /// 
        /// gets compiled to
        /// 
        ///     object obj = new object();
        ///     bool lockTaken = false;
        ///     try
        ///     {
        ///         Monitor.Enter(obj, ref lockTaken);
        ///         Console.WriteLine("locked");
        ///     }
        ///     finally
        ///     {
        ///         if (lockTaken) { Monitor.Exit(obj); }
        ///     }
        ///     
        /// </summary>
        [Test]
        public void ShouldUseMonitor()
        {
            var o = new object();
            lock(o)
            {
                // Should only be one thread allowed in here at a time
            }
        }

        /// <summary>
        /// The new System.Threading.Lock
        /// </summary>
        [Test]
        public void ShouldUseLock()
        {

        }
    }
}
