namespace CSharpLang60
{
    /// <summary>
    /// The -deterministic option instructs the compiler to produce a byte-for-byte identical output assembly for successive compilations of the same source files.
    /// 
    /// By default, every compilation produces unique output on each compilation. The compiler adds a timestamp, and a GUID generated from random numbers. You use this option if you want to compare the byte-for-byte output to ensure consistency across builds.
    /// 
    /// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/deterministic-compiler-option
    /// </summary>
    class DeterministicBuildOutputs
    {
    }
}
