using FluentAssertions;
using NUnit.Framework;
using System;

namespace CSharpLang60
{
    /// <summary>
    /// C# 6.0 adds support for adding filters to `catch` blocks which prevent Exceptions
    /// from being caught based on the result of a boolean expression.  This preserves 
    /// Exception details for Exceptions that are not caught.  Exception filters are 
    /// expressions that determine when a given catch clause should be applied.
    /// </summary>
    public class ExceptionFilterBehaviour
    {
        private void RethrowException()
        {
            try
            {
                ThrowException();
            }
            catch (Exception)
            {
                // as soon as the catch block is entered the stack is unwound which means
                // that we lose the details of the stack as they were in the ThrowException 
                // method.
                throw;  
            }
        }

        private static void NotCatchException()
        {
            ThrowException();
        }

        private static void ThrowException()
        {
            throw new Exception("exception message");
        }

        /// <summary>
        /// Calling `throw;` in C# has always changed the stack trace of Exceptions that
        /// are caught.  Re-throwing means the throw location from the method that contains
        /// it is changed from the try line to the throw line.
        /// </summary>
        [Test]
        public void RethrowLosesStackTrace()
        {
            try
            {
                RethrowException();
            }
            catch (Exception ex)
            {
                ex.StackTrace
                    .Should()
                    .Contain("37")
                    .And
                    .Contain("19")
                    .And
                    .Contain("50", because: "calling throw; changes moves the throw point for the method that contains it");
            }
        }

        /// <summary>
        /// Not catching Exceptions keeps the stack trace and adds another line 
        /// of the method that invoked the method.
        /// </summary>
        [Test]
        public void NotCatchExceptionKeepsStackTrace()
        {
            try
            {
                NotCatchException();
            }
            catch (Exception ex)
            {
                ex.StackTrace
                   .Should()
                   .Contain("37")
                   .And
                   .Contain("32")
                   .And
                   .Contain("73", because: "not catching an Exception adds a stack trace the call stack to the Exception");
            }
        }

        /// <summary>
        /// Because Exception filter expressions can reduce the number of exceptions that are
        /// caught, Stack Traces can be left intact.
        /// </summary>
        [Test]
        public void ExceptionFiltersReduceThrowSemicolonStatements()
        {
            try
            {
                ThrowAndFilterException();
            }
            catch (Exception)
            {
                // ex.StackTrace is left untouched here because the Exception is not caught in 
                // ThrowAndFilterException();
            }
        }

        /// <summary>
        /// C# 6.0 adds support for Exception Filter expressions which allows you 
        /// move conditions into the filter.
        /// </summary>
        private void ThrowAndFilterException()
        {
            try
            {
                ThrowException();
            }
            catch (Exception ex) when (ex.Message.Length > 0)
            {
                // previously you might have an if statement here to filter and re-throw
            }
        }

        [Test]
        public void CanInvokeMethodsInExceptionFilters()
        {
            try
            {
                throw new Exception();
            }
            catch (Exception ex) when (ShouldCatchException(ex))
            {

            }
            catch (Exception) when (ShouldCatchAllExceptions())
            {
                // filters are checked in order
            }
            catch (Exception) when (!System.Diagnostics.Debugger.IsAttached)
            {
                // do something when not debugging
            }
            // Note: `await` is not supported for filters.  The following generates
            // Error	CS7094	Cannot await in the filter expression of a catch clause
            //catch (Exception ex) when (await ShouldCatchException(ex))
            //{

            //}
        }

        private bool ShouldCatchException(Exception ex)
        {
            // I could log this Exception here
            return true;
        }

        private bool ShouldCatchAllExceptions()
        {
            return true;
        }
    }
}