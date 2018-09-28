namespace CSharpLang60.Util
{
    public static class StringExtensions
    {
        /// <summary>
        /// Extension Methods are not imported with `using static` statements
        /// </summary>
        /// <param name="aString"></param>
        /// <param name="otherString"></param>
        /// <returns></returns>
        public static bool CaseInsensitiveEquals(this string aString, string otherString)
        {
            return System.String.Equals(aString.ToUpper(), otherString.ToUpper());
        }

        /// <summary>
        /// A sample nested type to demonstrate `using static`
        /// </summary>
        public class NestedClass
        {

        }
    }
}