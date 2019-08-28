using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace CSharpLang10
{
    /// <summary>
    /// Attributes provide a method of associating metadata, or declarative information,
    /// with code (assemblies, types, methods, properties, and so forth).
    /// After an attribute is associated with a program entity, the attribute can be queried at
    /// run time by using a technique called reflection.
    ///
    /// Attributes can restrict the declaration for which they are valid (e.g. an attribute for methods)
    /// </summary>
    public class AttributeBehaviour
    {
        [Serializable]
        public class AttributesOnClasses
        {
            [Conditional("DEBUG")]
            public void AttributesOnMethods([In, Out]int attributesOnParameters) { }
        }

        public void MultipleAttributesCanBeSpecifiedInTwoForms1([In][Out]int arg) { }
        public void MultipleAttributesCanBeSpecifiedInTwoForms2([In, Out]int arg) { }
        public void MultipleAttributesCanBeSpecifiedInTwoForms3([Out, In]int arg) { }

        [Conditional("DEBUG")]
        [Conditional("TRACE")]
        public void SomeAttributesCanBeSpecifiedMoreThanOnce() { }

        /// <summary>
        /// By convention all attribute names end with "Attribute" but you don't need to specify
        /// "Attribute" when using the attribute
        /// </summary>
        public class MyAttribute : Attribute { }

        [My]
        public void YouDoNotNeedToSpecifyAttributeClassSuffix() { }

        [MyAttribute]
        public void YouCanSpecifyAttributeClassSuffix() { }
    }
}
