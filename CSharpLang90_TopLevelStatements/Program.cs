using System;
using System.IO;

//
// Top-level statements automatically generate a Main() method for the assembly 
// (with <OutputType>Exe</OutputType> the csproj).  
// There can only be one file with top-level statments in a project.  
// These are useful for console applications.
// 
Console.WriteLine("Hello World");

// args is defined as previously for Main methods
if (args.Length > 0)
{
    Console.WriteLine(args[0]);
}

// support for async Main
await File.AppendAllTextAsync("output.txt", "some file content");

// can return a value as an exit code
return -2;