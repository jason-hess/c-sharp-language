using NUnit.Framework;

namespace CSharpLang70
{
    /// <summary>
    /// Tuples allow you to create a simple structure with more than one element without
    /// creating a `class` or a `struct`.  They were available before C# 7.0 but had no
    /// language support and were inefficient.
    /// </summary>
    public class TupleBehaviour
    {
        /// <summary>
        /// Prior to C# 7.0 Tuple elements could be called Item1, Item2, etc.  Now in 
        /// C# 7.0 you can name elements of the Tuple.
        /// </summary>
        [Test]
        public void CanNowAssignNamesToTupleElements()
        {

        }
    }
}
