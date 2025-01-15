namespace CSharpLang72
{
    class StructBehaviour
    {
        /// <summary>
        /// The readonly struct declaration, to indicate that a struct is immutable and should be passed as an in parameter to its member methods. Adding the readonly modifier to an existing struct declaration is a binary compatible change.
        /// </summary>
        public readonly struct ReadOnlyPoint
        {
            public ReadOnlyPoint(int x)
            {
                // X = x;
            }

            // public int X; // Error CS8340  Instance fields of readonly structs must be readonly.

        }
    }
}
