using System;

namespace CSharpLang80
{
    public class UsingBehaviour
    {
        public void Method()
        {
            // A new syntax for using means this will be disposed when its scope is
            // left (i.e. the scope of the current method).
            using var stream = new System.IO.StreamWriter("WriteLines2.txt");
        }
    }
}
