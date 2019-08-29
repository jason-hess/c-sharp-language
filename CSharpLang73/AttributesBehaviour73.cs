using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpLang73
{
    class AttributesBehaviour73
    {
        /** Beginning in C# 7.3, attributes can be applied to either the property or the backing field for an auto-implemented property. The attribute applies to the property, unless you specify the field specifier on the attribute. Both are shown in the following example:
           
           C#
           
           Copy
           class MyClass
           {
           // Attribute attached to property:
           [NewPropertyOrField]
           public string Name { get; set; }
           
           // Attribute attached to backing field:
           [field:NewPropertyOrField]
           public string Description { get; set; }
           }
    **/
    }
}
