using NUnit.Framework;

namespace CSharpLang80
{
    class StaticLocalFunctionBehaviour
    {
        /// <summary>
        /// local functions can now be static
        /// </summary>
        [Test]
        public void Should()
        {
            static int Add(int x, int y) => x + y;
            int Substract(int x, int y) => x - y;
            Add(1, 2);
            Substract(2, 1);
        }
    }
}
