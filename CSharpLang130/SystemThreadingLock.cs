using NUnit.Framework;

namespace CSharpLang130
{
    /// <summary>
    /// C# 13 introduces the System.Threading.Lock class which offers better performance
    /// than the System.Threading.Monitor.
    /// </summary>
    /// <see href="https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-13#new-lock-object"/>
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
            lock(o) // The Compiler will transpile this into a System.Threading.Monitor call
            {
                // Thread-Safe execution here
            }

            // You can also use System.Threading.Monitor directly - but its
            // easiest to just let the compiler do the work for you.
        }

        /// <summary>
        /// The new System.Threading::Lock returns a System.Threading::Lock.Scope 
        /// - a `ref struct` that implements Disposable.
        /// 
        /// When the `lock` keyword is used with a variable of type System.Threading::Lock
        /// then the compiler will transpile to use System.Threading.Lock.
        /// </summary>
        [Test]
        public void ShouldUseLock()
        {
            Lock aLock = new();
            lock (aLock) {
                Console.WriteLine("thread safe here");
            }

            // the above transpiles to:

            Lock @lock = new Lock();
            Lock.Scope scope = @lock.EnterScope();
            try
            {
                Console.WriteLine("thread safe here");
            }
            finally
            {
                scope.Dispose();

            }

            // If the variable is not a System.THreading.Lock, (even if the variable
            // references a type of System.Threading.Lock, then the compiler will 
            // revert to using System.Threading::Monitor).
            object o = new Lock(); // CS9216: A value of type System.Threading.Lock converted to a different type will use likely unintended monitor-based locking in lock statement.
            lock (o)
            {
                Console.WriteLine("thread safe here with a Monitor");
            }

            // The above transpiles to:
            object obj = new Lock(); // 
            bool lockTaken = false;
            try
            {
                Monitor.Enter(obj, ref lockTaken);
                Console.WriteLine("thread safe here with a Monitor");
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(obj);
                }
            }
        }
    }
}
