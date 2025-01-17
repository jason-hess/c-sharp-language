using System.IO;

namespace CSharpLang80
{
    public class UsingBehaviour
    {
        public void Method()
        {
            // A new syntax for using means this will be disposed when its scope is
            // left (i.e. the scope of the current method).
            var stream = new System.IO.StreamWriter("WriteLines2.txt");
            using (stream) ;
            stream.Write(true);
            if (stream is StreamWriter x)
            {
                x.Dispose();
            }
        }
    }
}
