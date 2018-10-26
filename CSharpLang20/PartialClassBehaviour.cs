namespace CSharpLang20
{
    /// <summary>
    /// C# 2.0 added support for partial classes.  During complication the two definitions
    /// are combined into a single.  The benefit of these definitions are mostly for generated
    /// code.
    /// </summary>
    public partial class PartialClassBehaviour
    {
        public string Description
        {
            get { return "PartialClassBehaviour"; }
        }
    }

    public partial class PartialClassBehaviour
    {
        public int Length
        {
            get { return Description.Length; }
        }
    }
}
