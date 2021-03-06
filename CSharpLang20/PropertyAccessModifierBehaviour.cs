﻿namespace CSharpLang20
{
    /// <summary>
    /// Prior to C# 2.0 all the get and set portions of the property each had the same
    /// access modifier.  In C# 2.0 you can now specify different access modifiers
    /// for the get and set portions
    /// </summary>
    public class PropertyAccessModifierBehaviour
    {

        public class ClassWithProperties
        {
            private string _value;

            public string Description
            {
                get { return ""; }
                private set { _value = value; }
            }
        }
    }
}
