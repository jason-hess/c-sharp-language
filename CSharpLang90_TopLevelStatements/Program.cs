using System;

//
// Top-level statements automatically generate a Main() method for the assembly 
// (with <OutputType>Exe</OutputType> the csproj).  
// There can only be one file top-level statments.  These are useful for console applications.
// 
Console.WriteLine("Hello World");

// args is defined as previously for Main methods
if (args.Length > 0)
{
    Console.WriteLine(args[0]);
}