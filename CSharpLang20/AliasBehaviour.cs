using FluentAssertions;
using x = System;

namespace CSharpLang20
{
    /// <summary>
    /// C# 2.0 introduced aliases.  <code>using anAlias = namespace;</code> allowed you
    /// to refer to the namespace with the alias.
    /// </summary>
    public class AliasBehaviour
    {
        /// <summary>
        /// The <code>global</code> namespace allows you to reference the root namespace
        /// </summary>
        public void GlobalNamespaceShouldReferToRootNamespace()
        {
            global::System.String x = "";
            x.Should().Be("");
        }

        public void AliasRefersToNamespace()
        {
            // The `x` now refers to `System` because of the using alias at the top of the file.
            x::String y = "";
            y.Should().Be("");
        }
    }
}
