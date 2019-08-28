using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using FluentAssertions;
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

        /// <summary>
        /// Many attributes have parameters, which can be positional, unnamed, or named.
        /// Any positional parameters must be specified in a certain order and cannot be omitted.
        /// Named parameters are optional and can be specified in any order. 
        /// </summary>
        [Test(Author = "Billy Bob", Description = "Does Some Stuff")]
        [TestCase(1, Description = "Some Test", Author = "Billy's Brother Bob")]
        public void AttributeParameters(int x)
        {
            x.Should()
                .Be(1);
        }

        /// <summary>
        /// The targets of an attribute can be:
        /// assembly - Entire assembly
        /// module - Current assembly module
        /// field - Field in a class or a struct
        /// event - Event
        /// method - Method or get and set property accessors
        /// param - Method parameters or set property accessor parameters
        /// property - Property
        /// return - Return value of a method, property indexer, or get property accessor
        /// type - Struct, class, interface, enum, or delegate
        /// </summary>
        public class AttributeTargets
        {
            [type: Conditional("DEBUG")]
            public class MyTargetedAttribute : Attribute
            {
                [return: MyAttribute]
                public int MethodWithReturnAttribute()
                {
                    return 0;
                }
            }

            // To add an attribute to the assembly you would:
            // using System;
            // using System.Reflection;
            // [assembly: AssemblyTitleAttribute("Production assembly 4")]
            // [module: CLSCompliant(true)]

            // Common uses for attributes
            // The following list includes a few of the common uses of attributes in code:
            // 
            // - Marking methods using the WebMethod attribute in Web services to indicate that the method should be callable over the SOAP protocol. For more information, see WebMethodAttribute.
            // - Describing how to marshal method parameters when interoperating with native code. For more information, see MarshalAsAttribute.
            // - Describing the COM properties for classes, methods, and interfaces.
            // - Calling unmanaged code using the DllImportAttribute class.
            // - Describing your assembly in terms of title, version, description, or trademark.
            // - Describing which members of a class to serialize for persistence.
            // - Describing how to map between class members and XML nodes for XML serialization.
            // - Describing the security requirements for methods.
            // - Specifying characteristics used to enforce security.
            // - Controlling optimizations by the just-in-time (JIT) compiler so the code remains easy to debug.
            // - Obtaining information about the caller to a method.
        }
    }
}
