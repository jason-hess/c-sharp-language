using System;
using NUnit.Framework;

namespace CSharpLang
{
    // ref is like a window onto another window.  ref in is like a window
    // ref is like a door.
    // ref readonly
    // ref in
    // ref
    // 

    /// <summary>
    /// In C# 7.0 you now can declare a variable for an out parmeter inline
    /// </summary>
    public class OutParameterBehaviour
    {
        private OutParmeterExamples _underTest;

        [SetUp]
        public void SetUp()
        {
            _underTest = new OutParmeterExamples();
        }

        /// <summary>
        /// Prior to C# 7, variables for out parameters were declared separately
        /// </summary>
        [Test]
        public void PriorOutParameterVariableDeclaration()
        {
            int result; // note: this could be initialized
            _underTest.MethodWithOutParameter(out result);

            int anotherResult = 2;
            _underTest.MethodWithOutParameter(out anotherResult);

            Assert.AreEqual(result, anotherResult);
        }

        /// <summary>
        /// In C# 7 out parameter variables can be declared inline.
        /// Their scope leaks to the outer scope.
        /// </summary>
        [Test]
        public void CanDeclareOutParameterVariablesInline()
        {
            // in C# 7.0 the out variable can be declared inline.
            _underTest.MethodWithOutParameter(out int result);
            _underTest.MethodWithOutParameter(out int anotherResult);

            Assert.AreEqual(result, anotherResult);
        }

        /// <summary>
        /// Variable types for out parameters can also be declared
        /// implicitly
        /// </summary>
        [Test]
        public void CanDeclareOutParameterVariablesImplicityInline()
        {
            _underTest.MethodWithOutParameter(out var result);
            _underTest.MethodWithOutParameter(out var anotherResult);

            Assert.AreEqual(result, anotherResult);
        }
    }
}