namespace CSharpLang60
{
    public static class StringExtensions
    {
        public static bool CaseInsensitiveEquals(this string aString, string otherString)
        {
            return System.String.Equals(aString.ToUpper(), otherString.ToUpper());
        }

        public class NestedClass
        {
            
        }
    }
}