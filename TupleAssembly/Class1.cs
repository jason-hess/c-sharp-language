namespace TupleAssembly
{
    public class TupleClass
    {
        public (string Alpha, string Beta) TupleMethod()
        {
            return ("1", "2");
        }

        public void Deconstruct(out int alpha, out int beta)
        {
            alpha = 1;
            beta = 2;
        }
    }
}
