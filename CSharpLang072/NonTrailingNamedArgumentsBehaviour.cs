using NUnit.Framework;

namespace CSharpLang72
{
    /// <summary>
    /// Prior to C# 7.2 named parameters needed to be at the end of a method invocation.  Now they can proceed
    /// non-named parameters.
    /// </summary>
    public class NonTrailingNamedArgumentsBehaviour
    {
        [Test]
        public void NamedParametersCanProceedNonNamedParameters()
        {
            // Note: Out-of-order named arguments are invalid if they're followed by positional arguments.
            SomeMethod(name: "Jason", 199, 10);
            SomeMethod("Jason", length: 199, height: 10);
        }

        #region Private Members

        private static void SomeMethod(string name, int height, int length)
        {
        }

        #endregion
    }
}
