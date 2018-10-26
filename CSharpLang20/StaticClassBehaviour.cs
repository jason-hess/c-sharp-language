namespace CSharpLang20
{
    /// <summary>
    /// Prior to C# 2.0 it was impossible to create a class without an instance constructor.
    /// In C# 2.0 you can now create a class that cannot be derived from or instantiated.
    /// </summary>
    public class StaticClassBehaviour
    {


        public static class StaticClass
        {
            static StaticClass()
            {

            }
        }
    }
}
