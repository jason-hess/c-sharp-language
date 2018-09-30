using FluentAssertions;
using NUnit.Framework;
using System;
using TupleAssembly;

namespace CSharpLang
{
    /// <summary>
    /// Tuple element names can now be specified.  Prior to C# 7.0 Tuples existed but their 
    /// members could only be accessed through Item1, Item2, and so on.  Tuple.Create
    /// is no longer needed.
    /// 
    /// Tuples are useful when you need a new type, but don't need to go to the effort
    /// of creating a new `class` or `struct`.
    /// 
    /// You must include the System.ValueTuple NuGet package on platforms that do not
    /// include it.  This is because the .NET Standard and C# rev independently of each other.
    /// 
    /// Guideline: Tuples are most useful for `private` and `internal` methods.  Perhaps
    /// for public APIs, still return `struct`s or `class`es.  Tuples don't support 
    /// much unlike classes and structs.
    /// 
    /// If you want data and behaviour then use a class, otherwise if you want to return
    /// more than one value from a method, you could return a Tuple.
    /// </summary>
    public class TupleBehaviour2
    {
       


    }
}
