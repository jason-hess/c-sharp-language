using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
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
        public class AttributesCanBeAssociatedWithClasses
        {
            [Conditional("DEBUG")]
            public void AttributesOnMethods([In, Out] int attributesOnParameters) { }
        }

        public void MultipleAttributesCanBeSpecifiedInTwoForms1([In][Out] int arg) { }
        public void MultipleAttributesCanBeSpecifiedInTwoForms2([In, Out] int arg) { }
        public void MultipleAttributesCanBeSpecifiedInTwoForms3([Out, In] int arg) { }

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
            // How to specify the target of an attribute:
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

        public class DefiningCustomAttributes
        {
            /// <summary>
            /// To define a custom attribute you simply extend System.Attribute
            /// </summary>
            public class MyCustomAttribute : Attribute { }

            [MyCustom]
            public void UsageOfMyCustomAttribute() { }

            /// <summary>
            /// To define where your attribute is valid, decorate it with the AttributeUsage
            /// attribute.
            /// </summary>
            [AttributeUsage(System.AttributeTargets.Method | System.AttributeTargets.Property)]
            public class MyTargetedCustomAttribute : Attribute
            {
                // [MyTargetedCustom] // Error CS0592  Attribute 'MyTargetedCustom' is not valid on this declaration type. It is only valid on 'method, property, indexer' declarations.
                private int _field = 0;
            }

            [AttributeUsage(System.AttributeTargets.All, AllowMultiple = true)]
            public class MyMultipleAttribute : Attribute
            {
                public MyMultipleAttribute(string value) { }

                [MyMultiple("1")]
                [MyMultiple("2")]
                [MyMultiple("3"), MyMultiple("4")]
                public void Method() { }
            }

            public class AnyPublicReadWriteFieldsOrPropertiesAreParameters
            {
                public class MyAttributeWithParametersAttribute : Attribute
                {
                    public int FieldOne;
                    private int _backingValue;

                    /// <summary>
                    /// The constructor's paraemters become positional parameters for the attribute
                    /// </summary>
                    /// <param name="positionalParameter"></param>
                    public MyAttributeWithParametersAttribute(string positionalParameter)
                    {

                    }

                    public int PropertyOne
                    {
                        get { return _backingValue; }
                        set { _backingValue = value; }
                    }
                }

                [MyAttributeWithParameters("poisitonalParameterValue", FieldOne = 1)]
                public void Method() { }
            }

            public class ByDefaultAttributesOnClassesInheritOnDerivedClasses
            {
                [AttributeUsage(System.AttributeTargets.All, Inherited = false)]
                public class ButYouCanDefineAttributesThatDoNotInheritToDerivedClasses : Attribute { }
            }

        }

        /// <summary>
        /// You can retrieve information at runtime about attributes defined at compile time
        /// </summary>
        public class ReflectingOnAttributes
        {
            [AttributeUsage(System.AttributeTargets.Method)]
            public class MyCustomAttribute : Attribute
            {
                public string Value;
            }

            [MyCustom(Value = "One")]
            public void MyMethod() { }

            /// <remarks>
            /// An attribute specification such as:
            /// 
            /// C#
            /// 
            /// Copy
            /// [Author("P. Ackerman", version = 1.1)]  
            /// class SampleClass  
            /// is conceptually equivalent to this:
            /// 
            /// C#
            /// 
            /// Copy
            /// Author anonymousAuthorObject = new Author("P. Ackerman");  
            /// anonymousAuthorObject.version = 1.1;  
            /// However, the code is not executed until SampleClass is queried for attributes. 
            /// Calling GetCustomAttributes on SampleClass causes an Author object to be constructed 
            /// and initialized as above. If the class has other attributes, other attribute objects
            /// are constructed similarly. GetCustomAttributes then returns the Author object and any
            /// other attribute objects in an array. You can then iterate over this array, determine
            /// what attributes were applied based on the type of each array element, and extract
            /// information from the attribute objects.
            /// </remarks>
            [Test]
            public void ShouldBeAbleToSeeCustomAttribute()
            {
                MyCustomAttribute attribute =
                    typeof(ReflectingOnAttributes)
                        .GetMethods()[0]
                        .GetCustomAttributes(false)[0] as MyCustomAttribute;
                string x = attribute.Value;
                FluentAssertions.AssertionExtensions.Should(x).Be("One");
            }
        }
    }
}
